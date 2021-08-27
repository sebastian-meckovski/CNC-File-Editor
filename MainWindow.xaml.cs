using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using MassTextModifier.Classess;
using MassTextModifier.Model;

using MassTextModifier.Extention;
// Hello Gautam. Questions are on line 27, 75 and 135

namespace MassTextModifier
{
    public partial class MainWindow : Window
    {
        ObservableCollection<FileInfo> myItems = new ObservableCollection<FileInfo>();

        public MainWindow()
        {
            InitializeComponent();

            OverWriteRadioButton.IsChecked = true;
            SelectOutputLocationButton.IsEnabled = false;


            string outputFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //this.outputFilePathLabel.Content = outputFilePath;                                       // what's the difference between this.outputFilePathLabel.Content
            outputFilePathLabel.Content = outputFilePath;                                              // and outputFilePathLabel.Content? 
                                                                                                       //this refer to class MainWindow and so this. all the property of the class 
                                                                                                       //it means outputFilePathLabel is property of MainWindow
                                                                                                       //hence this.outputFilePathLabel and outputFilePathLabel both are the same
        }
        public void Browse_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();

            var listOfItems = openFileDialog.FileNames.ToList();

            foreach (var student in listOfItems)
            {
                FileInfo fileInfo = new FileInfo();
                fileInfo.FilePath = student;
                myItems.Add(fileInfo);
            }

            myListView.ItemsSource = myItems;
        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            int myIndex = myListView.SelectedIndex;
            try
            {
                myItems.RemoveAt(myIndex);
                myListView.SelectedItem = myListView.Items[myIndex];
            }
            catch (Exception)
            {
                // I left it blank because I want the program to perform no action if there is an exception.
            }
        }

        private void Delete_All_Button_Click(object sender, RoutedEventArgs e)
        {
            if (myItems != null)
            {
                myItems.Clear();
            }
        }

        private void Sort_A_Z_Button_Click(object sender, RoutedEventArgs e)
        {
            myItems.Sort((a, b) => { return a.FileName.CompareTo(b.FileName); });

            sortButton.Click += new RoutedEventHandler(Sort_Z_A_Button_Click);
            sortButton.Click -= new RoutedEventHandler(Sort_A_Z_Button_Click);
            sortButton.Content = "Sort Z-A";
        }

        private void Sort_Z_A_Button_Click(object sender, RoutedEventArgs e)
        {
            myItems.Sort((b, a) => { return a.FileName.CompareTo(b.FileName); }); //Gautam2010 wrote this and I'm not sure what

            sortButton.Click += new RoutedEventHandler(Sort_A_Z_Button_Click);
            sortButton.Click -= new RoutedEventHandler(Sort_Z_A_Button_Click);
            sortButton.Content = "Sort A-Z";
        }

        private void Execute_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OverWriteRadioButton.IsChecked == true)
                {
                    foreach (FileInfo itemFilePath in myItems)
                    {
                        textModifier.OverwriteFile(itemFilePath.FilePath, itemFilePath.FilePath);
                        Debug.WriteLine(System.IO.Path.GetFileName(itemFilePath.FilePath));
                        Debug.WriteLine(System.IO.Path.GetDirectoryName(itemFilePath.FilePath));
                    }
                    MessageBox.Show($"{myItems.Count} files have been modified");
                    myItems.Clear();
                }
                else
                {
                    foreach (FileInfo itemfilePath in myItems)
                    {
                        string newFilePath = System.IO.Path.Combine(Convert.ToString(outputFilePathLabel.Content), System.IO.Path.GetFileName(itemfilePath.FilePath));
                        textModifier.OverwriteFile(itemfilePath.FilePath, newFilePath);
                    }
                    MessageBox.Show($"{myItems.Count} files have been saved at {outputFilePathLabel.Content}");
                    myItems.Clear();
                }
            }
            catch
            {
                MessageBox.Show("Unexpected error occured");
            }
        }

        private void CreateNewRadioButton_Click(object sender, RoutedEventArgs e)
        {
            SelectOutputLocationButton.IsEnabled = true;
        }

        private void OverWriteRadioButton_Click(object sender, RoutedEventArgs e)
        {
            SelectOutputLocationButton.IsEnabled = false;
        }

        private void MyCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (MyCheckBox.IsChecked == true)
            {
                myListView.DisplayMemberPath = "FileName";
            }
            else
            {
                myListView.DisplayMemberPath = "FilePath";
            }
        }

        private void SelectOutputLocationButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog1 = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result1 = dialog1.ShowDialog();
                if (result1 == System.Windows.Forms.DialogResult.OK)
                {
                    outputFilePathLabel.Content = dialog1.SelectedPath;
                }
            }

            //System.Windows.Forms does provide the same feature without installing package uncomment above lines to see same result.

            //using Microsoft.WindowsAPICodePack.Dialogs; // the library I installed, not sure if necesarry.
            //using System.Collections.Generic;


            //var dialog = new CommonOpenFileDialog();                     // Is this the only way to create directory filepath dialog?
            //dialog.IsFolderPicker = true;                                // I had to install a package for that. I didn't have to install it for filepath dialog on line 30.
            //CommonFileDialogResult result = dialog.ShowDialog();
            //if (result == CommonFileDialogResult.Ok)
            //{
            //    outputFilePathLabel.Content = dialog.FileName;
            //}
        }
    }
}