using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //this is the message observable responsible of producing messages 
            using (var observer = new ConsoleIntegerProducer())
            //those are the message observer that consume messages 
            using (var consumer1 = observer.Subscribe(new IntegerConsumer(2)))
            using (var consumer2 = observer.Subscribe(new IntegerConsumer(3)))
            {
                using (var consumer3 = observer.Subscribe(new IntegerConsumer(5)))
                {
                    //internal lifecycle 
                }

                observer.Wait();
            }

            Console.WriteLine("END");
            Console.ReadLine();
        }
    }
}
