using System;
using Foundation;
using UIKit;
using System.Collections.Generic;
using System.Linq;

namespace Homepwner.Xamarin.iOS
{
	public partial class ItemsTableViewController : UITableViewController
	{
		public ItemsTableViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);

			TableView.Source = new ItemsTableSource(NavigationController, ItemData.Items);
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
			return _items.Count();
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


