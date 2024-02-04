using CronExpParser;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using System.Security.Cryptography;

namespace CronExpParserTests
{
    [TestFixture]
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }

        [TestCaseSource(nameof(TestCasesHappyPath))]
        public void CronExpParserIntegrationTest(string cmd, [Values] string expectedResult)
        {
            string[] args = new string[1];
            args[0] = cmd;

            var console = new System.IO.StringWriter();
            Console.SetOut(console);

            Program.Main(args);

            var outputLines = console.ToString().Trim().Split('\n');
            Assert.Multiple(() =>
            {
                Assert.That(console.ToString(), Is.EqualTo(expectedResult));

                Assert.That(outputLines, Has.Length.EqualTo(6));
            });
        }

        private static IEnumerable<TestCaseData> TestCasesHappyPath()
        {
            const string allStarExpectedResult = @"minute                    0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30 31 32 33 34 35 36 37 38 39 40 41 42 43 44 45 46 47 48 49 50 51 52 53 54 55 56 57 58 59
hour                      0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23
day of month              1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 19 20 21 22 23 24 25 26 27 28 29 30
month                     1 2 3 4 5 6 7 8 9 10 11 12
day of week               0 1 2 3 4 5 6
command                   /usr/bin/find
";

            const string providedExampleResult = @"minute                    0 15 30 45
hour                      0
day of month              1 15
month                     1 2 3 4 5 6 7 8 9 10 11 12
day of week               1 2 3 4 5
command                   /usr/bin/find
";
            const string stepValuesExpectedResult = @"minute                    0 2 4 6 8 10 12 14 16 18 20 22 24 26 28 30 32 34 36 38 40 42 44 46 48 50 52 54 56 58
hour                      0 3 6 9 12 15 18 21
day of month              1 15
month                     1 5 9
day of week               2 3 4 5 6
command                   /usr/bin/find
";
            const string rangeAndStepExpectedResult = @"minute                    0 15 30 45
hour                      3 5 7 9 11 13 15 17
day of month              1 3 5 7 9
month                     1 2 3 4 5 6 7 8 9 10 11 12
day of week               1 5
command                   /usr/bin/find
";


            yield return new TestCaseData("*/15 0 1,15 * 1-5 /usr/bin/find", providedExampleResult).SetName("Happy Path").SetDescription("Example Given");
            yield return new TestCaseData("* * * * * /usr/bin/find", allStarExpectedResult).SetName("Expression Test - All Stars").SetDescription("All fields are set to *");
            yield return new TestCaseData("*/2 */3 1,15 */4 2-6 /usr/bin/find", stepValuesExpectedResult).SetName("Expression Test - Step Values").SetDescription("Step values in various fields");
            yield return new TestCaseData("*/15 3-18/2 1-10/2 * 1,5 /usr/bin/find", rangeAndStepExpectedResult).SetName("Expression Test - Range and Step").SetDescription("Combination of range and step values");
        }

        [TestCase("15 4,8,12,16 * * /usr/bin/find", TestName = "Expected Invalid Length Cron Expression", Description = "Expected length of expression: 6, actual length was 5")]
        public void CronExpParserValidateExceptions(params string[] args)
        {
            var console = new System.IO.StringWriter();
            Console.SetOut(console);

            var ex = Assert.Throws<ArgumentException>(() => Program.Main(args));
            Assert.That(ex.Message, Is.EqualTo(ExceptionMessages.InvalidLengthCronExp(args.Length)));
        }
    }
}