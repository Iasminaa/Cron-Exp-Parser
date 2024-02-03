using CronExpParser;
using System;
using System.Text;

class Program
{
    static void Main()
    {
        var result = new List<string>(); 
        var cmd = "*/15 0 1,15 * 1-5 /usr/bin/find"; //Console.ReadLine
        //var cmd = "*/15 * 1,15 * 1-5 /usr/bin/find";
        var cmds = cmd.Split();

        result.Add(Parser.ParseMinutes(cmds[0]));
        result.Add(Parser.ParseHours(cmds[1]));
        result.Add(Parser.ParseDayOfMonth(cmds[2]));
        result.Add(Parser.ParseMonth(cmds[3]));
        result.Add(Parser.ParseDayOfWeek(cmds[4]));
        result.Add(cmds[5]);

        /* ==== Output ==== */
        var header = new string[] { "minute", "hour", "day of month", "month", "day of week", "command" }; 
        string format = "{0,-20} {1,-20}" + Environment.NewLine;
        var stringBuilder = new StringBuilder();
        for (int i = 0; i < result.Count(); i++)
        {
            stringBuilder.AppendFormat(format, header[i], result[i]);
        }

        Console.WriteLine(stringBuilder.ToString());

    }
}
       