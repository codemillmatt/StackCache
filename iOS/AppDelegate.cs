using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace StackCache.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			App.PlatformDecompression = new DecompressIOS ();
			App.SetPlatform (new iOSDataPlatform ());
			App.StackDataManager.Database.SetupDatabaseAsync();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}

