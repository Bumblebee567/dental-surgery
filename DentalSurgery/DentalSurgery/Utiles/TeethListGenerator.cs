using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DentalSurgery.Utiles
{
    public class TeethListGenerator
    {
        public static List<string> GenerateListOfTeeth()
        {
            List<string> teethList = new List<string>();
            for (int i = 0; i <= 8; i++)
            {
                teethList.Add($"{i}LG");
                teethList.Add($"{i}PG");
                teethList.Add($"{i}LD");
                teethList.Add($"{i}PD");
            }
            return teethList;
        }
    }
}