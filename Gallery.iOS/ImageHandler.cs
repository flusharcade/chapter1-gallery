// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImageHandler.cs" company="Flush Arcade Pty Ltd.">
//   Copyright (c) 2015 Flush Arcade Pty Ltd. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Gallery.iOS
{
	using System;
	using System.Threading;
	using System.Collections.Generic;
	using System.Linq;

	using UIKit;
	using AssetsLibrary;
	using Foundation;

	using Gallery.Shared;

	/// <summary>
	/// Image handler.
	/// </summary>
	public class ImageHandler
	{
		#region Events

		/// <summary>
		/// Occurs when assets loaded.
		/// </summary>
		public event EventHandler AssetsLoaded;

		#endregion

		#region Private Properties

		/// <summary>
		/// The asset library.
		/// </summary>
		private readonly ALAssetsLibrary _assetLibrary;

		/// <summary>
		/// The assets.
		/// </summary>
		private	IList<string> _assets;

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Gallery.iOS.ImageHandler"/> class.
		/// </summary>
		public ImageHandler ()
		{
			_assetLibrary = new ALAssetsLibrary();
			_assets = new List<string> ();
			_assetLibrary.Enumerate(ALAssetsGroupType.SavedPhotos, GroupEnumerator, Console.WriteLine);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Groups the enumerator.
		/// </summary>
		/// <param name="assetGroup">Asset group.</param>
		/// <param name="shouldStop">Should stop.</param>
		private void GroupEnumerator(ALAssetsGroup assetGroup, ref bool shouldStop)
		{
			if (assetGroup == null)
			{
				shouldStop = true;
				notifyAssetsLoaded ();

				return;
			}

			if (!shouldStop)
			{
				assetGroup.Enumerate(AssetEnumerator);
				shouldStop = false;
			}
		}

		/// <summary>
		/// Assets the enumerator.
		/// </summary>
		/// <param name="asset">Asset.</param>
		/// <param name="index">Index.</param>
		/// <param name="shouldStop">Should stop.</param>
		private void AssetEnumerator(ALAsset asset, nint index, ref bool shouldStop)
		{
			if (asset == null)
			{
				shouldStop = true;
				return;
			}

			if (!shouldStop)
			{
				// add asset name to list
				_assets.Add (asset.AssetUrl.ToString());
				shouldStop = false;
			}
		}

		/// <summary>
		/// Notifies the assets ready.
		/// </summary>
		private void notifyAssetsLoaded()
		{
			if (AssetsLoaded != null) 
			{
				AssetsLoaded (this, EventArgs.Empty);
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Gets the files.
		/// </summary>
		/// <returns>The files.</returns>
		public IEnumerable<GalleryItem> CreateGalleryItems()
		{
			foreach (var file in _assets.Take(100)) 
			{
				using (var asset = SynchronousGetAsset (file))
				{
					if (asset != null) 
					{
						var thumbnail = asset.Thumbnail;
						var image = UIImage.FromImage (thumbnail);
						var jpegData = image.AsJPEG ().ToArray ();

						yield return new GalleryItem () 
						{
							Title = file,
							Date = asset.Date.ToString(),
							ImageData = jpegData,
							ImageUri = asset.AssetUrl.ToString ()
						};
					}
				}
			}
		}

		/// <summary>
		/// Synchronouses the get asset.
		/// </summary>
		/// <returns>The get asset.</returns>
		/// <param name="filename">Filename.</param>
		public ALAsset SynchronousGetAsset(string filename)
		{
			ManualResetEvent waiter = new ManualResetEvent(false);
			NSError error = null;
			ALAsset result = null;
			Exception exception; 

			ThreadPool.QueueUserWorkItem ((object state) => _assetLibrary.AssetForUrl (new NSUrl (filename), (ALAsset asset) => 
				{
					result = asset;
					waiter.Set ();
				}, 
				e => 
				{
					error = e;
					waiter.Set ();
				}));


			if(!waiter.WaitOne (TimeSpan.FromSeconds (10)))
				throw new Exception("Error Getting Asset : Timeout, Asset=" + filename);

			if (error != null)
				throw new Exception (error.Description);

			return result;
		}

		#endregion
	}
}