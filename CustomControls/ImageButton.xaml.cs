using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RSPLMarketSurvey.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageButton : ContentView
    {


        public event EventHandler Clicked;

        protected override void OnParentSet()
        {
            base.OnParentSet();
            this.NormalImage = this.NormalImage == null ? "imagebutton.png" : this.NormalImage;
            this.PressedImage = this.PressedImage == null ? "imagebutton_pressed.png" : this.PressedImage;
            if (this.IsEnabled == false)
            {
                ImageButtonImage.Source = PressedImage;
            }
            else
            {
                ImageButtonImage.Source = NormalImage;
            }

            ImageButtonLabel.TextColor = TextColor;
            ImageButtonLabel.FontAttributes = FontAttributes;
            ImageButtonLabel.FontSize = FontSize;
            ImageButtonLabel.FontFamily = FontFamily;

        }
        protected virtual void OnClicked(EventArgs e)
        {
            Clicked?.Invoke(this, e);
        }
        public ImageButton()
        {
            InitializeComponent();
            TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
            ImageButtonImage.GestureRecognizers.Add(tapGestureRecognizer);
            ImageButtonLabel.GestureRecognizers.Add(tapGestureRecognizer);


        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName.ToUpper())
            {
                case "ISENABLED":
                    if (this.IsEnabled == false)
                    {
                        ImageButtonImage.Source = PressedImage;
                    }
                    else
                    {
                        ImageButtonImage.Source = NormalImage;
                    }
                    break;
                case "ASPECT":
                    ImageButtonImage.Aspect = this.Aspect;
                    break;
            }

        }
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ImageButtonImage.Source = PressedImage;
            OnClicked(null);
            Device.StartTimer(TimeSpan.FromMilliseconds(50), () =>
            {
                ImageButtonImage.Source = NormalImage;
                return false;
            });


        }

        public static BindableProperty PressedImageProperty = BindableProperty.Create(propertyName: "PressedImage", returnType: typeof(ImageSource), declaringType: typeof(ImageSource), defaultValue: ImageSource.FromFile("imagebutton_pressed.png"));
        public ImageSource PressedImage { get => (ImageSource)GetValue(PressedImageProperty); set => SetValue(PressedImageProperty, value); }


        public static BindableProperty NormalImageProperty = BindableProperty.Create(propertyName: "NormalImage", returnType: typeof(ImageSource), declaringType: typeof(ImageSource), defaultValue: ImageSource.FromFile("imagebutton.png"));
        public ImageSource NormalImage { get => (ImageSource)GetValue(NormalImageProperty); set => SetValue(NormalImageProperty, value); }



        public static BindableProperty TextColorProperty = BindableProperty.Create(propertyName: "TextColor", returnType: typeof(Color), declaringType: typeof(Color), defaultValue: Color.Black);
        public Color TextColor { get => (Color)GetValue(TextColorProperty); set => SetValue(TextColorProperty, value); }



        public static BindableProperty FontAttributesProperty = BindableProperty.Create(propertyName: "FontAttributes", returnType: typeof(FontAttributes), declaringType: typeof(FontAttributes), defaultValue: FontAttributes.None);
        public FontAttributes FontAttributes { get => (FontAttributes)GetValue(FontAttributesProperty); set => SetValue(FontAttributesProperty, value); }


        public static BindableProperty FontSizeProperty = BindableProperty.Create(propertyName: "FontSize", returnType: typeof(double), declaringType: typeof(double), defaultValue: 10.00);
        public double FontSize { get => (double)GetValue(FontSizeProperty); set => SetValue(FontSizeProperty, value); }


        public static BindableProperty FontFamilyProperty = BindableProperty.Create(propertyName: "FontFamily", returnType: typeof(string), declaringType: typeof(string), defaultValue: "Arial");
        public string FontFamily { get => (string)GetValue(FontFamilyProperty); set => SetValue(FontFamilyProperty, value); }

        public string Text { get => ImageButtonLabel.Text; set => ImageButtonLabel.Text = value; }
        public object CommandParameter { get; internal set; }

        public static BindableProperty AspectProperty = BindableProperty.Create(propertyName: "Aspect", returnType: typeof(Aspect), declaringType: typeof(Aspect), defaultValue: Aspect.AspectFill);
        public Aspect Aspect { get => (Aspect)GetValue(AspectProperty); set => SetValue(AspectProperty, value); }
        public object Command { get; internal set; }
    }


}