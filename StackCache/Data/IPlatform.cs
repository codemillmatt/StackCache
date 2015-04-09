using System;

using SQLite.Net.Interop;

namespace StackCache
{
	public interface IPlatform 
	{
		string DBPath { get; set; }
		ISQLitePlatform OSPlatform { get; set; }
	}
}

