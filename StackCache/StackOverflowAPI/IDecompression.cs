using System;
using System.IO;

namespace StackCache
{
	public interface IDecompression
	{
		Stream Decompress(Stream input);
	}
}

