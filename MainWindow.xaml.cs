using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MassTextModifier
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> myItems = new ObservableCollection<string>();

        public MainWindow()
        {
            InitializeComponent();
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
        }
    }
}
