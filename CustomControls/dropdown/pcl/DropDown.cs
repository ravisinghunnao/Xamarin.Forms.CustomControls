using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;

namespace NIFShopping.CustomControls
{
    public class DropDown : ContentView
    {

        public event EventHandler LeftIconTapped;
        private Picker _picker = null;
        private BoxView _LineRemover = null;
        private Color _borderColor = Color.Black;
        private CornerRadius _cornerRadius = 10;

        BoxView _BackgroundBox = null;
        private BoxView _BorderBox = null;
        private Image _LeftIcon = null;
        private double _IconWidth = 15;
        private double _IconHeight = 15;
        private double _DropIconWidth = 25;
        private double _DropIconHeight = 25;
        private Image _DropDownIcon = null;
        private Thickness _leftIconMargin = new Thickness(15, 10, 0, 10);
        private Thickness _rightIconMargin = new Thickness(0, 10, 15, 10);
        
        private IList<string> _items= new List<string>();

        public DropDown()
        {

            _BackgroundBox = new BoxView();
            _BorderBox = new BoxView();
            _picker = new Picker();
            _LineRemover = new BoxView();
            _LeftIcon = new Image();
            _DropDownIcon = new Image();
            TapGestureRecognizer LeftIconTapped = new TapGestureRecognizer();
            LeftIconTapped.Tapped += LeftIconTapped_Tapped;

            _LeftIcon.GestureRecognizers.Add(LeftIconTapped);
        }

        protected virtual void OnLeftIconTapped(EventArgs e)
        {
            LeftIconTapped?.Invoke(this, e);
        }

        private void LeftIconTapped_Tapped(object sender, EventArgs e)
        {
            OnLeftIconTapped(e);
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            _BackgroundBox.Color = BorderColor; _BackgroundBox.CornerRadius = CornerRadius; _BackgroundBox.HorizontalOptions = LayoutOptions.FillAndExpand; _BackgroundBox.VerticalOptions = LayoutOptions.FillAndExpand;

            _BorderBox.Color = BackgroundColor; _BorderBox.CornerRadius = CornerRadius; _BorderBox.Margin = BorderWidth; _BorderBox.HorizontalOptions = LayoutOptions.FillAndExpand; _BorderBox.VerticalOptions = LayoutOptions.FillAndExpand;
            _picker.ItemsSource = Items.ToList(); _picker.FontSize = FontSize; _picker.Title = Title; _picker.TextColor = TextColor; _picker.TitleColor = TitleColor; _picker.BackgroundColor = Color.Transparent; _picker.HorizontalOptions = LayoutOptions.FillAndExpand; _picker.VerticalOptions = LayoutOptions.CenterAndExpand; _picker.Margin = new Thickness(_IconWidth + CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);



            _LineRemover.HorizontalOptions = LayoutOptions.FillAndExpand; _LineRemover.Color = BackgroundColor; _LineRemover.HeightRequest = LineRemoverHeight; _LineRemover.VerticalOptions = LayoutOptions.End; _LineRemover.WidthRequest = Picker.Width; _LineRemover.Margin = new Thickness(_IconWidth + CornerRadius.TopLeft, 0, CornerRadius.TopLeft, BorderWidth);

            _LeftIcon.IsVisible = ShowLeftIcon; _LeftIcon.WidthRequest = _IconWidth; _LeftIcon.HeightRequest = _IconHeight; _LeftIcon.Source = LeftIconImage; _LeftIcon.HorizontalOptions = LayoutOptions.Start; _LeftIcon.Aspect = Aspect.Fill; _LeftIcon.Margin = LeftIconMargin; _LeftIcon.VerticalOptions = LayoutOptions.CenterAndExpand;

            _DropDownIcon.WidthRequest = _DropIconWidth; _DropDownIcon.HeightRequest = _DropIconHeight; _DropDownIcon.Source = DropDownIconImage; _DropDownIcon.HorizontalOptions = LayoutOptions.End; _DropDownIcon.Aspect = Aspect.Fill; _DropDownIcon.Margin = RightIconMargin; _DropDownIcon.VerticalOptions = LayoutOptions.CenterAndExpand;


            if (ShowLeftIcon)
            {
                _picker.Margin = new Thickness(_IconWidth + CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);
                _LineRemover.Margin = new Thickness(_IconWidth + CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);
            }
            else
            {
                _picker.Margin = new Thickness(CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);
                _LineRemover.Margin = new Thickness(CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);
            }

            Grid grid = new Grid
            {
                Children ={
                            _BackgroundBox,
                           _BorderBox,
                            _picker,
                         //   _LineRemover,
                            _LeftIcon,
                            _DropDownIcon

                        }
                ,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ColumnSpacing = 0,
                RowSpacing = 0,
                Padding = 0,
                Margin = 0
            };
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
            grid.GestureRecognizers.Add(tapGestureRecognizer);

            Content = new StackLayout
            {
                Children = {
                    grid
                }
            };

            this.BackgroundColor = Color.Transparent;
            _picker.Focused += _picker_Focused;
            _picker.Unfocused += _picker_Unfocused;
        }


