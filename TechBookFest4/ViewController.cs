using System;
using UIKit;
using Foundation;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TechBookFest4
{
    [Register(nameof(ViewController))]
    public partial class ViewController : UITableViewController
    {
        private const int _numberOfColors = 24;
        private IReadOnlyList<UIColor> _colors;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var rnd = new Random();
            this._colors = Enumerable
                .Range(0, _numberOfColors)
                .Select(x => UIColor.FromHSB((nfloat)rnd.NextDouble(), (nfloat)Math.Min(1.0, rnd.NextDouble() + 0.5), (nfloat)Math.Min(1.0, rnd.NextDouble() + 0.5)))
                .ToArray();
        }

        public override nint NumberOfSections(UITableView tableView) => 1;

		public override nint RowsInSection(UITableView tableView, nint section) => _numberOfColors;

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
            var cell = (ColorTableViewCell)tableView.DequeueReusableCell(nameof(ColorTableViewCell), indexPath);
            cell.UpdateByColor(this._colors[(int)indexPath.Item]);
            return cell;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
            tableView.DeselectRow(indexPath, true);
            var vc = new ColorViewController(this._colors[(int)indexPath.Item]);
            this.NavigationController.PushViewController(vc, true);
		}
	}
}
