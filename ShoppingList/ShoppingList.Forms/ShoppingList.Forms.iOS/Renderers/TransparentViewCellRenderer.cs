using ShoppingList.Forms.iOS.Renderers;

using Xamarin.Forms;

[assembly: ExportRenderer(typeof(ViewCell), typeof(TransparentViewCellRenderer))]

namespace ShoppingList.Forms.iOS.Renderers
{
    using UIKit;

    using Xamarin.Forms;
    using Xamarin.Forms.Platform.iOS;

    public class TransparentViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            cell.BackgroundColor = UIColor.FromRGBA(0, 0, 0, 0);
            return cell;
        }
    }
}