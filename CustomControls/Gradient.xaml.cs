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
    public partial class Gradient : ContentView
    {



        public enum BorderTypeEnum
        {
            All = 0,
            Left = 1,
            Top = 2,
            Right = 3,
            Bottom = 4,
            LeftRight = 5,
            TopBottom = 6,
            LeftTopRight = 7,
            TopRightBottom = 8,
            RightBottomLeft = 9,
            BottomLeftTop = 10,
            None = 11
        }
        public enum ColorPaletteEnum
        {
            RGB = 0,
            HSLA = 1
        }
        public enum GradientOriantationEnum
        {
            None = 0,
            Horizontal = 1,
            Vertical = 2,
            Center = 3,


        }
        public Gradient()
        {

            InitializeComponent();
        }





        private void GenerateGradient()
        {
            GradientContainer.Children.Clear();
            if (GradientOriantation != GradientOriantationEnum.None)
            {
                Color GradientColor = StartColor;
                switch (ColorPalette)
                {
                    case ColorPaletteEnum.RGB:
                        double sr = GradientColor.R;
                        double sg = GradientColor.G;
                        double sb = GradientColor.B;
                        double sa = GradientColor.A;

                        double r = sr;
                        double g = sg;
                        double b = sb;
                        switch (GradientOriantation)
                        {
                            case GradientOriantationEnum.Horizontal:

                                for (int i = 0; i < Width; i += Size)
                                {

                                    for (int j = 0; j < Size; j++)
                                    {
                                        r += (EndColor.R - sr) / Width;
                                        g += (EndColor.G - sg) / Width;
                                        b += (EndColor.B - sb) / Width;


                                    }
                                    GradientColor = Color.FromRgb(r, g, b);
                                    double boxWidth = Size;
                                    if (i + Size > Width)
                                    {
                                        boxWidth = Width - i;
                                    }
                                    BoxView bvLine = new BoxView { HeightRequest = Height, WidthRequest = boxWidth, BackgroundColor = GradientColor };
                                    GradientContainer.Children.Add(bvLine);
                                    AbsoluteLayout.SetLayoutBounds(bvLine, Rectangle.FromLTRB(i, 0, i + boxWidth, Height));
                                }
                                break;
                            case GradientOriantationEnum.Vertical:

                                for (int i = 0; i < Height; i += Size)
                                {


                                    for (int j = 0; j < Size; j++)
                                    {
                                        r += (EndColor.R - (sr)) / Height;
                                        g += (EndColor.G - (sg)) / Height;
                                        b += (EndColor.B - (sb)) / Height;

                                    }

                                    GradientColor = Color.FromRgb(r, g, b);

                                    double boxHeight = Size;
                                    if (i + Size > Height)
                                    {
                                        boxHeight = Height - i;
                                    }
                                    BoxView bvLine = new BoxView { HeightRequest = boxHeight, WidthRequest = Width, BackgroundColor = GradientColor };
                                    GradientContainer.Children.Add(bvLine);
                                    AbsoluteLayout.SetLayoutBounds(bvLine, Rectangle.FromLTRB(0, i, Width, i + boxHeight));


                                }

                                break;
                            case GradientOriantationEnum.Center:

                                for (int i = 0; i < (Height + Width) / 4; i += Size)
                                {



                                    for (int j = 0; j < Size; j++)
                                    {
                                        r += (EndColor.R - sr) / ((Height + Width) / 4);
                                        g += (EndColor.G - sg) / ((Height + Width) / 4);
                                        b += (EndColor.B - sb) / ((Height + Width) / 4);

                                    }

                                    GradientColor = Color.FromRgb(r, g, b);

                                    BoxView bvLine = new BoxView { HeightRequest = Height - i, WidthRequest = Width - i, Color = GradientColor };
                                    GradientContainer.Children.Add(bvLine);
                                    AbsoluteLayout.SetLayoutBounds(bvLine, Rectangle.FromLTRB(i, i, Width - i, Height - i));



                                }

                                break;

                        }
                        break;
                    case ColorPaletteEnum.HSLA:
                        double sh = GradientColor.Hue;
                        double ss = GradientColor.Saturation;
                        double sl = GradientColor.Luminosity;


                        double h = sh;
                        double s = ss;
                        double l = sl;
                        switch (GradientOriantation)
                        {
                            case GradientOriantationEnum.Horizontal:

                                for (int i = 0; i < Width; i += Size)
                                {

                                    for (int j = 0; j < Size; j++)
                                    {
                                        h += (EndColor.R - sh) / Width;
                                        s += (EndColor.G - ss) / Width;
                                        l += (EndColor.B - sl) / Width;


                                    }
                                    GradientColor = Color.FromHsla(h, s, l);
                                    double boxWidth = Size;
                                    if (i + Size > Width)
                                    {
                                        boxWidth = Width - i;
                                    }
                                    BoxView bvLine = new BoxView { HeightRequest = Height, WidthRequest = boxWidth, BackgroundColor = GradientColor };
                                    GradientContainer.Children.Add(bvLine);
                                    AbsoluteLayout.SetLayoutBounds(bvLine, Rectangle.FromLTRB(i, 0, i + boxWidth, Height));
                                }
                                break;
                            case GradientOriantationEnum.Vertical:

                                for (int i = 0; i < Height; i += Size)
                                {


                                    for (int j = 0; j < Size; j++)
                                    {
                                        h += (EndColor.R - (sh)) / Height;
                                        s += (EndColor.G - (ss)) / Height;
                                        l += (EndColor.B - (sl)) / Height;

                                    }

                                    GradientColor = Color.FromHsla(h, s, l);
                                    double boxHeight = Size;
                                    if (i + Size > Height)
                                    {
                                        boxHeight = Height - i;
                                    }

                                    BoxView bvLine = new BoxView { HeightRequest = boxHeight, WidthRequest = Width, BackgroundColor = GradientColor };
                                    GradientContainer.Children.Add(bvLine);
                                    AbsoluteLayout.SetLayoutBounds(bvLine, Rectangle.FromLTRB(0, i, Width, i + boxHeight));


                                }

                                break;
                            case GradientOriantationEnum.Center:

                                for (int i = 0; i < (Height + Width) / 4; i += Size)
                                {



                                    for (int j = 0; j < Size; j++)
                                    {
                                        h += (EndColor.R - sh) / ((Height + Width) / 4);
                                        s += (EndColor.G - ss) / ((Height + Width) / 4);
                                        l += (EndColor.B - sl) / ((Height + Width) / 4);

                                    }

                                    GradientColor = Color.FromHsla(h, s, l);

                                    BoxView bvLine = new BoxView { HeightRequest = Height - i, WidthRequest = Width - i, Color = GradientColor };
                                    GradientContainer.Children.Add(bvLine);
                                    AbsoluteLayout.SetLayoutBounds(bvLine, Rectangle.FromLTRB(i, i, Width - i, Height - i));



                                }

                                break;

                        }
                        break;

                }
            }

            /////////////////////////////////////background image///////////////////////////////////////
            if (BackGroundImage != null)
            {
                double PatternWidth = Width / RepeatColumns;
                double PatternHeight = Height / RepeatRows;
                Image image = new Image { Source = BackGroundImage, WidthRequest = PatternWidth, HeightRequest = PatternHeight };
                switch (RepeatDirection)
                {
                    case RepeatDirectionEnum.None:

                        GradientContainer.Children.Add(image);
                        AbsoluteLayout.SetLayoutBounds(image, Rectangle.FromLTRB(0, 0, PatternWidth, PatternHeight));

                        break;
                    case RepeatDirectionEnum.RepeatHorizontal:

                        for (int j = 0; j < Width; j += Convert.ToInt32(PatternWidth))
                        {
                            Image BGimage = new Image { Source = BackGroundImage, Aspect = Aspect.Fill };
                            GradientContainer.Children.Add(BGimage);
                            AbsoluteLayout.SetLayoutBounds(BGimage, Rectangle.FromLTRB(j, 0, j + PatternWidth, PatternHeight));
                        }

                        break;

                    case RepeatDirectionEnum.RepeatVertical:
                        for (int i = 0; i < Height; i += Convert.ToInt32(PatternHeight))
                        {

                            Image BGimage = new Image { Source = BackGroundImage, Aspect = Aspect.Fill };
                            GradientContainer.Children.Add(BGimage);
                            AbsoluteLayout.SetLayoutBounds(BGimage, Rectangle.FromLTRB(0, i, PatternWidth, i + PatternHeight));

                        }
                        break;
                    case RepeatDirectionEnum.RepeatBoth:
                        for (int i = 0; i < Height; i += Convert.ToInt32(PatternHeight))
                        {
                            for (int j = 0; j < Width; j += Convert.ToInt32(PatternWidth))
                            {
                                Image BGimage = new Image { Source = BackGroundImage, Aspect = Aspect.Fill };
                                GradientContainer.Children.Add(BGimage);
                                AbsoluteLayout.SetLayoutBounds(BGimage, Rectangle.FromLTRB(j, i, j + PatternWidth, i + PatternHeight));
                            }
                        }
                        break;
                    default:
                        break;
                }
            }



            if (BorderWidth > 0)
            {
                if (BorderType == BorderTypeEnum.All || BorderType == BorderTypeEnum.Left || BorderType == BorderTypeEnum.LeftRight || BorderType == BorderTypeEnum.LeftTopRight || BorderType == BorderTypeEnum.RightBottomLeft || BorderType == BorderTypeEnum.BottomLeftTop)
                {
                    BoxView bvLeft = new BoxView { WidthRequest = BorderWidth, BackgroundColor = BorderColor, HeightRequest = Height };
                    GradientContainer.Children.Add(bvLeft);
                    AbsoluteLayout.SetLayoutBounds(bvLeft, Rectangle.FromLTRB(0, 0, BorderWidth, Height));
                }
                if (BorderType == BorderTypeEnum.All || BorderType == BorderTypeEnum.Top || BorderType == BorderTypeEnum.TopBottom || BorderType == BorderTypeEnum.TopRightBottom || BorderType == BorderTypeEnum.BottomLeftTop || BorderType == BorderTypeEnum.LeftTopRight)
                {
                    BoxView bvTop = new BoxView { WidthRequest = Width, BackgroundColor = BorderColor, HeightRequest = BorderWidth };
                    GradientContainer.Children.Add(bvTop);
                    AbsoluteLayout.SetLayoutBounds(bvTop, Rectangle.FromLTRB(0, 0, Width, BorderWidth));
                }

                if (BorderType == BorderTypeEnum.All || BorderType == BorderTypeEnum.Right || BorderType == BorderTypeEnum.LeftRight || BorderType == BorderTypeEnum.RightBottomLeft || BorderType == BorderTypeEnum.LeftTopRight || BorderType == BorderTypeEnum.TopRightBottom)
                {
                    BoxView bvRight = new BoxView { WidthRequest = BorderWidth, BackgroundColor = BorderColor, HeightRequest = Height };
                    GradientContainer.Children.Add(bvRight);
                    AbsoluteLayout.SetLayoutBounds(bvRight, Rectangle.FromLTRB(Width - BorderWidth, 0, Width, Height));
                }

                if (BorderType == BorderTypeEnum.All || BorderType == BorderTypeEnum.Bottom || BorderType == BorderTypeEnum.TopBottom || BorderType == BorderTypeEnum.BottomLeftTop || BorderType == BorderTypeEnum.TopRightBottom || BorderType == BorderTypeEnum.RightBottomLeft)
                {
                    BoxView bvBottom = new BoxView { WidthRequest = Width, BackgroundColor = BorderColor, HeightRequest = BorderWidth };
                    GradientContainer.Children.Add(bvBottom);
                    AbsoluteLayout.SetLayoutBounds(bvBottom, Rectangle.FromLTRB(0, Height - BorderWidth, Width - BorderWidth, Height));
                }







            }





        }



        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName.ToUpper())
            {
                case "WIDTH":
                case "HEIGHT":
                    if (Height > 0 && Width > 0)
                    {
                        GenerateGradient();
                    }
                    break;


            }
        }

        public static BindableProperty StartColorProperty = BindableProperty.Create("StartColor", typeof(Color), typeof(Color), Color.Red);
        public Color StartColor { get => (Color)GetValue(StartColorProperty); set => SetValue(StartColorProperty, value); }

        public static BindableProperty EndColorProperty = BindableProperty.Create("EndColor", typeof(Color), typeof(Color), Color.White);
        public Color EndColor { get => (Color)GetValue(EndColorProperty); set => SetValue(EndColorProperty, value); }

        public static BindableProperty GradientOriantationProperty = BindableProperty.Create("GradientOriantation", typeof(GradientOriantationEnum), typeof(GradientOriantationEnum), GradientOriantationEnum.None);
        public GradientOriantationEnum GradientOriantation { get => (GradientOriantationEnum)GetValue(GradientOriantationProperty); set => SetValue(GradientOriantationProperty, value); }

        public static BindableProperty SizeProperty = BindableProperty.Create("Size", typeof(int), typeof(int), 12);
        public int Size { get => (int)GetValue(SizeProperty); set => SetValue(SizeProperty, value); }
        public static BindableProperty ColorPaletteProperty = BindableProperty.Create("ColorPalette", typeof(ColorPaletteEnum), typeof(ColorPaletteEnum), ColorPaletteEnum.RGB);
        public ColorPaletteEnum ColorPalette { get => (ColorPaletteEnum)GetValue(ColorPaletteProperty); set => SetValue(ColorPaletteProperty, value); }
        public static BindableProperty BorderWidthProperty = BindableProperty.Create("BorderWidth", typeof(int), typeof(int), 0);
        public int BorderWidth { get => (int)GetValue(BorderWidthProperty); set => SetValue(BorderWidthProperty, value); }
        public static BindableProperty BorderColorProperty = BindableProperty.Create("BorderColor", typeof(Color), typeof(Color), Color.Transparent);
        public Color BorderColor { get => (Color)GetValue(BorderColorProperty); set => SetValue(BorderColorProperty, value); }
        public static BindableProperty BorderTypeProperty = BindableProperty.Create("BorderType", typeof(BorderTypeEnum), typeof(BorderTypeEnum), BorderTypeEnum.None);
        public BorderTypeEnum BorderType { get => (BorderTypeEnum)GetValue(BorderTypeProperty); set => SetValue(BorderTypeProperty, value); }
        public static BindableProperty BackGroundImageProperty = BindableProperty.Create("BackGroundImage", typeof(ImageSource), typeof(ImageSource), null);
        public ImageSource BackGroundImage { get => (ImageSource)GetValue(BackGroundImageProperty); set => SetValue(BackGroundImageProperty, value); }


        public enum RepeatDirectionEnum
        {
            None = 0,
            RepeatHorizontal = 1,
            RepeatVertical = 2,
            RepeatBoth = 3

        }

        public static BindableProperty RepeatDirectionProperty = BindableProperty.Create("RepeatDirection", typeof(RepeatDirectionEnum), typeof(RepeatDirectionEnum), RepeatDirectionEnum.None);
        public RepeatDirectionEnum RepeatDirection { get => (RepeatDirectionEnum)GetValue(RepeatDirectionProperty); set => SetValue(RepeatDirectionProperty, value); }


        public static BindableProperty RepeatColumnsProperty = BindableProperty.Create("RepeatColumns", typeof(int), typeof(int), 0);
        public int RepeatColumns { get => (int)GetValue(RepeatColumnsProperty); set => SetValue(RepeatColumnsProperty, value); }

        public static BindableProperty RepeatRowsProperty = BindableProperty.Create("RepeatRows", typeof(int), typeof(int), 0);
      

        public int RepeatRows { get => (int)GetValue(RepeatRowsProperty); set => SetValue(RepeatRowsProperty, value); }




    }
}



