﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainActivity.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Gallery.Droid
{
	using Android.App;
	using Android.Widget;
	using Android.OS;

	/// <summary>
	/// Main activity.
	/// </summary>
	[Activity (Label = "Gallery.Droid", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		/// <summary>
		/// The adapter.
		/// </summary>
		private ListAdapter adapter;

		/// <summary>
		/// Raises the create event.
		/// </summary>
		/// <param name="savedInstanceState">Saved instance state.</param>
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			this.adapter = new ListAdapter (this);

			var listView = this.FindViewById<ListView> (Resource.Id.listView);
			listView.Adapter = adapter;
		}
	}
}