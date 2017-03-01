// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppDelegate.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Gallery.iOS
{
	using Foundation;
	using UIKit;

	/// <summary>
	/// App delegate.
	/// </summary>
	[Register ("AppDelegate")]
	public class AppDelegate : UIApplicationDelegate
	{
		#region Private Properties

		/// <summary>
		/// The window.
		/// </summary>
		private UIWindow _window;

		#endregion

		#region Public Methods

		/// <summary>
		/// Finisheds the launching.
		/// </summary>
		/// <returns><c>true</c>, if launching was finisheded, <c>false</c> otherwise.</returns>
		/// <param name="application">Application.</param>
		/// <param name="launchOptions">Launch options.</param>
		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			_window = new UIWindow (UIScreen.MainScreen.Bounds);

			MainController mainController = new MainController();

			var rootNavigationController = new UINavigationController();
			rootNavigationController.PushViewController(mainController, false);

			_window.RootViewController = rootNavigationController;
			_window.MakeKeyAndVisible ();

			return true;
		}

		#endregion
	}
}