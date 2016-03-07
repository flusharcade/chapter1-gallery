// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GalleryItem.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Gallery.Shared
{
	using System;

	/// <summary>
	/// Gallery item.
	/// </summary>
	public class GalleryItem
	{
		/// <summary>
		/// The image data.
		/// </summary>
		public byte[] ImageData;

		/// <summary>
		/// The image URI.
		/// </summary>
		public string ImageUri;

		/// <summary>
		/// The title.
		/// </summary>
		public string Title;

		/// <summary>
		/// The date.
		/// </summary>
		public string Date;

		/// <summary>
		/// Initializes a new instance of the <see cref="Gallery.Shared.GalleryItem"/> class.
		/// </summary>
		public GalleryItem ()
		{
		}
	}
}

