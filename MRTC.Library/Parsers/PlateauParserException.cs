using System;
using System.Runtime.Serialization;

namespace MRTC.Library.Parsers
{
	[Serializable]
	public class PlateauParserException : MRTCException
	{
		public PlateauParserException()
		{
		}

		public PlateauParserException(string message) : base(message)
		{
		}

		public PlateauParserException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected PlateauParserException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}