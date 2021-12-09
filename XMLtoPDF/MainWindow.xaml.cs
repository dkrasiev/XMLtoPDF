using System.Collections.Generic;
using System.Windows;

namespace XMLtoPDF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Person> people = new List<Person>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            Program program = new Program();

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "XML documents (*.xml)|*.xml|All Files (*.*)|*.*",
                Multiselect = false
            };

            if (dlg.ShowDialog() == true)
            {
                program.GetPeopleFromXml(dlg.FileName, ref people);
            }

            if (people.Count != 0)
            {
                textBlockFile.Text = dlg.FileName;
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Program program = new Program();

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "PDF document (*.pdf)|*.pdf",
                CheckFileExists = false,
                CheckPathExists = true,
                ValidateNames = false,
                FileName = "result.pdf"
            };

            if (people.Count == 0)
            {
                program.Error("Нечего сохранить");
            }
            else if (dlg.ShowDialog() == true)
            {
                program.SavePDF(dlg.FileName, people);
                program.Succes("Сохранено успешно");
            }
        }
    }
}
