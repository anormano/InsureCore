using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsureCore.Module.CodeLibrary
{
    public static class CommonLibrary
    {
        public static int CalculateAge(DateTime startDate, DateTime endDate)
        {
            var age = endDate.Year - startDate.Year;
            if (startDate > endDate.AddYears(-age)) age--;
            return age;
        }

        public static string CalculateAgeString(DateTime startDate, DateTime endDate)
        {
            int months = endDate.Month - startDate.Month;
            int years = endDate.Year - startDate.Year;

            if (endDate.Day < startDate.Day)
            {
                months--;
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            int days = (endDate - startDate.AddMonths((years * 12) + months)).Days;

            return string.Format("{0} year{1}, {2} month{3} and {4} day{5}",
                                 years, (years == 1) ? "" : "s",
                                 months, (months == 1) ? "" : "s",
                                 days, (days == 1) ? "" : "s");
        }
    }
}
