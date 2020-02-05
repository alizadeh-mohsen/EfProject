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
            var p = context.Perishables.Distinct().ToList();
            p.ForEach(p => Console.WriteLine($"ExpirationDays= {p.ExpirationDays}" +
                                       $" Refrigerated={p.Refrigerated}" +
                                       $" Name={ p.ProductName} "));

            Console.ReadKey();


        }
    }
}
