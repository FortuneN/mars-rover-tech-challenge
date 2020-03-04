using System;
using System.Runtime.Serialization;

namespace MRTC.Library
{
	[Serializable]
	public class MRTCException: ApplicationException
	{
		public MRTCException()
		{
		}

		public MRTCException(string message) : base(message)
		{
		}

		public MRTCException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected MRTCException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
