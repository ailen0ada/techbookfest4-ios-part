using System;
using UIKit;
namespace TechBookFest4
{
    public class ColorViewController : UIViewController
    {
        private UIColor _color;

        public ColorViewController(UIColor color)
        {
            this._color = color;
        }

		public override void ViewDidLoad()
		{
            base.ViewDidLoad();

            this.ApplyColor();
		}

        private void ApplyColor()
        {
            this.View.BackgroundColor = this._color;

            this._color.GetRGBA(out var r, out var g, out var b, out var _);
            this.NavigationItem.Title = $"#{(int)(r * 255):X2}{(int)(g * 255):X2}{(int)(b * 255):X2}";
        }

        ~ColorViewController()
        {
            System.Diagnostics.Debug.WriteLine($"{nameof(ColorViewController)} has finalized.");
        }
	}
}
