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

			TableView.Source = new ItemsTableSource(new List<Item>
			{
				new Item { Name = "Test" },
 				new Item { Name = "Test2" },
			});
		}
	}

	public class ItemsTableSource : UITableViewSource
	{
		private readonly IEnumerable<Item> _items;
		private string CellIdentifier => "ItemCell";

		public ItemsTableSource(IEnumerable<Item> items)
		{
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


