using System;
using System.Globalization;
using UIKit;
using System.IO;
using System.Runtime.InteropServices;
using Foundation;
using Homepwner.Xamarin.iOS.Infrastructure.HTTP;
using Refit;

namespace Homepwner.Xamarin.iOS
{
	public partial class ItemDetailViewController : UIViewController
	{
		private UIImagePickerController _imagePickerController;
		private Item _item;
        private IHomepwnerApi _homepwnerApi => RestService.For<IHomepwnerApi>("http://homepwnertestapi.ngrok.io");

        public ItemDetailViewController(IntPtr handle) : base(handle)
		{
		}

		public ItemDetailViewController(Item item)
		{
			_item = item; 
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Save);
			doneButton.Clicked += DoneButton_Clicked;

			NavigationItem.SetRightBarButtonItem(doneButton, false);

			if (!UIImagePickerController.IsSourceTypeAvailable(UIImagePickerControllerSourceType.Camera))
			{
				return;
			}

			_imagePickerController = new UIImagePickerController
			{
				SourceType = UIImagePickerControllerSourceType.Camera,
			};

			_imagePickerController.FinishedPickingMedia += ImagePickerController_FinishedPickingMedia;
			_imagePickerController.Canceled += ImagePickerController_Canceled;

			CameraButton.Clicked += CameraButton_Clicked;
		}

		public override async void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			if (_item == null)
			{
				return;
			}

			NameText.Text = _item.Name;
			SerialText.Text = _item.SerialNumber;
			ValueText.Text = _item.Value.ToString(CultureInfo.InvariantCulture);

		    try
		    {
                var httpResponse = await _homepwnerApi.GetItemImage(_item.Id);
		        var image = httpResponse.ReadAsByteArrayAsync().Result;
                ItemImage.Image = new UIImage(NSData.FromArray(image));
            }
		    catch (Exception ex) 
		    {
                var alert = UIAlertController.Create("Network Error",
                    "There was a problem making the network request. Please try again.", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, null));
                PresentViewController(alert, true, null);
            }
		    
		}

	    private async void DoneButton_Clicked(object sender, EventArgs e)
		{
		    try
		    {
		        byte[] image;

		        using (var imageData = ItemImage.Image.AsPNG())
		        {
		            image = new byte[imageData.Length];
                    Marshal.Copy(imageData.Bytes, image, 0, Convert.ToInt32(imageData.Length));
		        }

		        var updateItemCommand = new Item
		        {
		            Id = _item.Id,
		            Name = NameText.Text,
		            SerialNumber = SerialText.Text,
		            Value = double.Parse(ValueText.Text),
		            Image = image,
		        };

		        await _homepwnerApi.UpdateItem(updateItemCommand);
		    }
            catch (Exception ex)
		    {
                var alert = UIAlertController.Create("Network Error",
                    "There was a problem making the network request. Please try again.", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, null));
                PresentViewController(alert, true, null);
            }

			NavigationController.PopToRootViewController(true);
		}

		void ImagePickerController_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			var photo = e.OriginalImage;

			var photoFileName = Path.Combine(ApplicationConstants.ApplicationDocumentsDirectory, "Photo.jpeg");
			var photoData = photo.AsJPEG();

			NSError photoSaveError;

			photoData.Save(photoFileName, NSDataWritingOptions.Atomic, out photoSaveError); ;

			_imagePickerController.DismissViewController(true, null);
		}

		void ImagePickerController_Canceled(object sender, EventArgs e)
		{
			_imagePickerController.DismissViewController(true, null);
		}

		void CameraButton_Clicked(object sender, EventArgs e)
		{
			PresentModalViewController(_imagePickerController, true);
		}

		public void SetItem(Item item)
		{
			_item = item;
		}
	}
}