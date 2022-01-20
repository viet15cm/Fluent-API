using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace Demo
{
    class DemoSoft
    {
    }

    public interface IAppComparer<T>
    {
        int Compare([AllowNull] T x);
    }

    public class AppStudent : IAppComparer<AppStudent>
    {
        public string Name { get; set; }


        public AppStudent(string Name)
        {
            this.Name = Name;
        }
        public int Compare([AllowNull] AppStudent x)
        {
            return Name.CompareTo(x.Name);
        }
    }

    public  class AppList
    {
        public void Soft<T>(IAppComparer<T> Comparer)
        {

            var method = Comparer.GetType().GetMethod(nameof(Comparer.Compare));
            Type type = method.ReturnType;

            object[] mParams = new object[] { Comparer };

            var returnValue = method.Invoke(Comparer, mParams);

            Console.WriteLine(returnValue);

        }
    }

    public class MyTypeClass
    {
        public void MyMethods()
        {
        }
        public int MyMethods1()
        {
            return 3;
        }
        protected String MyMethods2()
        {
            return "hello";
        }
    }

  
}