//public partial class Gradient : ContentView
//{
//    public enum GradientOriantationEnum
//    {
//        Horizontal = 0,
//        Vertical = 1,
//        Center = 2

//    }
//    public Gradient()
//    {
//        InitializeComponent();
//        StartColor = Color.Red;
//        EndColor = Color.White;
//        GradientOriantation = GradientOriantationEnum.Horizontal;
//        Size = 10;
//        BackgroundColor = StartColor;
//    }




//    private  void GenerateGradientAsync()
//    {
//        GradientContainer.Children.Clear();
//        Color GradientColor = StartColor;
//        double sr = GradientColor.R;
//        double sg = GradientColor.G;
//        double sb = GradientColor.B;
//        double sa = GradientColor.A;

//        double r = sr;
//        double g = sg;
//        double b = sb;
//        switch (GradientOriantation)
//        {
//            case GradientOriantationEnum.Horizontal:

//                for (int i = 0; i < Width; i += Size)
//                {

//                    for (int j = 0; j < Size; j++)
//                    {
//                        r += (EndColor.R - sr) / Width;
//                        g += (EndColor.G - sg) / Width;
//                        b += (EndColor.B - sb) / Width;


//                    }
//                    GradientColor = Color.FromRgb(r, g, b);

//                    BoxView bvLine = new BoxView { HeightRequest = Height, WidthRequest = Size, BackgroundColor = GradientColor };
//                    GradientContainer.Children.Add(bvLine);
//                    AbsoluteLayout.SetLayoutBounds(bvLine, Rectangle.FromLTRB(i, 0, i + Size, Height));
//                }
//                break;
//            case GradientOriantationEnum.Vertical:

