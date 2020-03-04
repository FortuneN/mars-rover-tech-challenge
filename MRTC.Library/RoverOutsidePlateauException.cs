using System;
using System.Runtime.Serialization;

namespace MRTC.Library
{
	[Serializable]
	public class RoverOutsidePlateauException : MRTCException
	{
		public RoverOutsidePlateauException(): base("You cannot drive a rover outside the plateau")
		{
		}

		public RoverOutsidePlateauException(string message) : base(message)
		{
		}

		public RoverOutsidePlateauException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected RoverOutsidePlateauException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}