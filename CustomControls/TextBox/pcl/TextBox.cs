using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;

namespace NIFShopping.CustomControls
{
    public class TextBox : ContentView
    {

        public event EventHandler LeftIconTapped;
        private Entry _entry = null;
        private BoxView _LineRemover = null;
        private Color _borderColor = Color.Black;
        private CornerRadius _cornerRadius = 10;
       
        BoxView _BorderColorBox = null;
        private BoxView _BackgroundColorBox = null;
        private Image _LeftIcon = null;
        private double _IconWidth = 15;
        private double _IconHeight = 15;
        private Thickness _leftIconMargin = new Thickness(15, 10, 0, 10);
        


        public TextBox()
        {
            _BorderColorBox = new BoxView();
            _BackgroundColorBox = new BoxView();
            _entry = new Entry();
            _LineRemover = new BoxView();
            _LeftIcon = new Image();
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

            _BorderColorBox.Color = BorderColor; _BorderColorBox.CornerRadius = CornerRadius; _BorderColorBox.HorizontalOptions = LayoutOptions.FillAndExpand; _BorderColorBox.VerticalOptions = LayoutOptions.FillAndExpand;

            _BackgroundColorBox.Color = BackgroundColor; _BackgroundColorBox.CornerRadius = CornerRadius; _BackgroundColorBox.Margin = BorderWidth; _BackgroundColorBox.HorizontalOptions = LayoutOptions.FillAndExpand; _BackgroundColorBox.VerticalOptions = LayoutOptions.FillAndExpand;
            _entry.FontSize = FontSize;  _entry.IsPassword = IsPassword; _entry.Keyboard = Keyboard; _entry.MaxLength = MaxLength; _entry.Placeholder = Placeholder;  _entry.TextColor = TextColor; _entry.PlaceholderColor = PlaceholderColor; _entry.BackgroundColor = Color.Transparent; _entry.HorizontalOptions = LayoutOptions.FillAndExpand; _entry.VerticalOptions = LayoutOptions.CenterAndExpand; _entry.Margin = new Thickness(_IconWidth + CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);

 

            _LineRemover.HorizontalOptions = LayoutOptions.FillAndExpand; _LineRemover.Color = BackgroundColor; _LineRemover.HeightRequest = LineRemoverHeight; _LineRemover.VerticalOptions = LayoutOptions.End; _LineRemover.WidthRequest = Entry.Width; _LineRemover.Margin = new Thickness(_IconWidth + CornerRadius.TopLeft, 0, CornerRadius.TopLeft, BorderWidth);
            
            _LeftIcon.IsVisible = ShowLeftIcon; _LeftIcon.WidthRequest = _IconWidth; _LeftIcon.HeightRequest = _IconHeight; _LeftIcon.Source = LeftIconImage; _LeftIcon.HorizontalOptions = LayoutOptions.Start; _LeftIcon.Aspect = Aspect.Fill; _LeftIcon.Margin = LeftIconMargin; _LeftIcon.VerticalOptions = LayoutOptions.CenterAndExpand ;


            if (ShowLeftIcon)
            {
                _entry.Margin =  new Thickness(_IconWidth+ CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);
                _LineRemover.Margin = new Thickness(_IconWidth + CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);
            }
            else
            {
                _entry.Margin = new Thickness(CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);
                _LineRemover.Margin = new Thickness(CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);
            }

            Grid grid = new Grid
            {
                Children ={
                            _BorderColorBox,
                           _BackgroundColorBox,
                            _entry,
                        //    _LineRemover,
                            _LeftIcon,
                           
                        }
                ,HorizontalOptions=LayoutOptions.FillAndExpand,
                VerticalOptions=LayoutOptions.FillAndExpand,
                ColumnSpacing=0,
                RowSpacing=0,
                Padding=0,
                Margin=0
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
            _entry.Focused += _entry_Focused;
            _entry.Unfocused += _entry_Unfocused;
        }

        private void _entry_Unfocused(object sender, FocusEventArgs e)
        {
            IsFocused = false;
        }

        private void _entry_Focused(object sender, FocusEventArgs e)
        {
            IsFocused = true;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            _entry.Focus();
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
                            _entry.Margin =  new Thickness(_IconWidth + CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);
                            _LineRemover.Margin = new Thickness(_IconWidth + CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);
                        }
                        else
                        {
                            _entry.Margin = new Thickness(CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);
                            _LineRemover.Margin = new Thickness(CornerRadius.TopLeft, BorderWidth, CornerRadius.TopLeft, BorderWidth);
                        }

                    }
                    break;
                case "ISPASSWORD":
                    _entry.IsPassword = IsPassword;
                    break;
                case "LEFTICONIMAGE":
                    _LeftIcon.Source = LeftIconImage;
                    break;
                case "ISENABLED":
                    if (IsEnabled == false)
                    {
                        _BorderColorBox.Opacity = 0.5;
                        _entry.Opacity = 0.5;
                    }
                    else
                    {
                        _BorderColorBox.Opacity = 1;
                        _entry.Opacity = 1;
                    }
                    break;
                case "BORDERCOLOR":
                    if (_BorderColorBox != null) {
                        _BorderColorBox.Color = BorderColor;
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

        public double LineRemoverHeight
        {
            get { return (double)GetValue(LineRemoverHeightProperty); }
            set => SetValue(LineRemoverHeightProperty, value);
        }



        public static readonly BindableProperty LineRemoverHeightProperty = BindableProperty.Create("LineRemoverHeight", typeof(double), typeof(double), 10.0);



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

        public Color PlaceholderColor
        {
            get { return (Color)GetValue(PlaceholderColorProperty); }
            set => SetValue(PlaceholderColorProperty, value);
        }



        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create("PlaceholderColor", typeof(Color), typeof(Color), Color.Black);


        //public string Text
        //{
        //    get { return (string)GetValue(TextProperty); }
        //    set => SetValue(TextProperty, value);
        //}



        //public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(string), "");

        public string Text { get=>_entry.Text; set=>_entry.Text=value; }

        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set => SetValue(PlaceholderProperty, value);
        }



        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create("Placeholder", typeof(string), typeof(string), "");

