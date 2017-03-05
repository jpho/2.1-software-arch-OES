using System;
using System.Collections.Generic;
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

namespace OrderEntrySystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // public string DisplayName { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            //   this.DisplayName = "Order Entry System - Yang";


        }

        ////private void newProductButton_Click(object sender, RoutedEventArgs e)
        ////{
        ////    (this.DataContext as MainWindowViewModel).CreateNewProduct();
        ////}
    }
}
