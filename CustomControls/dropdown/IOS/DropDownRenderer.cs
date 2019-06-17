using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms.Platform.iOS;
using Foundation;
using UIKit;


[assembly:Xamarin.Forms.ExportRenderer(typeof(Xamarin.Forms.Picker),typeof(Xamarin.Forms.DropDownRenderer))]
namespace Xamarin.Forms
{
    public class DropDownRenderer : PickerRenderer
    {
        public DropDownRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}