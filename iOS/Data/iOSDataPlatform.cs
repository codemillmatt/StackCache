using System;
using System.IO;

using SQLite.Net.Platform.XamarinIOS;
using SQLite.Net.Interop;

namespace StackCache.iOS
{
	public class iOSDataPlatform : IPlatform
	{
		public iOSDataPlatform ()
		{
		}

		public string DBPath {
			get {
				return Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "stack.db3");
			}
		}

		public ISQLitePlatform OSPlatform { 
			get {
				return new SQLitePlatformIOS ();
			}		
		}
	}
}

