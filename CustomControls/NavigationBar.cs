using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace HS.Controls
{
    public class NavigationBar : Xamarin.Forms.ContentView
    {
        StackLayout Bar = new StackLayout();
        Label BackButtonLabel = new Label();
        Image BackButtonImage = new Image();
        Button BackButton = new Button();
        Label TitleLabel = new Label();
        public NavigationBar()
        {

            Controls.Clear();
        }

        public static BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(string), ""); public string Title { get => (string)GetValue(TitleProperty); set => SetValue(TitleProperty, value); }
        public static BindableProperty TitleColorProperty = BindableProperty.Create("TitleColor", typeof(Color), typeof(Color), Color.White); public Color TitleColor { get => (Color)GetValue(TitleColorProperty); set => SetValue(TitleColorProperty, value); }
        public static BindableProperty BarHeightProperty = BindableProperty.Create("BarHeight", typeof(double), typeof(double), 50.00); public double BarHeight { get => (double)GetValue(BarHeightProperty); set => SetValue(BarHeightProperty, value); }
        public static BindableProperty BarColorProperty = BindableProperty.Create("BarColor", typeof(Color), typeof(Color), Color.Blue); public Color BarColor { get => (Color)GetValue(BarColorProperty); set => SetValue(BarColorProperty, value); }
        public static BindableProperty TitleFontSizeProperty = BindableProperty.Create("TitleFontSize", typeof(double), typeof(double), 20.00); public double TitleFontSize { get => (double)GetValue(TitleFontSizeProperty); set => SetValue(TitleFontSizeProperty, value); }
        public static BindableProperty BarPaddingProperty = BindableProperty.Create("BarPadding", typeof(Thickness), typeof(Thickness), new Thickness(20, 5, 5, 5)); public Thickness BarPadding { get => (Thickness)GetValue(BarPaddingProperty); set => SetValue(BarPaddingProperty, value); }
        public static BindableProperty TitleFontAttributeProperty = BindableProperty.Create("TitleFontAttribute", typeof(FontAttributes), typeof(FontAttributes), FontAttributes.Bold); public FontAttributes TitleFontAttribute { get => (FontAttributes)GetValue(TitleFontAttributeProperty); set => SetValue(TitleFontAttributeProperty, value); }
        public static BindableProperty BackButtonTextProperty = BindableProperty.Create("BackButtonText", typeof(string), typeof(string), "←"); public string BackButtonText { get => (string)GetValue(BackButtonTextProperty); set => SetValue(BackButtonTextProperty, value); }
        public static BindableProperty BackButtonColorProperty = BindableProperty.Create("BackButtonColor", typeof(Color), typeof(Color), Color.White); public Color BackButtonColor { get => (Color)GetValue(BackButtonColorProperty); set => SetValue(BackButtonColorProperty, value); }
        public static BindableProperty BackButtonIconProperty = BindableProperty.Create("BackButtonIcon", typeof(FileImageSource), typeof(FileImageSource), null); public FileImageSource BackButtonIcon { get => (FileImageSource)GetValue(BackButtonIconProperty); set => SetValue(BackButtonIconProperty, value); }
        public static BindableProperty BackButtonFontSizeProperty = BindableProperty.Create("BackButtonFontSize", typeof(double), typeof(double), 20.00); public double BackButtonFontSize { get => (double)GetValue(BackButtonFontSizeProperty); set => SetValue(BackButtonFontSizeProperty, value); }
        public static BindableProperty ButtonWidthProperty = BindableProperty.Create("ButtonWidth", typeof(double), typeof(double), 45.00); public double ButtonWidth { get => (double)GetValue(ButtonWidthProperty); set => SetValue(ButtonWidthProperty, value); }
        public static BindableProperty BackButtonTypeProperty = BindableProperty.Create("BackButtonType", typeof(BackButtonTypeEnum), typeof(BackButtonTypeEnum), BackButtonTypeEnum.Label); public BackButtonTypeEnum BackButtonType { get => (BackButtonTypeEnum)GetValue(BackButtonTypeProperty); set => SetValue(BackButtonTypeProperty, value); }
        public static BindableProperty BackButtonBackgroundColorProperty = BindableProperty.Create("BackButtonBackgroundColor", typeof(Color), typeof(Color), Color.Transparent); public Color BackButtonBackgroundColor { get => (Color)GetValue(BackButtonBackgroundColorProperty); set => SetValue(BackButtonBackgroundColorProperty, value); }
        public static BindableProperty BackButtonImageSourceProperty = BindableProperty.Create("BackButtonImageSource", typeof(ImageSource), typeof(ImageSource), null); public ImageSource BackButtonImageSource { get => (ImageSource)GetValue(BackButtonImageSourceProperty); set => SetValue(BackButtonImageSourceProperty, value); }
        public static BindableProperty ControlsProperty = BindableProperty.Create("Controls", typeof(List<View>), typeof(List<View>), new List<View>());
        private bool _showBackButton=true;
        private bool _showTitleText=true;

        public List<View> Controls { get => (List<View>)GetValue(ControlsProperty); set => SetValue(ControlsProperty, value); }
        public bool ShowBackButton { get => _showBackButton; set => _showBackButton = value; }
        public bool ShowTitleText { get => _showTitleText; set => _showTitleText = value; }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName.ToUpper())
            {
                case "TITLE": TitleLabel.Text = Title; break;
                case "TITLECOLOR": TitleLabel.TextColor = TitleColor; break;
                case "BARHEIGHT": Bar.HeightRequest = BarHeight; break;
                case "BARCOLOR": Bar.BackgroundColor = BarColor; break;
                case "TITLEFONTSIZE": TitleLabel.FontSize = TitleFontSize; break;
                case "BARPADDING": Bar.Padding = BarPadding; break;
                case "TITLEFONTATTRIBUTE": TitleLabel.FontAttributes = TitleFontAttribute; break;
                case "BACKBUTTONTEXT": BackButton.Text = BackButtonText; BackButtonLabel.Text = BackButtonText; break;
                case "BACKBUTTONCOLOR": BackButton.TextColor = BackButtonColor; BackButtonLabel.TextColor = BackButtonColor; break;
                case "BACKBUTTONICON": break;
                case "BACKBUTTONFONTSIZE": break;
                case "BUTTONWIDTH": break;
                case "BACKBUTTONTYPE": break;
                case "BACKBUTTONBACKGROUNDCOLOR": break;
                case "BACKBUTTONIMAGESOURCE": break;
                case "CONTROLS": break;
                case "SHOWBACKBUTTON":
                    BackButtonLabel.IsVisible = ShowBackButton;
                    BackButton.IsVisible = ShowBackButton;
                    BackButtonImage.IsVisible = ShowBackButton;
                    break;
                case "SHOWTITLETEXT":
                    TitleLabel.IsVisible = ShowTitleText;

                    break;
            }
        }
        protected override void OnParentSet()
        {
            base.OnParentSet();

            Bar.Orientation = StackOrientation.Horizontal;
            switch (BackButtonType)
            {
                case BackButtonTypeEnum.Label:
                    BackButtonLabel = new Label { Text = BackButtonText, TextColor = BackButtonColor, FontSize = BackButtonFontSize, BackgroundColor = BackButtonBackgroundColor, WidthRequest = ButtonWidth, FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.CenterAndExpand, IsVisible = ShowBackButton };
                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
                    BackButtonLabel.GestureRecognizers.Add(tapGestureRecognizer);
                    Bar.Children.Add(BackButtonLabel);
                    break;
                case BackButtonTypeEnum.Image:
                    BackButtonImage = new Image { Source = BackButtonImageSource, BackgroundColor = BackButtonBackgroundColor, WidthRequest = ButtonWidth, VerticalOptions = LayoutOptions.CenterAndExpand, IsVisible = ShowBackButton };
                    Bar.Children.Add(BackButtonImage);
                    break;
                case BackButtonTypeEnum.Button:
                    BackButton = new Button { Text = BackButtonText, TextColor = BackButtonColor, Image = BackButtonIcon, FontSize = BackButtonFontSize, BackgroundColor = BackButtonBackgroundColor, WidthRequest = ButtonWidth, FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.CenterAndExpand, IsVisible = ShowBackButton };
                    Bar.Children.Add(BackButton);
                    break;

            }


            TitleLabel = new Label { Text = Title, TextColor = TitleColor, FontSize = TitleFontSize, FontAttributes = TitleFontAttribute, VerticalOptions = LayoutOptions.CenterAndExpand, IsVisible = ShowTitleText };
            Bar.Children.Add(TitleLabel);
            Bar.HeightRequest = BarHeight;
            Bar.BackgroundColor = BarColor;
            Bar.Padding = BarPadding;
            foreach (var item in Controls)
            {
                if (!(Bar.Children.Contains(item)))
                {
                    Bar.Children.Add(item);
                }
            }

            this.Content = Bar;

        }



        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                //var pages =Navigation.NavigationStack.GetEnumerator();
                //int i = 0;
                //while (i<Navigation.NavigationStack.Count-1)
                //{
                //    pages.MoveNext();
                //    i++;
                //}

                Navigation.PopAsync();

            }
            catch (Exception)
            {


            }
        }
    }



    public enum BackButtonTypeEnum
    {
        Label = 0,
        Image = 1,
        Button = 2
    }
}
