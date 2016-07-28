using System;
using UIKit;
using System.IO;
using Foundation;

namespace Homepwner.Xamarin.iOS
{
	public partial class ItemDetailViewController : UIViewController
	{
		private UIImagePickerController _imagePickerController;
		private Item _item;

		public ItemDetailViewController(IntPtr handle) : base(handle)
		{
		}

		public ItemDetailViewController(Item item)
		{
			_item = item; ;
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

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			if (_item == null)
			{
				return;
			}

			NameText.Text = _item.Name;
			SerialText.Text = _item.SerialNumber;
			ValueText.Text = _item.Value.ToString();
		}

		void DoneButton_Clicked(object sender, EventArgs e)
		{
			ItemData.Items.Add(new Item
			{
				Id = Guid.NewGuid(),
				Name = NameText.Text,
				SerialNumber = SerialText.Text,
				Value = double.Parse(ValueText.Text),
			});

			NavigationController.PopToRootViewController(true);
		}

		void ImagePickerController_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			var photo = e.OriginalImage;

			var photoFileName = Path.Combine(ApplicationConstants.ApplicationDocumentsDirectory, "Photo.jpeg");
			var photoData = photo.AsJPEG();

			NSError photoSaveError;

			photoData.Save(photoFileName, Foundation.NSDataWritingOptions.Atomic, out photoSaveError);

			//ItemData.Items.Add(new Item
			//{
			//	Id = Guid.NewGuid(),
			//	Name = NameText.Text,
			//	SerialNumber = SerialText.Text,
			//	Value = double.Parse(ValueText.Text),
			//});

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