using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronExpParser
{
    public class Parser
    {
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
                        result.Append(timeResult);
                        result.Append(' ');
                        timeResult += time;
                    }
                }
            }
            else if (isStar)
            {
                    for (int i = offset; i < unitOfTime + offset; i++)
                    {
                        result.Append(i);
                        result.Append(' ');
                    } 
            }
            else
            {
                return input; 
            }
         
            return result.ToString(); 
        }
        public static string ParseMinutes(string input)
        {
            return ParseSlashStar(input, 60, 1);
        }
        public static string ParseHours(string input) 
        {
            return ParseSlashStar(input, 24, 0);
        }
        public static string ParseDayOfMonth(string input) 
         {
            return String.Join(' ',input.Split(","));
        }
        public static string ParseMonth(string input)
        {
            return ParseSlashStar(input, 12, 1);
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
                        result.Append(i);
                        result.Append(' ');
                }
            }
            return result.ToString();
        }
    }
   
}
