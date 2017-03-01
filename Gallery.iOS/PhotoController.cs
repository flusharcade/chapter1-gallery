// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PhotoController.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Gallery.iOS
{
	using UIKit;
	using Foundation;
	using AssetsLibrary;

	/// <summary>
	/// Main controller.
	/// </summary>
	public partial class PhotoController : UIViewController
	{
		#region Private Properties

		/// <summary>
		/// The image view.
		/// </summary>
		private UIImageView _imageView;

		/// <summary>
		/// The title label.
		/// </summary>
		private UILabel _titleLabel;

		/// <summary>
		/// The date label.
		/// </summary>
		private UILabel _dateLabel;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Gallery.iOS.PhotoController"/> class.
		/// </summary>
		public PhotoController(ALAsset asset) : base("PhotoController", null)
		{
			_imageView = new UIImageView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
				ContentMode = UIViewContentMode.ScaleAspectFit
			};

			_titleLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			_dateLabel = new UILabel()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			_imageView.Image = new UIImage(asset.DefaultRepresentation.GetFullScreenImage());
			_titleLabel.Text = asset.DefaultRepresentation.Filename;
			_dateLabel.Text = asset.Date.ToString();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Views the did load.
		/// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			View.Add(_imageView);
			View.Add(_titleLabel);
			View.Add(_dateLabel);

			// set layout constraints for main view
			View.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|[imageView]-10-[titleLabel(50)]-10-[dateLabel(50)]|", NSLayoutFormatOptions.DirectionLeftToRight, null, new NSDictionary("imageView", _imageView, "titleLabel", _titleLabel, "dateLabel", _dateLabel)));

			View.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|[imageView]|", NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary("imageView", _imageView)));
			View.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|[titleLabel]|", NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary("titleLabel", _titleLabel)));
			View.AddConstraints(NSLayoutConstraint.FromVisualFormat("H:|[dateLabel]|", NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary("dateLabel", _dateLabel)));
		}

		#endregion
	}
}