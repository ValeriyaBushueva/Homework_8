using System;
using System.Linq;
using System.IO;
using System.Xml.Linq;

//Бушуева Валерия
//Написать программу-преобразователь из CSV в XML-файл с информацией о студентах (6 урок).

namespace ConvertFileStudenttoXML
{
    internal class Program
    {
        
        static void Converter(string fileNameOpen, string fileNameSave)
        {
            string[] lines = File.ReadAllLines(fileNameOpen);
            string[] headers = { "FirstName", "LastName", "University","Age", "Course", "City" };
        
            var xml = new XElement("Students",
                lines.Where((line, index) => index > 0).Select(line => new XElement("StudentIndo",
                    line.Split(';').Select((column, index) => new XElement(headers[index], column)))));
        
            xml.Save(fileNameSave);
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("Вас приветствует программа-преобразователь из CSV в XML-файл с информацией о студентах");
            
            Converter("..\\..\\StudentFile.txt", "..\\..\\studentFileSave.xml");
            
            Console.WriteLine("Работа программы завершена! Посмотреть файл можно в папке.");
            Console.ReadLine();
        }
    }
}