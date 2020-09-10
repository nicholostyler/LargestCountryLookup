using System.Text;
using CsvHelper.Configuration.Attributes;
using CsvHelper.TypeConversion;
namespace LargestCountryLookup.src
{
    public class Country
    {
        public string CountryName { get; private set; }
        public string CountryCode { get; private set; }
        public string Continent { get; private set; }
        public int Population { get; private set; }

        public Country(string CountryName, string CountryCode, string Continent, int Population)
        {
            this.CountryName = CountryName;
            this.CountryCode = CountryCode;
            this.Continent = Continent;
            this.Population = Population;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.Append($"{CountryName}: {Population} || {Continent}: {CountryCode}");

            return builder.ToString();
        }

    }
}