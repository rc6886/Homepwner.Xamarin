// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Homepwner.Xamarin.iOS
{
    [Register ("ItemDetailViewController")]
    partial class ItemDetailViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIBarButtonItem CameraButton { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel DateCreated { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Name { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField NameText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Serial { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField SerialText { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel Value { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField ValueText { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (CameraButton != null) {
                CameraButton.Dispose ();
                CameraButton = null;
            }

            if (DateCreated != null) {
                DateCreated.Dispose ();
                DateCreated = null;
            }

            if (Name != null) {
                Name.Dispose ();
                Name = null;
            }

            if (NameText != null) {
                NameText.Dispose ();
                NameText = null;
            }

            if (Serial != null) {
                Serial.Dispose ();
                Serial = null;
            }

            if (SerialText != null) {
                SerialText.Dispose ();
                SerialText = null;
            }

            if (Value != null) {
                Value.Dispose ();
                Value = null;
            }

            if (ValueText != null) {
                ValueText.Dispose ();
                ValueText = null;
            }
        }
    }
}