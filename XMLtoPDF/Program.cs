using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Xml;

namespace XMLtoPDF
{
    static class Program
    {
        /// <summary>
        /// Создает список из xml документа
        /// </summary>
        /// <param name="filename"></param>
        public static bool GetPeopleFromXml(string filename, out List<Person> people)
        {
            people = new();

            try
            {
                // Создание xml документа
                XmlDocument xmlDocument = new XmlDocument();
                // Открытие xml документа
                xmlDocument.Load(filename);
                // Получение главного корня
                XmlElement xmlRoot = xmlDocument.DocumentElement;

                // Наполняет список данными
                foreach (XmlNode xmlNode in xmlRoot)
                {
                    string name = null;
                    int age = 0;
                    string company = null;

                    foreach (XmlNode childNode in xmlNode.ChildNodes)
                    {
                        switch (childNode.Name)
                        {
                            case "Name":
                                name = childNode.InnerText;
                                break;
                            case "Age":
                                age = int.Parse(childNode.InnerText);
                                break;
                            case "Company":
                                company = childNode.FirstChild.InnerText;
                                break;
                        }
                    }

                    people.Add(new Person(name, age, company));
                }
            }
            catch (Exception ex)
            {
                Error(ex.Message);
                people.Clear();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Сортирует список по компаниям и сохраняет его в PDF в виде таблицы
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="people"></param>
        public static void SavePDF(string filename, List<Person> people)
        {
            // Сортировка
            people.Sort((a, b) => a.Company.CompareTo(b.Company));

            // Создание pdf документа
            Document doc = new Document();

            // Создания файла pdf документа
            PdfWriter.GetInstance(doc, new FileStream(filename, FileMode.Create));

            // Открытие pdf документа
            doc.Open();

            // Создание таблицы
            PdfPTable table = new PdfPTable(3);

            // Создание шапки таблицы
            List<string> titles = new List<string>() { "Name", "Age", "Company" };
            foreach (string title in titles)
            {
                PdfPCell cell = new PdfPCell(new Phrase(title));
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                table.AddCell(cell);
            }

            // Заполнение таблицы
            foreach (Person person in people)
            {
                table.AddCell(person.Name);
                table.AddCell(person.Age.ToString());
                table.AddCell(person.Company);
            }

            // Добавление таблицы в pdf документ
            doc.Add(table);

            // Закрытие pdf документа
            doc.Close();

            Succes("Сохранено успешно");
        }

        public static void Error(string message)
        {
            string messageBoxText = message;
            string caption = "Error";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;

            MessageBox.Show(messageBoxText, caption, button, icon);
        }

        public static void Succes(string message)
        {
            string messageBoxText = message;
            string caption = "Succes";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;

            MessageBox.Show(messageBoxText, caption, button, icon);
        }
    }
}
