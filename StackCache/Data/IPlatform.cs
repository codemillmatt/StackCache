using System;

using SQLite.Net.Interop;

namespace StackCache
{
	public interface IPlatform 
	{
		string DBPath { get; }
		ISQLitePlatform OSPlatform { get; }
	}
}