        private void _picker_Unfocused(object sender, FocusEventArgs e)
        {
            IsFocused = false;
        }

        private void _picker_Focused(object sender, FocusEventArgs e)
        {
            IsFocused = true;
        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            _picker.Focus();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            switch (propertyName.ToUpper())
            {
                case "SHOWLEFTICON":
                    if (_LeftIcon != null)
                    {
                        _LeftIcon.IsVisible = ShowLeftIcon;
                        if (ShowLeftIcon)
                        {
                            _picker.Margin = new Thickness(_IconWidth + CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);
                            _LineRemover.Margin = new Thickness(_IconWidth + CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);
                        }
                        else
                        {
                            _picker.Margin = new Thickness(CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);
                            _LineRemover.Margin = new Thickness(CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);
                        }

                    }
                    break;

                case "LEFTICONIMAGE":
                    _LeftIcon.Source = LeftIconImage;
                    break;
                case "BORDERCOLOR":
                    if (_BackgroundBox != null)
                    {
                        _BackgroundBox.Color = BorderColor;
                    }
                    break;
                default:
                    break;
            }
        }

        public bool ShowLeftIcon
        {
            get { return (bool)GetValue(ShowLeftIconProperty); }
            set => SetValue(ShowLeftIconProperty, value);
        }



        public static readonly BindableProperty ShowLeftIconProperty = BindableProperty.Create("ShowLeftIcon", typeof(bool), typeof(bool), true);

        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set => SetValue(IsPasswordProperty, value);
        }



        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create("IsPassword", typeof(bool), typeof(bool), false);
        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set => SetValue(TextColorProperty, value);
        }



        public static readonly BindableProperty TextColorProperty = BindableProperty.Create("TextColor", typeof(Color), typeof(Color), Color.Black);

        public Color TitleColor
        {
            get { return (Color)GetValue(TitleColorProperty); }
            set => SetValue(TitleColorProperty, value);
        }



        public static readonly BindableProperty TitleColorProperty = BindableProperty.Create("TitleColor", typeof(Color), typeof(Color), Color.Black);


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set => SetValue(TextProperty, value);
        }



        public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(string), "");

        public string Title
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set => SetValue(PlaceholderProperty, value);
        }



        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string), typeof(string), "");







        public Picker Picker { get => _picker; }


        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set => SetValue(BorderColorProperty, value);
        }



        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create("BorderColor", typeof(Color), typeof(Color), Color.Black);




        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set => SetValue(CornerRadiusProperty, value);
        }



        public static readonly BindableProperty CornerRadiusProperty = BindableProperty.Create("CornerRadius", typeof(CornerRadius), typeof(CornerRadius), new CornerRadius(5));



        public int BorderWidth
        {
            get { return (int)GetValue(BorderWidthProperty); }
            set => SetValue(BorderWidthProperty, value);

        }



        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create("BorderWidth", typeof(int), typeof(int), 1);


        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set => SetValue(FontSizeProperty, value);

        }

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create("FontSize", typeof(double), typeof(double), Device.GetNamedSize(NamedSize.Medium, typeof(Entry)));

        public Thickness LeftIconMargin { get => _leftIconMargin; private set => _leftIconMargin = value; }


        public ImageSource LeftIconImage
        {
            get { return (ImageSource)GetValue(LeftIconImageProperty); }
            set => SetValue(LeftIconImageProperty, value);

        }

        public static readonly BindableProperty LeftIconImageProperty = BindableProperty.Create("LeftIconImage", typeof(ImageSource), typeof(ImageSource), ImageSource.FromFile("textboxicon.png"));

        public IList<string> Items { get => _items; set => _items = value; }


        public ImageSource DropDownIconImage
        {
            get { return (ImageSource)GetValue(DropDownIconImageProperty); }
            set => SetValue(DropDownIconImageProperty, value);

        }

        public Thickness RightIconMargin { get => _rightIconMargin; private set => _rightIconMargin = value; }

        public static readonly BindableProperty DropDownIconImageProperty = BindableProperty.Create("DropDownIconImage", typeof(ImageSource), typeof(ImageSource), ImageSource.FromFile("dropdowniconimage.png"));
        public double LineRemoverHeight
        {
            get { return (double)GetValue(LineRemoverHeightProperty); }
            set => SetValue(LineRemoverHeightProperty, value);
        }



        public static readonly BindableProperty LineRemoverHeightProperty = BindableProperty.Create("LineRemoverHeight", typeof(double), typeof(double), 10.0);

        public new bool IsFocused
        {
            get { return (bool)GetValue(IsFocusedProperty); }
            private set { SetValue(IsFocusedProperty, value); }

        }

        public new static readonly BindableProperty IsFocusedProperty = BindableProperty.Create("IsFocused", typeof(bool), typeof(bool), false);




    }
}