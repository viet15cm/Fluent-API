using Demo.DbContextLayer;
using Demo.Describer;
using Demo.Models;
using Demo.Serviecs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Demo
{
   public static class Program
    {


        public static IEnumerable<TResult> Select1<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            var item = source.ToList();
            List<TResult> results = new List<TResult>();
            foreach (var it in item)
            {
                results.Add(selector(it));
            }

            return results;
        }

        

        public static async Task  Main(string[] args)
        {

            //IServiceProvider serviceProvider;
            //IServiceCollection services = new ServiceCollection();
            //services.AddTransient<AppDbContext>();
            //services.AddTransient<AppErrorDescriber>();
            //services.AddTransient<StudentServices>();
            //serviceProvider = services.BuildServiceProvider();

            //var person = serviceProvider.GetService<StudentServices>();

            //await CreateStudents(person);

            //var c = new Employee()
            //{
            //    Name = "Viet",
            //};

            //c.Dispose();

            //if (c == null)
            //{
            //    Console.WriteLine("Giai phong thah cong");
            //}

            

            var list = new List<Person>()
            {
                new Person("T", 2, (float)2.221221),
                new Person("V", 4, (float)2.121221),
                new Person("C", 1, (float)2.421221),
                new Person("B", 2, (float)2.321221)
            };


            list.Sort(new Person());

            foreach (var item in list)
            {
                Console.WriteLine(item.Pice);
            }

            //AppStudent student = new AppStudent("Viet");
            //var st = new AppList();
            //st.Soft(student);
            
            Console.ReadKey();

        }
        

        public class Input : Person
        {
        }

        public interface IStudent
        {
            
            void Display();
        }

        public class Student : IStudent , IDisposable
        {
            public string Name { get; set; }

            private bool m_Disposed = false;

            private Person st;

            public Student()
            {
                st = new Person();
                
            }

            // Phương thức triển khai từ giao diện
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!m_Disposed)
                {
                    if (disposing)
                    {
                        // các đối tượng có Dispose gọi ở đây
                        st.Dispose();
                    }

                    // giải phóng các tài nguyên không quản lý được cửa lớp (unmanaged)
                    Console.WriteLine("Da Giai phong tai nguyen khong dc quan ly");

                    m_Disposed = true;
                }
            }

            public void Display()
            {
                throw new NotImplementedException();
            }

            ~Student()
            {
                Dispose(false);
            }


        }

        public class Person : IComparable<Person> , IComparer<Person>, IDisposable
        {
            public string Name { get; set; }

            public int Age { get; set; }

            public float Pice { get; set; }


            public Person() { }

            public Person(string name, int age, float pice)
            {
                Name = name;
                Age = age;
                Pice = pice;
            }

            public override string ToString()
            {
                return Name;
            }


            public int Compare([AllowNull] Person x, [AllowNull] Person y)
            {
                return x.Pice.CompareTo(y.Pice);
            }

            public int CompareTo([AllowNull] Person other)
            {
                return this.Age.CompareTo(other.Age);
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }

        public static async Task CreateStudents(StudentServices studentServices)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            
                var keyBoardTask = Task.Run(() =>
                {
                    Console.WriteLine("Muon dung hay bam emter");
                    Console.ReadKey();

                    // Cancel the task
                    cancellationTokenSource.Cancel();
                });


                try
                {

                    for (int i = 0; i < 1000000000; i++)
                    {

                        var create = await studentServices.Create(new Teacher()
                        {

                            Name = "Viet" + i
                        }, cancellationTokenSource.Token) ;

                        

                        Console.WriteLine(create);


                    }
                }

                catch (TaskCanceledException)
                {
                    Console.WriteLine("Task was cancelled");
                }

                catch (OperationCanceledException)
                {
                    Console.WriteLine("Da Dung");
                }

                await keyBoardTask;
            
        }



    }

   



}
