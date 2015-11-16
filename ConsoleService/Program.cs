using System;
using ConsoleService.Services;
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
