// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GalleryCell.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Gallery.iOS 
{
	using Foundation;
	using UIKit;

	using Gallery.Shared;

	/// <summary>
	/// Gallery cell.
	/// </summary>
	public class GalleryCell: UITableViewCell  
	{
		#region Private Properties

		/// <summary>
		/// The image view.
		/// </summary>
		private UIImageView imageView;

		/// <summary>
		/// The title label.
		/// </summary>
		private UILabel titleLabel;

		/// <summary>
		/// The date label.
		/// </summary>
		private UILabel dateLabel;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Gallery.iOS.GalleryCell"/> class.
		/// </summary>
		/// <param name="cellId">Cell identifier.</param>
		public GalleryCell (string cellId) : base (UITableViewCellStyle.Default, cellId)
		{
			SelectionStyle = UITableViewCellSelectionStyle.Gray;

			imageView = new UIImageView()
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			titleLabel = new UILabel () 
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			dateLabel = new UILabel () 
			{
				TranslatesAutoresizingMaskIntoConstraints = false,
			};

			ContentView.Add (imageView);
			ContentView.Add (titleLabel);
			ContentView.Add (dateLabel);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Updates the cell.
		/// </summary>
		/// <returns>The cell.</returns>
		/// <param name="galleryItem">Gallery item.</param>
		public void UpdateCell (GalleryItem galleryItem)
		{
			imageView.Image = UIImage.LoadFromData (NSData.FromArray (galleryItem.ImageData));
			titleLabel.Text = galleryItem.Title;
			dateLabel.Text = galleryItem.Date;
		}

		/// <summary>
		/// Layouts the subviews.
		/// </summary>
		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			ContentView.TranslatesAutoresizingMaskIntoConstraints = false;

			// set layout constraints for main view
			AddConstraints (NSLayoutConstraint.FromVisualFormat("V:|[imageView(100)]|", NSLayoutFormatOptions.DirectionLeftToRight, null, new NSDictionary("imageView", imageView)));
			AddConstraints (NSLayoutConstraint.FromVisualFormat("V:|[titleLabel]|", NSLayoutFormatOptions.DirectionLeftToRight, null, new NSDictionary("titleLabel", titleLabel)));
			AddConstraints (NSLayoutConstraint.FromVisualFormat("H:|-10-[imageView(100)]-10-[titleLabel]-10-|", NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary ("imageView", imageView, "titleLabel", titleLabel)));
			AddConstraints (NSLayoutConstraint.FromVisualFormat("H:|-10-[imageView(100)]-10-[dateLabel]-10-|", NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary ("imageView", imageView, "dateLabel", dateLabel)));
		}

		#endregion
	}
}