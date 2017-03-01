// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainController.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Gallery.iOS
{
	using System;

	using UIKit;
	using CoreGraphics;

	/// <summary>
	/// Main controller.
	/// </summary>
	public partial class MainController : UIViewController
	{
		#region Private Properties

		/// <summary>
		/// The table.
		/// </summary>
		private UITableView _tableView;

		/// <summary>
		/// The source.
		/// </summary>
		private TableSource _source;

		/// <summary>
		/// The image handler.
		/// </summary>
		private ImageHandler _imageHandler;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Gallery.iOS.MainController"/> class.
		/// </summary>
		public MainController() : base("MainController", null)
		{
			_source = new TableSource();

			_source.ItemSelected += (sender, e) =>
			{
				var asset = _imageHandler.SynchronousGetAsset(e.Title);
				NavigationController.PushViewController(new PhotoController(asset), true);
			};

			_imageHandler = new ImageHandler();
			_imageHandler.AssetsLoaded += HandleAssetsLoaded;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Handles the assets loaded.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void HandleAssetsLoaded(object sender, EventArgs e)
		{
			_source.UpdateGalleryItems(_imageHandler.CreateGalleryItems());
			_tableView.ReloadData();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var width = View.Bounds.Width;
			var height = View.Bounds.Height;

			_tableView = new UITableView(new CGRect(0, 0, width, height));
			_tableView.AutoresizingMask = UIViewAutoresizing.All;
			_tableView.Source = _source;

			Add(_tableView);
		}

		#endregion
	}
}