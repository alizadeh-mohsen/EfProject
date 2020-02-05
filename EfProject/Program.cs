using EfProject.Models;
using System;
using System.Linq;

namespace EfProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new HPlusSportsContext();
            var salesPersons = context.Salesperson.Where(s => s.LastName.StartsWith("s"));
            salesPersons.ToList().ForEach(s => Console.WriteLine(s.FullName));

            Console.ReadKey();


        }
    }
}
