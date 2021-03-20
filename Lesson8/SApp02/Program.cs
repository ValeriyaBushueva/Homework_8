using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SApp02
{
    class Program
    {


        public static Person LoadPersonFromXml(string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                return (Person)xmlSerializer.Deserialize(fileStream);
            }
        }

        public static void SavePersonToXml(string fileName, Person person)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));
            using (var fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                 xmlSerializer.Serialize(fileStream, person);
            }
        }

        static void Main(string[] args)
        {
            var person = LoadPersonFromXml(AppDomain.CurrentDomain.BaseDirectory + "Person.xml");
            if (person != null)
            {
                Console.WriteLine($"{person.LastName} {person.FirstName} {person.SecondName} {person.Age}");
            }

            person.FirstName = "Андрей";

            SavePersonToXml(AppDomain.CurrentDomain.BaseDirectory + "Person.new.xml", person);

            person = LoadPersonFromXml(AppDomain.CurrentDomain.BaseDirectory + "Person.new.xml");
            if (person != null)
            {
                Console.WriteLine($"{person.LastName} {person.FirstName} {person.SecondName} {person.Age}");
            }

            Console.ReadLine();
        }
    }
}
