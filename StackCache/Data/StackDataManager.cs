using System;
using SQLite.Net;

namespace StackCache
{
	public class StackDataManager
	{
		private IPlatform _platform;
		private StackDBConnection _connection;

		public StackDataManager (IPlatform osPlatform)
		{
			_platform = osPlatform;
		}

		public StackDBConnection Database {
			get {
				if (_connection == null) {
					_connection = new StackDBConnection (() => 
						new SQLiteConnectionWithLock (_platform.OSPlatform,
						new SQLiteConnectionString (_platform.DBPath, true)));
				}

				return _connection;
			}
		}
	}
}

