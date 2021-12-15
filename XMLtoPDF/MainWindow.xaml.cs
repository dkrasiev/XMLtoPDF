using System.Collections.Generic;
using System.Windows;

namespace XMLtoPDF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Person> _people = new List<Person>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "XML documents (*.xml)|*.xml|All Files (*.*)|*.*",
                Multiselect = false,
                CheckFileExists = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                if (Program.GetPeopleFromXml(openFileDialog.FileName, out _people))
                {
                    textBlockFile.Text = openFileDialog.FileName;
                }
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (_people.Count == 0)
            {
                Program.Error("Список пустой");
                return;
            }

            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "PDF document (*.pdf)|*.pdf",
                CheckFileExists = false,
                CheckPathExists = true,
                ValidateNames = false,
                FileName = "result.pdf"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                Program.SavePDF(saveFileDialog.FileName, _people);
            }
        }
    }
}
