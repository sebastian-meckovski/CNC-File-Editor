using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

        public void Browse_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();

            List<string> myItems = openFileDialog.FileNames.ToList();

            myListView.ItemsSource = myItems;

        }

        private void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            myListView.Items.RemoveAt(1);  // Error here. Not sure what it means. Need help
                                           // Operation is not valid while ItemsSource is in use. Access and modify elements with ItemsControl.ItemsSource instead.
        }
    }
}
