using System;

namespace StackCache
{
	public class NoInternetException : Exception
	{
		public NoInternetException () : base ("Internet not reachable")
		{
		}
	}
}

