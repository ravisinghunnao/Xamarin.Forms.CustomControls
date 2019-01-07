using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace RSPLMarketSurvey.CustomControls
{
	public class Window : ContentView
	{
       
        private Thickness _contentPadding = 10;
       
        private FontAttributes _titleFontAttributes = FontAttributes.Bold;
        private double _titleFontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
        private Color _titleTextColor = Color.White;
        private Color _titlebarBackgroundColor = Color.FromHex("#0066cc");
        private double _titlebarHeight = 50;
        private string _defaultIconBase64 = "iVBORw0KGgoAAAANSUhEUgAAAGQAAABkCAYAAABw4pVUAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAGLgAABi4Bu5kyRgAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAAHwSURBVHic7dyhbgJBEMbxb5t6ICGpw9SQ1NRhMHWVPAmprKisqO4T8AiVdVTU4GpIampwJCQtT7A1iLuFlLmUuxuO/88t2WFIvuQuOxdOAgAAAAAAAAAAAAAAAACgwUIVTWKMQVKril4lWocQYtlNzstusNGS9F1Rr7J0JP2U3eSs7AYohkCcIRBnqrqH7PIkaWbc+yjpKrOeSnreUzOWdJNZzyU9GPsNJN0b9x5UnYHMQggvlo0xxrvko8W+2hjjKPloVaCfZVspuGQ5QyDOEIgzBOIMgThDIM4QiDME4gyBOFPV85C2tsfvc0kr41dcK/88ZSnpc09NX9JFZr2W9GHs11V+VCNJnRBC6eP3OgM5NpUEwiXLGQJxps5p71TSwrj3Vvn7wZek9z01Q0mXmfVS0quxX0/50X2zxBjbcVs6Hv+r/i2pnRhqJknNW4F+ox2/t22t/w8uWc4QiDME4gyBOEMgzhCIMwTiTJ0Hw3GBs0g/WQ8NZ5Fh+h2W88tGz7jv4Bgu2jFcPEUE4kyd95Bje0DVHAwX7bhkOUMgzhCIMwTiDIE4QyDOEIgzBOIMgThT5+hkEO3/du0m657hpJ+O0LsFpgMD476DY/xux/j9FBGIMwTiDC8ws6vkBWYAAAAAAAAAAAAAAAAAADTaL4LEA4L7tIyJAAAAAElFTkSuQmCC";
        private ImageSource _titleIcon;
        public Window ()
		{
            _titleIcon = ImageSource.FromStream(
         () => new MemoryStream(Convert.FromBase64String(_defaultIconBase64)));
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            Init();
        }

        private void ContentLayout_SizeChanged(object sender, EventArgs e)
        {
            ContentBackgroundImage.HeightRequest = ContentLayout.Height;
        }

        private void Init()
        {
           
            _IconImage = new Image { Source = TitleIcon, Aspect = Aspect.AspectFit, Margin = new Thickness(5, 0, 0, 0) };
            _TitleLabel = new Label { Text = TitleText, FontAttributes = TitleFontAttributes, FontFamily = TitleFontFamily, FontSize = TitleFontSize, TextColor = TitleTextColor };


            ContentLayout = new StackLayout
            {

                Padding = ContentPadding,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = ContentBackgroundColor,
                Children =
                {
                    WindowContent
                }

            };

            ContentLayout.SizeChanged += ContentLayout_SizeChanged;

          
            ContentBackgroundImage = new Image { Source = ContentBackgroundImageSource, HorizontalOptions = LayoutOptions.FillAndExpand, Aspect = Aspect.Fill, HeightRequest = 0 };

           
 

            Content = new StackLayout
            {
                Spacing = 0,
                Children = {
                    new StackLayout
                    {
                      BackgroundColor = TitlebarBackgroundColor,
                      Spacing=0,
                      HorizontalOptions=LayoutOptions.FillAndExpand,
                      HeightRequest=TitlebarHeight,
                        Children   =   {
                            new Grid { HorizontalOptions=LayoutOptions.FillAndExpand,VerticalOptions=LayoutOptions.FillAndExpand, Children =
                                {
                                    new Image {Source=TitileBackgroundImageSource,VerticalOptions=LayoutOptions.CenterAndExpand, HorizontalOptions=LayoutOptions.FillAndExpand,Aspect=Aspect.Fill},
                        new StackLayout{
                                        VerticalOptions=LayoutOptions.CenterAndExpand,
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        Orientation = StackOrientation.Horizontal,
                                        Children = {
                                            _IconImage ,
                                            _TitleLabel
                                                    }
                                    }

                        }
                        }
                        }
                    }
                    ,
                    new StackLayout {
                        Children =
                        {
                            new Grid
                            {
                                Children =
                                {
                                    ContentBackgroundImage,
                                    ContentLayout
                                }
                            }

                        }
                    }
                    
                }
            };
        }

        public ImageSource TitleIcon { get => _titleIcon; set => _titleIcon = value; }
        public string TitleText { get; set; }
        public FontAttributes TitleFontAttributes { get => _titleFontAttributes; set => _titleFontAttributes = value; }
        public string TitleFontFamily { get; set; }
        public double TitleFontSize { get => _titleFontSize; set => _titleFontSize = value; }
        public Color TitleTextColor { get => _titleTextColor; set => _titleTextColor = value; }
        public Color ContentBackgroundColor { get; set; }
      
    
        public Thickness ContentPadding { get => _contentPadding; set => _contentPadding = value; }
      

        private StackLayout ContentLayout { get; set; }
        private Image _IconImage { get; set; }
        private Label _TitleLabel { get; set; }
      
        public Color TitlebarBackgroundColor { get => _titlebarBackgroundColor; set => _titlebarBackgroundColor = value; }
   
        public ImageSource TitileBackgroundImageSource { get; set; }
        public double TitlebarHeight { get => _titlebarHeight; set => _titlebarHeight = value; }
        public ImageSource ContentBackgroundImageSource { get; set; }
        public View ContentBackgroundImage { get; private set; }
        public View WindowContent { get; set; }
    }
}