using System;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace HS.Controls
{
    public class CircleView : AbsoluteLayout
    {
        public double DotSize { get; set; }
        public Color LineColor { get; set; }



        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName.ToUpper())
            {
                case "WIDTH":
                case "HEIGHT":
                    RenderControl();
                    break;

                default:
                    break;
            }
        }

        private void RenderControl()
        {
            try
            {
                Children.Clear();

                double Radius = Height / 2;
                if (Width < Height)
                {
                    Radius = Width / 2;
                }

                    for (double i = 0.0; i < 360.0; i += 1)

                    {

                        double angle = i * System.Math.PI / 180;

                        int x = (int)(150 + Radius * System.Math.Cos(angle));

                        int y = (int)(150 + Radius * System.Math.Sin(angle));



                        putpixel( x, y);

                       
                    }

              






            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void putpixel(int x, int y)
        {
            BoxView fpx = new BoxView { BackgroundColor = LineColor };
            fpx.HeightRequest = DotSize;
            fpx.WidthRequest = DotSize;
            Children.Add(fpx);
            Rectangle FirstPoint = new Rectangle(x, y, DotSize, DotSize);
            SetLayoutBounds(fpx, FirstPoint);

        }
    }
}