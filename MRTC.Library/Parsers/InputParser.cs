using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MRTC.Library.Parsers
{
    /// <summary>
    /// Parser for system input
    /// </summary>
    public class InputParser : Parser<Input>
	{
        private readonly InputLinesParser inputLinesParser;
        private readonly PlateauParser plateauParser;
        private readonly RoverParser roverParser;
        private readonly CommandSetParser roverCommandSetParser;

        /// <summary>
        /// Creates InputParser that uses the default InputLinesParser, PlateauParser, RoverParser and RoverCommandSetParser
        /// </summary>
        public InputParser()
        {
            inputLinesParser = new InputLinesParser();
            plateauParser = new PlateauParser();
            roverParser = new RoverParser();
            roverCommandSetParser = new CommandSetParser();
        }

        /// <summary>
        /// Creates InputParser that uses the specified InputLinesParser, PlateauParser, RoverParser and RoverCommandSetParser
        /// </summary>
        /// <param name="inputLinesParser">InputLinesParser to be used</param>
        /// <param name="plateauParser">PlateauParser to be used</param>
        /// <param name="roverParser">RoverParser to be used</param>
        /// <param name="roverCommandSetParser">RoverCommandSetParser to be used</param>
        public InputParser(InputLinesParser inputLinesParser, PlateauParser plateauParser, RoverParser roverParser, CommandSetParser roverCommandSetParser)
        {
            this.inputLinesParser = inputLinesParser;
            this.plateauParser = plateauParser;
            this.roverParser = roverParser;
            this.roverCommandSetParser = roverCommandSetParser;
        }

        /// <summary>
        /// Parse from text
        /// </summary>
        /// <param name="text">Input text</param>
        /// <returns>Parsed result</returns>
        public override async Task<Input> ParseAsync(string text)
		{
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException(nameof(text));
            }

            try
            {
                Plateau plateau = null;
                InputLine currentLine = null;
                List<Rover> rovers = new List<Rover>();
                List<CommandSet> roverCommandSets = new List<CommandSet>();

                // parse lines

                var lines = await inputLinesParser.ParseAsync(text);
                var linesQueue = new Queue<InputLine>(lines);

                // parse plateau
                
                if (linesQueue.Any())
                {
                    try
                    {
                        currentLine = linesQueue.Dequeue();
                        plateau = await plateauParser.ParseAsync(currentLine.Text);
                    }
                    catch (Exception exception)
                    {
                        throw new PlateauParserException($"Expected valid plateau initialization at line {currentLine.Number} e.g. 5 5", exception);
                    }
                }

                // parse rovers and command sets

                while (linesQueue.Any())
                {
                    Rover rover;
                    CommandSet roverCommandSet;

                    // parse rover

                    try
                    {
                        currentLine = linesQueue.Dequeue();
                        rover = await roverParser.ParseAsync(currentLine.Text);
                    }
                    catch (Exception exception)
                    {
                        throw new RoverParserException($"Expected valid rover initialization at line {currentLine.Number} e.g. 1 2 N", exception);
                    }

                    rover.SetPlateau(plateau);
                    rovers.Add(rover);

                    // parse rover command set

                    if (linesQueue.Any())
                    {
                        try
                        {
                            currentLine = linesQueue.Dequeue();
                            roverCommandSet = await roverCommandSetParser.ParseAsync(currentLine.Text);
                        }
                        catch (Exception exception)
                        {
                            throw new CommandSetParserException($"Expected valid rover command set at line {currentLine.Number} e.g. LMLMLMLMM", exception);
                        }

                        roverCommandSet.SetRover(rover);
                        roverCommandSets.Add(roverCommandSet);
                    }
                }

                // create new input

                var input = new Input(plateau, rovers, roverCommandSets);

                // return new input

                return input;
            }
            catch (Exception exception)
            {
                throw new InputParserException("Failed to parse input", exception);
            }
        }
	}
}
