# Cron Expression Parser

### How to Run:

1. Clone Repository
   ```git clone https://github.com/your-username/CronExpParser.git```

3. Execute PowerShell Commands:
   Navigate to the directory containing the CronExpParser executable on your local machine. For example:
   cd CronExpParser\bin\Debug\net6.0

   Execute the following PowerShell command to run the CronExpParser with a sample cron expression: <br>
   ```.\CronExpParser.exe "*/15 0 1,15 * 1-5 /usr/bin/find"```

   Output:
   ```
   minute 0 15 30 45
   hour 0
   day of month 1 15
   month 1 2 3 4 5 6 7 8 9 10 11 12
   day of week 1 2 3 4 5
   command /usr/bin/find
   ```

Feel free to replace the sample cron expression with your own to see the parsed output.

### Dependencies:
This project is built using .NET 6.0. Ensure that you have the appropriate runtime installed on your machine.

