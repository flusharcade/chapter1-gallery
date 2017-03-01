// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableSource.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Gallery.iOS 
{
	using System;
	using System.Collections.Generic;

	using UIKit;
	using Foundation;

	using Gallery.Shared;

	/// <summary>
	/// Table source.
	/// </summary>
	public class TableSource : UITableViewSource 
	{
		#region Events

		/// <summary>
		/// Occurs when row selected.
		/// </summary>
		public event EventHandler<GalleryItem> ItemSelected;

		#endregion

		#region Private Properties

		/// <summary>
		/// The gallery items.
		/// </summary>
		private List<GalleryItem> _galleryItems;

		#endregion

		#region Protected Properties

		/// <summary>
		/// The cell identifier.
		/// </summary>
		protected string CellIdentifier = "GalleryCell";

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Gallery.iOS.TableSource"/> class.
		/// </summary>
		public TableSource ()
		{
			_galleryItems = new List<GalleryItem> ();
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Updates the gallery items.
		/// </summary>
		public void UpdateGalleryItems(IEnumerable<GalleryItem> galleryList)
		{
			foreach (var galleryItem in galleryList)
			{
				_galleryItems.Add (galleryItem);
			}
		}

		/// <summary>
		/// Numbers the of sections.
		/// </summary>
		/// <returns>The of sections.</returns>
		/// <param name="tableView">Table view.</param>
		public override nint NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		/// <summary>
		/// Called by the TableView to determine how many cells to create for that particular section.
		/// </summary>
		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return _galleryItems.Count;
		}

		/// <summary>
		/// Called when a row is touched
		/// </summary>
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			if (ItemSelected != null)
			{
				ItemSelected (this, _galleryItems[indexPath.Row]);
			}

			tableView.DeselectRow (indexPath, true);
		}

		/// <summary>
		/// Gets the height for row.
		/// </summary>
		/// <returns>The height for row.</returns>
		/// <param name="tableView">Table view.</param>
		/// <param name="indexPath">Index path.</param>
		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 100;
		}

		/// <summary>
		/// Called by the TableView to get the actual UITableViewCell to render for the particular row
		/// </summary>
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = (GalleryCell)tableView.DequeueReusableCell (CellIdentifier);
			var galleryItem = _galleryItems[indexPath.Row];

			if (cell == null)
			{ 
				// we create a new cell if this row has not been created yet
				cell = new GalleryCell (CellIdentifier); 
			}

			cell.UpdateCell (galleryItem);

			return cell;
		}

		#endregion
	}
}