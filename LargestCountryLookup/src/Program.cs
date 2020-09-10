using LargestCountryLookup.src;
using System;
using System.IO;
using System.Net;

namespace LargestCountryLookup
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"Data\", @"PopByLargest20.csv");
            CountryHelper csvReader = new CountryHelper(path);
            ShowMenu(csvReader);
            //csvReader.PrintCountries();
            //CsvReader csvReader = new CsvReader(path);
            //csvReader.GetCountriesFromFile();
            //PrintnCountries(csvReader);
        }

        static void ShowMenu(CountryHelper reader)
        {
            Console.Clear();
            string choice = string.Empty;
            while (true)
            {
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1: Print All");
                Console.WriteLine("2: Print x amount");
                Console.WriteLine("3: Sort by population");
                Console.WriteLine("4: Sort by contient");
                Console.WriteLine("5: Sort alphabetically");
                Console.Write("\r\nSelect an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        reader.PrintCountries();
                        return ;
                    case "2":
                        reader.PrintnCountries();
                        return;
                    case "3":
                        reader.FilterCountries(CountryFilters.Population);
                        return;
                    case "4":
                        reader.FilterCountries(CountryFilters.Contient);
                        return;
                    case "5":
                        reader.FilterCountries(CountryFilters.Alphabetical);
                        return;
                    default:
                        Console.WriteLine("Please enter a number 1-5");
                        break;
                }
            }

        }
        
    }
}
