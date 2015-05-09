using System;

using Xamarin.Forms;

namespace StackCache
{
	public class App : Application
	{
		public static IDecompression PlatformDecompression {
			get;
			set;
		}

		static StackDataManager _dataMgr;

		public static void SetPlatform(IPlatform osPlatform)
		{
			_dataMgr = new StackDataManager (osPlatform);
		}

		public static StackDataManager StackDataManager 
		{
			get { 
				return _dataMgr;
			}
		}

		public App ()
		{
			// The root page of your application
			MainPage = new NavigationPage (new QuestionDateListPage ());
		}
			
	}
}

