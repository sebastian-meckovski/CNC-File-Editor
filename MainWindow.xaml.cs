using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using MassTextModifier.Classess;

namespace MassTextModifier
{
    public partial class MainWindow : Window
    {
        ObservableCollection<string> myItems = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();

            OverWriteRadioButton.IsChecked = true;

            string outputFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            this.outputFilePathLabel.Content = outputFilePath;
        }
        public void Browse_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();

            var students = openFileDialog.FileNames.ToList();

            foreach(var student in students)
            {
                myItems.Add(student);
            }

            myListView.ItemsSource = myItems;

        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            //if (myListView.SelectedItem != null)
            //{
            //    int myIndex = myListView.SelectedIndex;
            //    myItems.RemoveAt(myIndex);
            //    myListView.SelectedItem = myListView.Items[myIndex];
            //}

            int myIndex = myListView.SelectedIndex;                  // can I write code like this? It works better because it will always handle error for me
            try
            {
                myItems.RemoveAt(myIndex);
                myListView.SelectedItem = myListView.Items[myIndex];
            }
            catch (ArgumentOutOfRangeException)
            {

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
            //still don't know how to do that
            outputFilePathLabel.Content = "mnononononon";
        }

        private void Execute_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OverWriteRadioButton.IsChecked == true)
                {
                    foreach (string itemFilePath in myItems)
                    {
                        textModifier.OverwriteFile(itemFilePath, itemFilePath);
                        Debug.WriteLine(System.IO.Path.GetFileName(itemFilePath));
                        Debug.WriteLine(System.IO.Path.GetDirectoryName(itemFilePath));
                    }
                    MessageBox.Show($"{myItems.Count} files have been modified");
                    myItems.Clear();
                }
                else
                {
                    
                    foreach (string itemfilePath in myItems)
                    {
                        string newFilePath = System.IO.Path.Combine(outputFilePathLabel.Content.ToString(), System.IO.Path.GetFileName(itemfilePath));
                        textModifier.OverwriteFile(itemfilePath, newFilePath);
                    }
                    MessageBox.Show($"{myItems.Count} files have been modified");
                    myItems.Clear();
                }
            }
            catch
            {
                MessageBox.Show("Unexpected error occured");
            }
        }
    }
}
