using System.Text;

namespace CronExpParser
{
    public class Program
    {
        public static void Print(List<string> result)
        {
            /* ==== Output ==== */
            var header = new string[] { "minute", "hour", "day of month", "month", "day of week", "command" };
            var format = "{0,-25} {1,-25}" + Environment.NewLine;
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < result.Count; i++)
            {
                stringBuilder.AppendFormat(format, header[i], result[i]);
            }

            Console.WriteLine(stringBuilder.ToString());
        }

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException(ExceptionMessages.NoArgsProvided);
            }

            var result = new List<string>();
            var cmds = args[0].Split();

            if(cmds.Length < 6) 
            {
                throw new ArgumentException(ExceptionMessages.InvalidLengthCronExp(args.Length));
            }

            result.Add(Parser.ParseMinutes(cmds[0]));
            result.Add(Parser.ParseHours(cmds[1]));
            result.Add(Parser.ParseDayOfMonth(cmds[2]));
            result.Add(Parser.ParseMonth(cmds[3]));
            result.Add(Parser.ParseDayOfWeek(cmds[4]));
            result.Add(cmds[5]);

            Print(result); 
        }
    }
}