using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using MassTextModifier.Classess;
using MassTextModifier.Model;
// Hi Gautam. My questions are on lines 51, 76, 98, 124
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


            //string outputFilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //this.outputFilePathLabel.Content = outputFilePath;
        }
        public void Browse_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();

            var students = openFileDialog.FileNames.ToList();
            

            foreach (var student in students)
            {
                FileInfo fileInfo = new FileInfo();
                fileInfo.FilePath = student;
                myItems.Add(fileInfo);
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
            // not sure if it's better to use try or if statement here. //Try catch is always good practive to use
            int myIndex = myListView.SelectedIndex;                  // can I write code like this? It works better for me because 
            try                                                      // it will always handle error even if I could not predict it
            {
                myItems.RemoveAt(myIndex);
                myListView.SelectedItem = myListView.Items[myIndex];
            }
            catch (OutOfMemoryException ex) //only get out of exception index
            {
                MessageBox.Show("Your personalize messge for the out of index exception");
            }
            catch (Exception ex)//try to get all exception even if other than out of index exception
            {
                MessageBox.Show(ex.Message);
                //ex.Message; error message
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
            myItems = new ObservableCollection<FileInfo>(myItems.OrderBy(i => i.FileName)); //just use linq to order by and observable collection for the sync ui with list
            //still don't know how to do that. I tried
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
                        string newFilePath = System.IO.Path.Combine(Convert.ToString(outputFilePathLabel.Content), System.IO.Path.GetFileName(itemfilePath.FilePath));  // can I pass label.content as a string //label.content is fine Try use Convert.string() rather than toString() method
                        textModifier.OverwriteFile(itemfilePath.FilePath, newFilePath);                                                                          // argument or is it a bad practice? //we can discuss this not sure what this means question
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
    }
}


// Display File names only checkbox not sure if there is an easy way to implement it.
// you mean showing file name list with checkbox?