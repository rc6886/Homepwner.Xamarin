using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using System.Linq;
using Homepwner.Xamarin.iOS.Infrastructure.HTTP;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

namespace Homepwner.Xamarin.iOS
{
	public partial class ItemsTableViewController : UITableViewController
	{
		public ItemsTableViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			EditButton.Clicked += EditButton_Clicked;
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

		    var homepwnerApi = RestService.For<IHomepwnerApi>("http://homepwnerapi.ngrok.io", new RefitSettings
		    {
		        JsonSerializerSettings = new JsonSerializerSettings
		        {
		            ContractResolver = new CamelCasePropertyNamesContractResolver()
		        }
		    });

		    IList<Item> items = new List<Item>();

		    try
		    {
                var response = homepwnerApi.GetAllItems();
                items = response.Result.ToList();
            }
		    catch
		    {
		        var alert = UIAlertController.Create("Network Error",
		            "There was a problem making the network request. Please try again.", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Cancel, null));
		        PresentViewController(alert, true, null);
		    }
		    

			TableView.Source = new ItemsTableSource(NavigationController, items);
		}

		void EditButton_Clicked(object sender, EventArgs e)
		{
			TableView.SetEditing(!TableView.Editing, true);

			EditButton.Title = TableView.Editing ? "Done" : "Edit";
		}
	}

	public class ItemsTableSource : UITableViewSource
	{
		private readonly IList<Item> _items;
		private string CellIdentifier => "ItemCell";

		private readonly UINavigationController _navigationController;

		public ItemsTableSource(UINavigationController navigationController, IList<Item> items)
		{
			_navigationController = navigationController;
			_items = items;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);
			var item = _items.ElementAt(indexPath.Row);

			if (cell == null)
			{
				return new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
			}

			cell.TextLabel.Text = item.Name;

			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return _items.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var itemDetailViewController = _navigationController.Storyboard.InstantiateViewController("ItemDetailViewController")
																as ItemDetailViewController;
			itemDetailViewController.SetItem(_items.ElementAt(indexPath.Row));
			_navigationController.PushViewController(itemDetailViewController, true);
		}

		public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			switch (editingStyle)
			{
				case UITableViewCellEditingStyle.Delete:
					_items.RemoveAt(indexPath.Row);
					tableView.DeleteRows(new[] { indexPath }, UITableViewRowAnimation.Automatic);
					break;
			}
		}

		public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
		{
			return true;
		}
	}

	public class Item
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string SerialNumber { get; set; }
		public double Value { get; set; }
		public DateTime DateCreated { get; set; }
	}
}


