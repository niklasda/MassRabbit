using System;
using Topshelf;

namespace ConsoleService
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var ret = HostFactory.Run(x => x.Service<RequestService>());
            Console.WriteLine(ret);
        }
    }
}
