using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SApp01
{

    public class MyClass
    {
        private int _myField;

        public int MyProperty
        {
            get { return _myField; }
            set { _myField = value; }
        }

        void DoOperation()
        {

        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Type type01 = typeof(MyClass);


            var myClass = new MyClass();
            myClass.MyProperty = 12;

            var myClass02 = new MyClass();
            myClass02.MyProperty = 50;

            Type type02 = myClass.GetType();


            PropertyInfo propertyInfo = type01.GetProperty("MyProperty");

            if (propertyInfo.CanRead)
            {
                var value = propertyInfo.GetValue(myClass02);
                Console.WriteLine($"MyProperty => {value}");
            }

            if (propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(myClass02, 10);
            }

            if (propertyInfo.CanRead)
            {
                var value = propertyInfo.GetValue(myClass02);
                Console.WriteLine($"MyProperty => {value}");
            }



            Console.ReadKey();
        }
    }
}
