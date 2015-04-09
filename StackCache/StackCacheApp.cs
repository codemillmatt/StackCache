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
			MainPage = new NavigationPage (new QuestionListPage ());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

