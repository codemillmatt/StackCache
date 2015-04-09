using System;
using System.IO;

using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinAndroid;

namespace StackCache.Droid
{
	public class AndroidDataPlatform : IPlatform
	{
		public AndroidDataPlatform ()
		{			
		}

		public string DBPath {
			get {
				return Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "stack.db3");
			}
		}

		public ISQLitePlatform OSPlatform { 
			get {
				return new SQLitePlatformAndroid ();
			}		
		}
	}
}

