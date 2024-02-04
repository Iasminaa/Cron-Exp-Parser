using System.Text;

namespace CronExpParser
{
    public class Parser
    {
        private const int HoursInADay = 24;
        private const int MinutesInAnHour = 60;
        private const int MonthsInAYear = 12;
        private const int DaysInAMonth = 30;
        private const int DaysInAWeek = 7;

        public Parser() { }

        public static bool IsStar(string input) => input.Equals("*");
        private static bool IsSlashStar(string input) => input.StartsWith("*/");
        private static bool IsCommaSeparated(string input) => input.Contains(",");
        private static bool IsRange(string input) => input.Contains("-");

        private static string ParseRange(string input)
        {
            var str = input.Split("-");
            bool success = Int32.TryParse(str[0], out var start);
            var step = 1; 

            var result = new StringBuilder();
            if (success)
            {
                var stepDef = str[1].Split("/");
                _ = Int32.TryParse(stepDef[0], out var n);

                if (stepDef.Length > 1)
                {
                   _ = Int32.TryParse(stepDef[1], out step);
                }

                for (int i = start; i <= n; i=i+step)
                {
                    result.Append(i).Append(' ');
                }
            }
            return result.ToString().Trim();
        }
        private static string ParseCommaSeparated(string input) => String.Join(' ', input.Split(","));
        private static string ParseStar(string input, int unitOfTime, int offset)
        {
            var result = new StringBuilder();
    
            for (int i = offset; i < unitOfTime + offset; i++)
            {
                result.Append(i).Append(' ');
            }
            
            return result.ToString().Trim(); 
        }
        private static string ParseSlashStar(string input, int unitOfTime, int offset) 
        {
            var result = new StringBuilder();

            var str = input.Split("*/");
            bool success = Int32.TryParse(str[1], out var time);
            if (success)
            {
                var executeAmountOfTimes = unitOfTime / time;
                var timeResult = offset;
                for (int i = 0; i < executeAmountOfTimes; i++)
                {
                    result.Append(timeResult).Append(' ');
                    timeResult += time;
                }
            }
          
            return result.ToString().Trim(); 
        }

        private static string ParseTime(string input, int unitOfTime, int offset)
        {
            if (IsStar(input))
            {
                return ParseStar(input, unitOfTime, offset);
            }
            else if (IsSlashStar(input))
            {
                return ParseSlashStar(input, unitOfTime, offset);
            }
            else if (IsCommaSeparated(input))
            {
                return ParseCommaSeparated(input); 
            }
            else if (IsRange(input))
            {
                return ParseRange(input);
            }
            return input; 
        }

        public static string ParseMinutes(string input)
        {
            return ParseTime(input, MinutesInAnHour, 0);
        }
        public static string ParseHours(string input) 
        {
            return ParseTime(input, HoursInADay, 0);
        }
        public static string ParseDayOfMonth(string input) 
        {
            return ParseTime(input, DaysInAMonth, 1);    
        }
        public static string ParseMonth(string input)
        {
            return ParseTime(input, MonthsInAYear, 1);
        }
        public static string ParseDayOfWeek(string input)
        {
            return ParseTime(input, DaysInAWeek, 0);
        }
    }
   
}
