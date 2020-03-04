using System;
using System.IO;
using System.Threading.Tasks;

namespace MRTC.Library
{
    /// <summary>
    /// Abstract super class of all parsers
    /// </summary>
    /// <typeparam name="T">Type of the parsed result</typeparam>
    public abstract class Parser<T>
	{
        /// <summary>
        /// Parse from text
        /// </summary>
        /// <param name="text">Input text</param>
        /// <returns>Parsed result</returns>
        public abstract Task<T> ParseAsync(string text);

        /// <summary>
        /// Parse from stream
        /// </summary>
        /// <param name="stream">Input stream</param>
        /// <returns>Parsed result</returns>
        public async Task<T> ParseAsync(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            string text;

            using (var reader = new StreamReader(stream))
            {
                text = await reader.ReadToEndAsync();
            }

            return await ParseAsync(text);
        }

        /// <summary>
        /// Parse from path
        /// </summary>
        /// <param name="fileInfo">Input file</param>
        /// <returns>Parsed result</returns>
        public async Task<T> ParseAsync(FileInfo fileInfo)
        {
            if (fileInfo == null)
            {
                throw new ArgumentNullException(nameof(fileInfo));
            }

            using (var stream = File.OpenRead(fileInfo.FullName))
            {
                return await ParseAsync(stream);
            }
        }
    }
}