       public Keyboard Keyboard
        {
            get { return (Keyboard)GetValue(KeyboardProperty); }
            set => SetValue(KeyboardProperty, value);
        }



        public static readonly BindableProperty KeyboardProperty = BindableProperty.Create("Keyboard", typeof(Keyboard), typeof(Keyboard), Keyboard.Default);


        public int MaxLength { get=>_entry.MaxLength; set=>_entry.MaxLength=value; }



        public Entry Entry { get => _entry; }


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
            get { return (int)GetValue(BorderWidthProperty);  }
            set => SetValue(BorderWidthProperty, value);

        }



        public static readonly BindableProperty BorderWidthProperty = BindableProperty.Create("BorderWidth", typeof(int), typeof(int), 1);


        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set => SetValue(FontSizeProperty, value);

        }

        public static readonly BindableProperty FontSizeProperty = BindableProperty.Create("FontSize", typeof(double), typeof(double), Device.GetNamedSize(NamedSize.Medium,typeof(Entry)));

        public Thickness LeftIconMargin { get => _leftIconMargin; private set => _leftIconMargin = value; }
        

        public ImageSource LeftIconImage
        {
            get { return (ImageSource)GetValue(LeftIconImageProperty); }
            set => SetValue(LeftIconImageProperty, value);

        }

        public static readonly BindableProperty LeftIconImageProperty = BindableProperty.Create("LeftIconImage", typeof(ImageSource), typeof(ImageSource),ImageSource.FromFile("textboxicon.png"));


        public new bool IsFocused
        {
            get { return (bool)GetValue(IsFocusedProperty); }
           private set { SetValue(IsFocusedProperty, value); }

        }

        public new static readonly BindableProperty IsFocusedProperty = BindableProperty.Create("IsFocused", typeof(bool), typeof(bool), false);

     

      

    }
}