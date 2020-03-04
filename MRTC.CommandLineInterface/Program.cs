using Fclp;
using MRTC.Library;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MRTC.CommandLineInterface
{
    class Program
	{
        static readonly InputExecutor InputExecutor = new InputExecutor();

        static string inputFilePath;
        static bool help;

        private static readonly string[] Usage = new[]
        {
            Constants.CONSOLE_LINE,
            "Mars Rover Command Center :)",
            Constants.CONSOLE_LINE,
            Constants.RETURN,
            "-f, --file    Path of the input file",
            "-h, --help    Show this help message",
            Constants.RETURN,
            "Example file content:",
            Constants.RETURN,
            "5 5",
            "1 2 N",
            "LMLMLMLMM",
            "3 3 E",
            "MMRMMRMRRM"
        };

        static async Task Main(string[] args)
		{
            // parse args

            var parser = new FluentCommandLineParser();

            parser
            .Setup<string>('f', "file")
            .Callback(f => inputFilePath = f)
            .WithDescription(string.Join(Constants.LINE_BREAK_2, Usage));

            parser
            .Setup<bool>('h', "help")
            .Callback(h => help = h)
            .WithDescription(string.Join(Constants.LINE_BREAK_2, Usage));

            var parseResult = parser.Parse(args);

            if (!help && string.IsNullOrWhiteSpace(inputFilePath))
            {
                Print("Run -h/--help option to see usage");
                return;
            }

            if (help || string.IsNullOrWhiteSpace(inputFilePath))
            {
                Print(Usage);
                return;
            }

            // check file arg

            var inputFile = new FileInfo(inputFilePath);

            if (!inputFile.Exists)
            {
                Print($"Given file does not exist : {inputFilePath}");
                return;
            }

            // process file

            var outputText = await ProcessRequestAsync(inputFile);

            // print output

            Print(outputText);
        }

        static void Print(params string[] text)
        {
            Console.WriteLine(string.Join(Constants.LINE_BREAK_2, text));
        }

        static async Task<string> ProcessRequestAsync(FileInfo inputFile)
        {
            try
            {
                var output = await InputExecutor.ExecuteAsync(inputFile);

                if (!output.Rovers.Any())
                {
                    return "No rovers were specified in the input";
                }

                return string.Join(Constants.LINE_BREAK_2, output.Rovers.Select(r => $"{r.FinalXCoordinate} {r.FinalYCoordinate} {(char)r.FinalHeading}"));
            }
            catch (Exception exception)
            {
                return string.Join(Constants.LINE_BREAK_2, exception.Message, exception?.InnerException?.Message);
            }
        }
    }
}
