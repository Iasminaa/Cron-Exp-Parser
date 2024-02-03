using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronExpParser
{
    public class Parser
    {
        private const int HoursInADay = 24;
        private const int MinutesInAnHour = 60;
        private const int MonthsInAYear = 12;

        public Parser() { }
        private static string ParseSlashStar(string input, int unitOfTime, int offset) 
        {
            var result = new StringBuilder();

            bool isStar = input.Equals("*");
            bool startsWithSlashStar = input.StartsWith("*/");
  
            if (startsWithSlashStar)
            {
                var str = input.Split("*/");
                bool success = Int32.TryParse(str[1], out var time);
                if (success)
                {
                    var executeAmountOfTimes = unitOfTime / time;
                    var timeResult = 0;
                    for (int i = 0; i < executeAmountOfTimes; i++)
                    {
                        result.Append(timeResult).Append(' ');
                        timeResult += time;
                    }
                }
            }
            else if (isStar)
            {
                    for (int i = offset; i < unitOfTime + offset; i++)
                    {
                        result.Append(i).Append(' ');
                    } 
            }
            else
            {
                return input; 
            }
         
            return result.ToString().Trim(); 
        }
        public static string ParseMinutes(string input)
        {
            return ParseSlashStar(input, MinutesInAnHour, 1);
        }
        public static string ParseHours(string input) 
        {
            return ParseSlashStar(input, HoursInADay, 0);
        }
        public static string ParseDayOfMonth(string input) 
         {
            return String.Join(' ',input.Split(","));
        }
        public static string ParseMonth(string input)
        {
            return ParseSlashStar(input, MonthsInAYear, 1);
        }
        public static string ParseDayOfWeek(string input)
        {
            var str = input.Split("-");
            bool success = Int32.TryParse(str[0], out var start);
            bool success2 = Int32.TryParse(str[1], out var n);
            var result = new StringBuilder();
            if (success && success2)
            {
                for(int i = start; i <= n; i++)
                { 
                    result.Append(i).Append(' ');
                }
            }
            return result.ToString().Trim();
        }
    }
   
}
