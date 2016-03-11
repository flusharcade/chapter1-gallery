// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListAdapter.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Gallery.Droid
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.IO;

	using Android.App;
	using Android.Widget;
	using Android.Views;
	using Android.Graphics;

	using Gallery.Shared;

	/// <summary>
	/// List adapter.
	/// </summary>
	public class ListAdapter : BaseAdapter
	{
		private List<GalleryItem> items;
		private Activity context;

		public ListAdapter(Activity context) : base()
		{
			this.context = context;
			this.items = new List<GalleryItem>();

			foreach (var galleryitem in ImageHandler.GetFiles (this.context))
			{
				this.items.Add (galleryitem);
			}
		}
			
		public override Java.Lang.Object GetItem (int position)
		{
			return null;
		}

		public GalleryItem GetItemByPosition (int position)
		{
			return this.items[position];
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override int Count
		{
			get 
			{ 
				return items.Count; 
			} 
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			View view = convertView; // re-use an existing view, if one is available

			if (view == null)
			{ 
				// otherwise create a new one
				view = context.LayoutInflater.Inflate(Resource.Layout.CustomCell, null);
			}

			// set image
			var imageView = view.FindViewById<ImageView> (Resource.Id.image);
			BitmapHelpers.CreateBitmap (imageView, this.items [position].ImageData);

			// set labels
			var titleTextView = view.FindViewById<TextView> (Resource.Id.title);
			titleTextView.Text = this.items[position].Title;
			var dateTextView = view.FindViewById<TextView> (Resource.Id.date);
			dateTextView.Text = this.items[position].Date;

			return view;
		}
	}
}