//                for (int i = 0; i < Height; i += Size)
//                {


//                    for (int j = 0; j < Size; j++)
//                    {
//                        r += (EndColor.R - (sr)) / Height;
//                        g += (EndColor.G - (sg)) / Height;
//                        b += (EndColor.B - (sb)) / Height;

//                    }

//                    GradientColor = Color.FromRgb(r, g, b);


//                    BoxView bvLine = new BoxView { HeightRequest = Size, WidthRequest = Width, BackgroundColor = GradientColor };
//                    GradientContainer.Children.Add(bvLine);
//                    AbsoluteLayout.SetLayoutBounds(bvLine, Rectangle.FromLTRB(0, i, Width, i + Size));


//                }

//                break;
//            case GradientOriantationEnum.Center:

//                for (int i = 0; i < (Height + Width) / 4; i += Size)
//                {



//                    for (int j = 0; j < Size; j++)
//                    {
//                        r += (EndColor.R - sr) / ((Height + Width) / 4);
//                        g += (EndColor.G - sg) / ((Height + Width) / 4);
//                        b += (EndColor.B - sb) / ((Height + Width) / 4);

//                    }

//                    GradientColor = Color.FromRgb(r, g, b);

//                    BoxView bvLine = new BoxView { HeightRequest = Height - i, WidthRequest = Width - i, Color = GradientColor };
//                    GradientContainer.Children.Add(bvLine);
//                    AbsoluteLayout.SetLayoutBounds(bvLine, Rectangle.FromLTRB(i, i, Width - i, Height- i));



//                }




//                break;
//            default:
//                break;
//        }



//    }

//    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
//    {
//        base.OnPropertyChanged(propertyName);
//        switch (propertyName.ToUpper())
//        {
//            case "WIDTH":
//            case "HEIGHT":
//                if (Height > 0 && Width > 0)
//                {
//                    GenerateGradientAsync();
//                }
//                break;


//        }
//    }

//    public Color StartColor { get; set; }
//    public Color EndColor { get; set; }

//    public GradientOriantationEnum GradientOriantation { get; set; }
//    public int Size { get; set; }

//}