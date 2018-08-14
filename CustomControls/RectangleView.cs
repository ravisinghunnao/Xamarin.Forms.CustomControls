using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
namespace HS.Controls
{
    public class RectangleView : AbsoluteLayout
    {
        
        List<Element> elements = new List<Element>();
        private readonly string _ControlStyleID = "__boxviewstyleid";
        public int BorderWidth { get =>(int) GetValue(BorderWidthProperty); set => SetValue(BorderWidthProperty, value); }

        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create("BorderWidth", typeof(int), typeof(int), 1);
        

        public Color BorderColor { get => (Color)GetValue(BorderColorProperty); set => SetValue(BorderColorProperty, value); }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create("BorderColor", typeof(Color), typeof(Color), Color.Black);

       
        public Color FillColor { get => (Color)GetValue(FillColorProperty); set => SetValue(FillColorProperty, value); }

        public static readonly BindableProperty FillColorProperty = BindableProperty.Create("FillColor", typeof(Color), typeof(Color), Color.Transparent);
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName.ToUpper())
            {
                case "WIDTH":
                case "HEIGHT":
                case "BORDERCOLOR":
                case "BORDERWIDTH":
                case "FILLCOLOR":
                    RenderControl();
                    break;
                default:
                    break;
            }

        }

        private void RenderControl()
        {
            this.Children.Clear();

            BoxView fillBox = new BoxView { StyleId= _ControlStyleID, BackgroundColor = FillColor };
            SetLayoutBounds(fillBox, Rectangle.FromLTRB(0, 0, Width, Height));
            Children.Add(fillBox);

            BoxView bvLeft = new BoxView { StyleId = _ControlStyleID, WidthRequest = BorderWidth, BackgroundColor = BorderColor, HeightRequest = Height };
            Children.Add(bvLeft);
            SetLayoutBounds(bvLeft, Rectangle.FromLTRB(0, 0, BorderWidth, Height));

            BoxView bvTop = new BoxView { StyleId = _ControlStyleID, WidthRequest = Width, BackgroundColor = BorderColor, HeightRequest = BorderWidth };
            Children.Add(bvTop);
            SetLayoutBounds(bvTop, Rectangle.FromLTRB(0, 0, Width, BorderWidth));


            BoxView bvRight = new BoxView { StyleId = _ControlStyleID, WidthRequest = BorderWidth, BackgroundColor = BorderColor, HeightRequest = Height };
            Children.Add(bvRight);
            SetLayoutBounds(bvRight, Rectangle.FromLTRB(Width - BorderWidth, 0, Width, Height));


            BoxView bvBottom = new BoxView { StyleId = _ControlStyleID, WidthRequest = Width, BackgroundColor = BorderColor, HeightRequest = BorderWidth };
            Children.Add(bvBottom);
            SetLayoutBounds(bvBottom, Rectangle.FromLTRB(0, Height - BorderWidth, Width - BorderWidth, Height));



            foreach (Element item in elements)
            {
                this.Children.Remove((View)item);
                this.Children.Add((View)item);
            }






        }

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
            if (child.StyleId != _ControlStyleID)
            {
                if (!elements.Contains(child))
                {

                    elements.Add(child);
                }
            }
        }
    }
}
