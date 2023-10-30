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
            Console.WriteLine("Watching for new files");
            using (var publisher = new NewFileSavedMessagePublisher(@"C:\Users\mi.bentaiba\Pictures\Screenshots"))
            using (var subscriber = publisher.Subscribe(new NewFileSavedMessageSubscriber()))
            {
                Console.WriteLine("Press RETURN to exit");
                //wait for user RETURN 
                Console.ReadLine();
            }
        }
    }
}
