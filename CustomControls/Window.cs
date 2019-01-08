using System;
using System.Collections.Generic;
using System.IO;

using Xamarin.Forms;

namespace RSPLMarketSurvey.CustomControls
{
    public class Window : ContentView
    {
        private static Thickness _contentPadding = 10;

        private static FontAttributes _titleFontAttributes = FontAttributes.Bold;
        private static double _titleFontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
        private static Color _titleTextColor = Color.White;
        private static Color _titlebarBackgroundColor = Color.FromHex("#0066cc");
        private static double _titlebarHeight = 50;
        private string _defaultIconBase64 = "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";
        private ImageSource _titleIcon;

        public Window()
        {
            TitlebarControls = new List<View>();
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
            CommandContainer = new StackLayout { HorizontalOptions = LayoutOptions.EndAndExpand, Orientation = StackOrientation.Horizontal };
            TitlebarLayout = new StackLayout
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children = {
                                            _IconImage ,
                                            _TitleLabel,
                                            CommandContainer
                                                    }
            };

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
                        TitlebarLayout
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

            foreach (var item in TitlebarControls)
            {
                CommandContainer.Children.Add(item);
            }
        }

        public ImageSource TitleIcon { get => _titleIcon; set => _titleIcon = value; }
        public string TitleText { get; set; }

        private StackLayout ContentLayout { get; set; }
        private Image _IconImage { get; set; }
        private Label _TitleLabel { get; set; }

        public View ContentBackgroundImage { get; private set; }
        public StackLayout CommandContainer { get; private set; }
        public StackLayout TitlebarLayout { get; private set; }
        public View WindowContent { get; set; }

        public List<View> TitlebarControls { get; set; }

        public FontAttributes TitleFontAttributes { get => (FontAttributes)GetValue(TitleFontAttributesProperty); set => SetValue(TitleFontAttributesProperty, value); }
        public static readonly BindableProperty TitleFontAttributesProperty = BindableProperty.Create("TitleFontAttributes", typeof(FontAttributes), typeof(FontAttributes), _titleFontAttributes);
        public string TitleFontFamily { get => (string)GetValue(TitleFontFamilyProperty); set => SetValue(TitleFontFamilyProperty, value); }
        public static readonly BindableProperty TitleFontFamilyProperty = BindableProperty.Create("TitleFontFamily", typeof(string), typeof(string), "Arial");
        public double TitleFontSize { get => (double)GetValue(TitleFontSizeProperty); set => SetValue(TitleFontSizeProperty, value); }
        public static readonly BindableProperty TitleFontSizeProperty = BindableProperty.Create("TitleFontSize", typeof(double), typeof(double), _titleFontSize);
        public Color TitleTextColor { get => (Color)GetValue(TitleTextColorProperty); set => SetValue(TitleTextColorProperty, value); }
        public static readonly BindableProperty TitleTextColorProperty = BindableProperty.Create("TitleTextColor", typeof(Color), typeof(Color), _titleTextColor);
        public Color ContentBackgroundColor { get => (Color)GetValue(ContentBackgroundColorProperty); set => SetValue(ContentBackgroundColorProperty, value); }
        public static readonly BindableProperty ContentBackgroundColorProperty = BindableProperty.Create("ContentBackgroundColor", typeof(Color), typeof(Color), Color.Transparent);
        public Thickness ContentPadding { get => (Thickness)GetValue(ContentPaddingProperty); set => SetValue(ContentPaddingProperty, value); }
        public static readonly BindableProperty ContentPaddingProperty = BindableProperty.Create("ContentPadding", typeof(Thickness), typeof(Thickness), _contentPadding);
        public Color TitlebarBackgroundColor { get => (Color)GetValue(TitlebarBackgroundColorProperty); set => SetValue(TitlebarBackgroundColorProperty, value); }
        public static readonly BindableProperty TitlebarBackgroundColorProperty = BindableProperty.Create("TitlebarBackgroundColor", typeof(Color), typeof(Color), _titlebarBackgroundColor);
        public ImageSource TitileBackgroundImageSource { get => (ImageSource)GetValue(TitileBackgroundImageSourceProperty); set => SetValue(TitileBackgroundImageSourceProperty, value); }
        public static readonly BindableProperty TitileBackgroundImageSourceProperty = BindableProperty.Create("TitileBackgroundImageSource", typeof(ImageSource), typeof(ImageSource), null);
        public double TitlebarHeight { get => (double)GetValue(TitlebarHeightProperty); set => SetValue(TitlebarHeightProperty, value); }
        public static readonly BindableProperty TitlebarHeightProperty = BindableProperty.Create("TitlebarHeight", typeof(double), typeof(double), _titlebarHeight);
        public ImageSource ContentBackgroundImageSource { get => (ImageSource)GetValue(ContentBackgroundImageSourceProperty); set => SetValue(ContentBackgroundImageSourceProperty, value); }
        public static readonly BindableProperty ContentBackgroundImageSourceProperty = BindableProperty.Create("ContentBackgroundImageSource", typeof(ImageSource), typeof(ImageSource), null);

    }
}