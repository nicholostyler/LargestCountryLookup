using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using CsvHelper;

namespace LargestCountryLookup.src
{
    enum CountryFilters
    { 
        Alphabetical,
        Population,
        Contient
    }

    // Using CsvHelper 
    class CountryHelper
    {
        public List<Country> Records { get; set; }
        public CountryHelper(string fileLocation)
        {
            // Using CsvHelper instead of my own implementation
            using (var reader = new StreamReader(fileLocation))
            using (var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture))
            {
                Console.WriteLine($"Reading countries from {fileLocation}...");
                try
                {
                    csv.Configuration.HasHeaderRecord = false;
                    csv.Read();
                    Records = csv.GetRecords<Country>().ToList();
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine($"Success! Found {Records.Count} countries in the file.");
            }
        }

        public void FilterCountries(int quantity, CountryFilters filter)
        {
            IEnumerable<Country> countries = null;

            switch (filter)
            {
                case CountryFilters.Alphabetical:
                    countries = Records.Take(quantity).OrderBy(x => x.CountryName);
                    break;
                case CountryFilters.Population:
                    //countries = Records.Take(quantity).OrderByDescending(x => x.Population);
                    countries =
                        from country in Records.Take(quantity)
                        orderby country.Population descending
                        select country;
                    break;
                case CountryFilters.Contient:
                    countries =
                        from country in Records
                        where country.Continent.Equals("Asia")
                        select country;
                    break;
            }

            if (countries == null) return;
            PrintCountries(countries);
        }

        public void FilterCountries(CountryFilters filter)
        {
            IEnumerable<Country> countries = null;

            switch (filter)
            {
                case CountryFilters.Alphabetical:
                    countries = Records.OrderBy(x => x.CountryName);
                    break;
                case CountryFilters.Population:
                    //countries = Records.Take(quantity).OrderByDescending(x => x.Population);
                    countries =
                        from country in Records
                        orderby country.Population descending
                        select country;
                    break;
                case CountryFilters.Contient:
                    countries =
                        from country in Records
                        where country.Continent.Equals("Asia")
                        select country;
                    break;
            }

            if (countries == null) return;
            PrintCountries(countries);
        }

        public void PrintCountries()
        {
            Console.WriteLine($"Showing {Records.Count} countries.");
            foreach (Country country in Records)
            {
                Console.WriteLine(country.ToString());
            }
        }

        public void PrintCountries(IEnumerable<Country> countries)
        {
            Console.WriteLine($"Showing {countries.Count()} sorted countries");
            foreach (Country country in countries)
            {
                Console.WriteLine(country.ToString());
            }
        }

        public void PrintnCountries()
        {
            Console.Write("Enter number of countries to display: ");
            // inputIsInt will be true if tryparse returns true
            bool inputIsInt = int.TryParse(Console.ReadLine(), out int userInput);
            // if InputIsInt == false or userInput is less than 0.
            if (!inputIsInt || userInput <= 0)
            {
                Console.WriteLine("You must type in a valid number. Exiting");
                return;
            }

            int maxToDisplay = userInput;
            for (int i = 0; i < maxToDisplay; i++)
            {
                Country selectedCountry = Records[i];
                Console.WriteLine(selectedCountry.ToString());
            }
        }
    }
    //class CsvReader
    //{
    //    public string FileLocation { get; set; }
    //    public List<Country> Countries { get; set; }

    //    public CsvReader(string fileLocation)
    //    {
    //        FileLocation = fileLocation;
    //        Countries = new List<Country>();
    //    }

    //    public void GetCountriesFromFile()
    //    {
    //        Console.WriteLine("Reading csv file...");
    //        // use a stream reader to read a file.
    //        using (StreamReader reader = new StreamReader(FileLocation))
    //        {
    //            // Read header file so it doesn't end up being fed
    //            reader.ReadLine();

    //            // Loop through each line in csv file
    //            while(!reader.EndOfStream)
    //            { 
    //                string line = reader.ReadLine();
    //                Country nextCountry = ReadCountryFromLine(line);
    //                Countries.Add(nextCountry);
    //            }
    //        }

    //        Console.WriteLine($"Read file! Found {Countries.Count} instances.");
    //    }

    //    private Country ReadCountryFromLine(string csvLine)
    //    {
    //        Country selectedCountry;
    //        string[] fixedline = new string[2];
    //        string[] parts;
    //        string name = string.Empty;

    //        if (csvLine.Contains("\""))
    //        {
    //            fixedline = CountryBetweenQuotes(csvLine);
    //        }
    //        // Split line by delimeter ","
    //        if (fixedline[0] != null)
    //        {
    //            parts = fixedline[1].Split(',');
    //            name = fixedline[0];
    //            fixedline = null;
    //        }
    //        else
    //            parts = csvLine.Split(',');

    //        Console.WriteLine();
    //        // sort array into Country object properties
    //        if (name == string.Empty)
    //            name = parts[0];
    //        string code = parts[1];
    //        string region = parts[2];
    //        long population = long.Parse(parts[3]);

    //        selectedCountry = new Country(name, code, region, population);
    //        return selectedCountry;
    //    }


    //    private string[] CountryBetweenQuotes(string line)
    //    {
    //        string country = "";
    //        string[] fixedLine = new string[2];

    //        Regex regex = new Regex("\"(.*?)\"");
    //        var matches = regex.Matches(line);

    //        if (matches.Count == 1)
    //        {
    //            country = matches[0].ToString();
    //            var withoutQuotes = country.Replace("\"", string.Empty);
    //            fixedLine[0] = withoutQuotes;
    //            line = line.Replace(country, "");
    //            fixedLine[1] = line;
    //        }

    //        return fixedLine;

    //    }
    //}
}
