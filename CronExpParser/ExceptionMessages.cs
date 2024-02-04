using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CronExpParser
{
    public static class ExceptionMessages
    {
        public static readonly string NoArgsProvided = "Cron expression missing. Nothing to be parsed. Please provide a valid expression in the arguments.";

        public static string InvalidLengthCronExp(int value)
        {
            return $"Cron expression is invalid. Expected length of expression: 6, actual length was: {value} Ensure your expression contains minute, hour, day of month, month, and day of week";
        }
    }

}
