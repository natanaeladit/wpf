using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using System.Xml.Linq;
using System.Xml.Serialization;

namespace App
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = TextBoxFolder.Text;

            string[] files = InputOutput.ReadFilesFromFolder(folderPath);

            if (files == null)
            {
                MessageBox.Show("No xml file found in the folder. Please make sure folder path is valid.");
                return;
            }

            List<CustomerInfoRequest> custInfoList = new List<CustomerInfoRequest>();
            foreach (var file in files)
            {
                string xmlInputData = InputOutput.ReadFileContent(file);

                CustomerInfoRequest obj = Serializer.GetCustomerInfoRequest(xmlInputData);
                if (obj != null)
                    custInfoList.Add(obj);
            }

            if (custInfoList.Count > 0)
            {
                itemDataGrid.ItemsSource = custInfoList;
            }
        }
    }
}
