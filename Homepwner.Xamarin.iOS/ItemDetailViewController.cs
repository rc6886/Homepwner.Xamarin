using System;
using UIKit;

namespace Homepwner.Xamarin.iOS
{
	public partial class ItemDetailViewController : UIViewController
	{
		private UIImagePickerController _imagePickerController;

		public ItemDetailViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			_imagePickerController = new UIImagePickerController
			{
				SourceType = UIImagePickerControllerSourceType.Camera,
			};

			_imagePickerController.FinishedPickingMedia += ImagePickerController_FinishedPickingMedia;
			_imagePickerController.Canceled += ImagePickerController_Canceled;

			CameraButton.Clicked += CameraButton_Clicked;
		}

		void ImagePickerController_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
		{
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
	}
}