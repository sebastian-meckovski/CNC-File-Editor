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
        public MainWindow()
        {
            InitializeComponent();
        }
        ObservableCollection<string> myItems = new ObservableCollection<string>();
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
            myItems.RemoveAt(myListView.SelectedIndex);
            //myListView.Items.RemoveAt(myListView.SelectedIndex);  // Error here. Not sure what it means. Need help
            // Operation is not valid while ItemsSource is in use. Access and modify elements with ItemsControl.ItemsSource instead.
        }
    }
}
