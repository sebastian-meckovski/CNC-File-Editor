using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using MassTextModifier.Classess;
using MassTextModifier.Model;
using Microsoft.WindowsAPICodePack.Dialogs; // the library I installed, not sure if necesarry.
// Hello Gautam. Questions are on line 75 and 135

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
            //this.outputFilePathLabel.Content = outputFilePath; //what's the difference
            outputFilePathLabel.Content = outputFilePath;
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

        private void Sort_Button_Click(object sender, RoutedEventArgs e)
        {
            myItems = new ObservableCollection<FileInfo>(myItems.OrderBy(i => i.FileName)); 


            // I tried testing your code but it still doesn't work.
            // Also, Is there a way to sort it in descending order?
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
            var dialog = new CommonOpenFileDialog();                     // Is this the only way to create directory filepath dialog?
            dialog.IsFolderPicker = true;                                // I had to install a package for that. I didn't have to install it for filepath dialog.
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                outputFilePathLabel.Content = dialog.FileName;
            }
        }
    }
}
