using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace LargestCountryLookup.src
{
    class CountryMap : ClassMap<Country>
    {
        public CountryMap()
        {
            Map(m => m.CountryName);
            Map(m => m.CountryCode);
            Map(m => m.Continent);
            Map(m => m.Population).TypeConverter<CsvHelper.TypeConversion.Int32Converter>();
        }
    }
}
