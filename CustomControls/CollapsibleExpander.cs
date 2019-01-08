using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace RSPLMarketSurvey.CustomControls
{
    public class CollapsibleExpander : ContentView
    {
        private static double _imageWidth = 32;
        private static double _imageHeight = 32;
        private static double _titleFontSize = 14;
        private static double _titleHeight = 50;
        private static Color _titleBackColor = Color.FromHex("#0066cc");
        private static Color _titleTextColor = Color.White;
        private static FontAttributes _titleFontAttributes = FontAttributes.Bold;
        private static Thickness _contentPadding = 5;

        StackLayout mainContainer = null;
        private bool _animating = false;
        private static double _barSapcing = 1;
        private string _expandImageBase64 = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAB+gAAAfoBF4pEbwAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAAL+SURBVFiFvZfNS1RRGMZ/54Im2tfGrxZBuggSpLiCXy2iTfS1EKMgCFGhq5NLiYYW9QeoCLpp79/QTsSNCxcSItpoTIMwOibCjBXlJPO0GEdnnBnn3nH02Z173vP+nvN5zzF4lKQa4DZwC7hw8PkvsAx8McZse83pBlov6aOkgAprRdIHSXWlANdK+iRpzwX4uPYO2tYWC78raasI8HFFJLV7hb+R9K8E8JT+SfK5hb+SlCghPKWEpJeF4Pclxc8AnlJc0r10pkmDXwYCwOlX78naAG4aY34BWGkV/nOAA1wD3qYKBpLbDfgGXDwHAwA/gUZjzHZqBHznCAe4BAzC0RQ8zxcZj8cJBoMkEglPBBftngGHx2xOxWIxdXd3y7Zt9fT0KBqNulrq4XBYXV1dsm1bvb292tvLeZAmJNVYgJ3P4uzsLKFQCIClpSUGBweJxWIn9nxzc5OBgQHW19cBWFxcZGFhIVeoAWwLuJEvWVNTE+Xl5Yfl1dVVfD4fu7u7OeMjkQiO47CxsXH4raqqisbGxnyIBgu4kre2oYGxsbEME4FAgL6+PnZ2djJiw+Ew/f39WfCJiQmqq6vzIa5a+WpSamtrY3R0NMNEKBRiaGiIaDR6CHcch62trSx4c3Pzifkt4ORJBdrb27NMrK2t4fP5WF5exnEcIpGIZzgQNZIeAp8LRQLMzMzg9/vZ398/6oFlZWy1iooKxsfHaWlpcZPygZFUT/J8dqW5uTmGh4eJx+NZdZWVlUxOTrrpOYCAOssYswl8dWugo6ODkZGRjOkoAg6wZIz5kbQivff4W9X09LRaW1tl27Y6Ozs1Pz/vNcW7o7FI3v12vWZYWVnR1NSUgsGg16YxSZl7U5Lfa5ZTaDjFTb+QVJG82193O4lF6jvQZIz5A2kXEmPMb+Ap8PsM4THgUQqeU5Je6uwupS9c2ZT0RMmFUipFJT32NFaSbEmhEsC/S7rjCZ5mokzSa0nbRYB/HLQtKwp+zEidvD9OXb0JTeGQLDMlfZ7/B3BR2BUYGsFoAAAAAElFTkSuQmCC";
        private string _collapseImageBase64 = "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAAB+gAAAfoBF4pEbwAAABl0RVh0U29mdHdhcmUAd3d3Lmlua3NjYXBlLm9yZ5vuPBoAAALiSURBVFiFvZfNS1RRGMZ/58LYorI25dQiSILSIIq7ymkRbcQ+QGRQEMKFoDYtlSAXfhA6C0VRcNFWJFfq2v9AXA0kDWkws4pGHRjTqCbwaTGO3XGae+faTM/yfc85v4f3nnPueww+JekycBdoBM4chX8AH4CYMWbH75rlQK9IGpH0Ud6KSxqWFKwEuE7SW0k/ywCf1M+juXWnhT+QlDoF+KS+SLrvF/5S0q8KwPP6JSlSLvy5pMMKwvM6lNTpBX8kKVsFeF5ZSQ+dTOOA1wIfgX/fve76DNw0xhwAWI7Ea7/w9fV1VldXyWazfqZdBV4VRJQ7bvt+ajk9PS3btmXbtrq6urS/72v6V0mXnBWIAOfKsS+JsbExFhYWjmMbGxv09PSQyWTKrcJ54IXTQHu58Gg0ysrKSlFuc3OTSCTix0QYwEi6Qm5jeGp2dpb5+XnXMQ0NDczNzVFbW+u1nICgBdieIyXGx8eL4B0dHczMzFBTU3Mci8fjdHd3k06nvZY1gG0B173g0WiU5eXlIvjAwAChUIjJyckCE4lEgt7eXnZ3d71M1FvABTf41NRUSbgxuWukqamJiYmJAhPJZJK+vj4vExctt+zS0hKLi4sFsdbWVvr7+4/heYVCIYaHh7GsP0smk0mGhobcEFjAXqnk2tpaEXxwcLAA4lRzczOjo6MF+VgshqRSiIwFfCqVbWtrIxAIABAOh13hebW0tDAyMnL8Odrb24uq5dCW5zHc3t7m4OCA+vp6V/BJpVIp0uk0jY2NpYYICBoASXHgli/Cv+u9MeZOvp4LrkOro3dw9DtWrm/bIndH/w99BW4YY3YsAGNMCoj+JzjAm3z77mxIzpLr7a9VGZ4AbhtjvoOjITHGfAOeAd+qCN8DHufhf5WkTlWvKe0oy6akp5L2KgjPSHriq1aSbEnJCsATku75gjtMBCT1SNo5BXj7aG7gVPATRoLy/zgt601Y8i/hYqaiz/PfMZCSHuQmLlgAAAAASUVORK5CYII=";
        private static ImageSource _expandImage;
        private static ImageSource _collapseImage;
       
        private static bool _autoCollapseInactiveItems = true;
        private bool _initilized = false;

        public CollapsibleExpander()
        {

            ExpandableItems = new List<ExpandableItem>();
            _expandImage = ImageSource.FromStream(
            () => new MemoryStream(Convert.FromBase64String(_expandImageBase64)));
            _collapseImage = ImageSource.FromStream(
            () => new MemoryStream(Convert.FromBase64String(_collapseImageBase64)));

        }
        public enum AnimationEnum
        {
            None = 0,
            Resize = 1,
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            init();
        }


        public void init()
        {
            try
            {
                this.Content = null;
                mainContainer = new StackLayout { Spacing = BarSapcing };
                if (ExpandableItems != null)
                {
                    int itemIndex = 0;
                    foreach (var item in ExpandableItems)
                    {

                        item._Key = itemIndex.ToString();

                        StackLayout subContainer = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand, IsClippedToBounds = true, Spacing = 0 };
                        item._SubContainer = subContainer;
                        Grid grid = new Grid { HorizontalOptions = LayoutOptions.FillAndExpand, RowSpacing = 0, ColumnSpacing = 0 };
                        grid.RowDefinitions.Add(new RowDefinition { Height = TitleHeight });
                        grid.RowDefinitions.Add(new RowDefinition { });

                        StackLayout titleBar = new StackLayout { StyleId = "titlebar_" + itemIndex.ToString(), HeightRequest = TitleHeight, BackgroundColor = TitleBackColor, Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand };

                        item._TitleBarLayout = titleBar;

                        TapGestureRecognizer titleBar_TapGestureRecognizer = new TapGestureRecognizer();
                        titleBar_TapGestureRecognizer.Tapped += TitleBar_TapGestureRecognizer_Tapped;
                        titleBar.GestureRecognizers.Add(titleBar_TapGestureRecognizer);
                        Image image = new Image { Source = CollapseImage, WidthRequest = ImageWidth, HeightRequest = ImageHeight, Margin = new Thickness(5, 0, 0, 0) };
                        item._TitleIcon = image;


                        Label TitleLabel = new Label { Text = item.Title, TextColor = TitleTextColor, FontAttributes = TitleFontAttributes, FontFamily = TitleFontFamily, FontSize = TitleFontSize, LineBreakMode = LineBreakMode.TailTruncation };
                        titleBar.Children.Add(image);
                        titleBar.Children.Add(TitleLabel);
                        StackLayout ContentLayout = new StackLayout { BackgroundColor = ContentBackgroundColor, StyleId = "content_" + itemIndex.ToString(), Padding = ContentPadding, HorizontalOptions = LayoutOptions.FillAndExpand };

                        item._ContentLayout = ContentLayout;

                        ContentLayout.SizeChanged += ContentLayout_SizeChanged;

                        ContentLayout.Children.Add(item.Content);
                        Image titleBackgroundImage = new Image { Source = TitleBackgroundImageSource, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Aspect = Aspect.AspectFill };
                        item._TitleBackgroundImage = titleBackgroundImage;
                        grid.Children.Add(titleBackgroundImage);
                        Grid.SetRow(titleBackgroundImage, 0);
                        Grid.SetColumn(titleBackgroundImage, 0);
                        grid.Children.Add(titleBar);
                        Grid.SetRow(titleBar, 0);
                        Grid.SetColumn(titleBar, 0);

                        ContentBackgroundImage = new Image { Source = ContentBackgroundImageSource, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Aspect = Aspect.AspectFill, HeightRequest = 0 };
                        item._ContentBackgroundImage = ContentBackgroundImage;


                        grid.Children.Add(ContentBackgroundImage);
                        Grid.SetColumn(ContentBackgroundImage, 0);
                        Grid.SetRow(ContentBackgroundImage, 1);
                        grid.Children.Add(ContentLayout);
                        Grid.SetColumn(ContentLayout, 0);
                        Grid.SetRow(ContentLayout, 1);
                        subContainer.Children.Add(grid);
                        mainContainer.Children.Add(subContainer);






                        itemIndex += 1;
                    }
                }
                this.Content = mainContainer;
                _initilized = true;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

   


        private void ContentLayout_SizeChanged(object sender, EventArgs e)
        {
            int itemIndex = Convert.ToInt32(((StackLayout)sender).StyleId.Split('_')[1]);
            ContentBackgroundImage.HeightRequest = ((StackLayout)sender).Height;
           // ExpandableItems.Find(f=>f._Key==itemIndex.ToString())._SubContainer.HeightRequest= ((StackLayout)sender).Height;
            if (Animating == false)
            {
                if (!_initilized)
                {

                    ExpandableItem expandableItem = ExpandableItems[itemIndex];
                    expandableItem.ItemHeight = expandableItem._ContentLayout.Height;

                    switch (ExpandMode)
                    {
                        case ExpandModeEnum.CollapseAll:
                            expandableItem._ContentLayout.IsVisible = false;
                            expandableItem.Expanded = false;
                            break;
                        case ExpandModeEnum.ExpandAll:
                            expandableItem._ContentLayout.IsVisible = true;
                            expandableItem.Expanded = true;
                            break;
                        case ExpandModeEnum.UserDefined:
                            if (expandableItem.Expanded == false)
                            {
                                expandableItem._ContentLayout.IsVisible = false;
                                expandableItem.Expanded = false;
                            }
                            else
                            {
                                expandableItem._ContentLayout.IsVisible = true;
                                expandableItem.Expanded = true;
                            }
                            break;

                    }

                }

            }

        }

        private void TitleBar_TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (Animating)
                {
                    return;
                }
                StackLayout subContainer = (StackLayout)((StackLayout)sender).Parent.Parent;
                int itemIndex = Convert.ToInt32(((StackLayout)sender).StyleId.Split('_')[1]);
                bool ContentVisible = false;



                if (AutoCollapseInactiveItems)
                {
                    foreach (var item in ExpandableItems)
                    {
                        if (Convert.ToInt32(item._Key) != itemIndex)
                        {
                            item._ContentLayout.IsVisible = false;
                            item._TitleIcon.Source = CollapseImage;
                            item.Expanded = false;
                        }
                    }
                }


                ExpandableItem expandableItem = ExpandableItems.Find(f => f._Key == itemIndex.ToString());
                ContentVisible = !expandableItem._ContentLayout.IsVisible;


                if (ContentVisible)
                {
                    ShowExpandAnimation(expandableItem._ContentLayout);


                }
                else
                {
                    ShowCollapseAnimation(expandableItem._ContentLayout);


                }

                expandableItem.Expanded = ContentVisible;



                if (expandableItem.Expanded)
                {
                    expandableItem._TitleIcon.Source = ExpandImage;
                }
                else
                {
                    expandableItem._TitleIcon.Source = CollapseImage;
                }





            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void ShowCollapseAnimation(View item)
        {
            Animating = true;
            int itemIndex = Convert.ToInt32(((StackLayout)item).StyleId.Split('_')[1]);
            switch (CollapseAnimation)
            {
                case AnimationEnum.None:
                    Animating = false;
                    break;
                case AnimationEnum.Resize:

                    double ContentHeight = ExpandableItems[itemIndex].ItemHeight;

                    double ResizeSpeed = ContentHeight / 15.00;
                    Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
                    {
                        if (item.HeightRequest <= 0)
                        {
                            Animating = false;
                            return false;
                        }
                        else
                        {
                            item.HeightRequest -= ResizeSpeed;
                            return true;
                        }
                    }

                    );
                    break;
                default:
                    break;
            }

            item.IsVisible = false;

        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName.ToUpper())
            {
                case "TITLEBACKGROUNDIMAGESOURCE":
                    if (ExpandableItems != null)
                    {
                        foreach (var item in ExpandableItems)
                        {
                            item._TitleBackgroundImage.Source = TitleBackgroundImageSource;
                        }
                    }
                    break;
                case "EXPANDIMAGE":
                    if (ExpandableItems != null)
                    {
                        foreach (var item in ExpandableItems)
                        {
                            if (item.Expanded)
                            {
                                item._TitleIcon.Source = ExpandImage;
                            }


                        }
                    }
                    break;
                case "COLLAPSEIMAGE":
                    if (ExpandableItems != null)
                    {
                        foreach (var item in ExpandableItems)
                        {
                            if (!item.Expanded)
                            {
                                item._TitleIcon.Source = CollapseImage;
                            }


                        }
                    }
                    break;
                case "CONTENTBACKGROUNDIMAGE":
                    if (ExpandableItems != null)
                    {
                        foreach (var item in ExpandableItems)
                        {


                            item._ContentBackgroundImage.Source = ContentBackgroundImageSource;



                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void ShowExpandAnimation(View item)
        {
            Animating = true;
            int itemIndex = Convert.ToInt32(((StackLayout)item).StyleId.Split('_')[1]);
            switch (ExpandAnimation)
            {
                case AnimationEnum.None:

                    item.IsVisible = true;
                    Animating = false;
                    //await item.FadeTo(1);
                    break;
                case AnimationEnum.Resize:
                    double ContentHeight = ExpandableItems[itemIndex].ItemHeight;
                    item.HeightRequest = 0;
                    item.IsVisible = true;
                    double ResizeSpeed = ContentHeight / 15.00;
                    Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
                    {
                        if (item.Height >= ContentHeight)
                        {
                            Animating = false;
                            return false;
                        }
                        else
                        {
                            item.HeightRequest += ResizeSpeed;
                            return true;
                        }
                    }

                    );

                    break;
                default:
                    break;
            }


        }



        public enum ExpandModeEnum
        {

            CollapseAll = 0,
            ExpandAll = 1,
            UserDefined = 2
        }




        public double BarSapcing { get => (double)GetValue(BarSapcingProperty); set => SetValue(BarSapcingProperty, value); }
        public static readonly BindableProperty BarSapcingProperty = BindableProperty.Create("BarSapcing", typeof(double), typeof(double), _barSapcing);
        public ImageSource CollapseImage { get =>(ImageSource)GetValue(CollapseImageProperty); set => SetValue(CollapseImageProperty, value); }
        public static readonly BindableProperty CollapseImageProperty = BindableProperty.Create("CollapseImage", typeof(ImageSource), typeof(ImageSource), _collapseImage);
        public Thickness ContentPadding { get => (Thickness)GetValue(ContentPaddingProperty); set => SetValue(ContentPaddingProperty,value); }
        public static readonly BindableProperty ContentPaddingProperty = BindableProperty.Create("ContentPadding", typeof(Thickness), typeof(Thickness), _contentPadding);
        public bool AutoCollapseInactiveItems { get => (bool)GetValue(AutoCollapseInactiveItemsProperty); set => SetValue(AutoCollapseInactiveItemsProperty,value); }
        public static readonly BindableProperty AutoCollapseInactiveItemsProperty = BindableProperty.Create("AutoCollapseInactiveItems", typeof(bool), typeof(bool), _autoCollapseInactiveItems);
        public ImageSource ExpandImage { get => (ImageSource)GetValue(ExpandImageProperty); set => SetValue(ExpandImageProperty, value); }
        public static readonly BindableProperty ExpandImageProperty = BindableProperty.Create("ExpandImage", typeof(ImageSource), typeof(ImageSource), _expandImage);
        public double ImageHeight { get => (double)GetValue(ImageHeightProperty); set => SetValue(ImageHeightProperty,value); }
        public static readonly BindableProperty ImageHeightProperty = BindableProperty.Create("ImageHeight", typeof(double), typeof(double), _imageHeight);
        public double ImageWidth { get => (double)GetValue(ImageWidthProperty); set => SetValue(ImageWidthProperty,value); }
        public static readonly BindableProperty ImageWidthProperty = BindableProperty.Create("ImageWidth", typeof(double), typeof(double), _imageWidth);
        public Color TitleBackColor { get => (Color)GetValue(TitleBackColorProperty); set => SetValue(TitleBackColorProperty,value); }
        public static readonly BindableProperty TitleBackColorProperty = BindableProperty.Create("TitleBackColor", typeof(Color), typeof(Color), _titleBackColor);
        public FontAttributes TitleFontAttributes { get => (FontAttributes)GetValue(TitleFontAttributesProperty); set => SetValue(TitleFontAttributesProperty, value); }
        public static readonly BindableProperty TitleFontAttributesProperty = BindableProperty.Create("TitleFontAttributes", typeof(FontAttributes), typeof(FontAttributes), _titleFontAttributes);
        public double TitleFontSize { get => (double)GetValue(TitleFontSizeProperty); set => SetValue(TitleFontSizeProperty, value); }
        public static readonly BindableProperty TitleFontSizeProperty = BindableProperty.Create("TitleFontSize", typeof(double), typeof(double), _titleFontSize);
        public double TitleHeight { get => (double)GetValue(TitleHeightProperty); set => SetValue(TitleHeightProperty,value); }
        public static readonly BindableProperty TitleHeightProperty = BindableProperty.Create("TitleHeight", typeof(double), typeof(double), _titleHeight);
        public Color TitleTextColor { get => (Color)GetValue(TitleTextColorProperty); set => SetValue(TitleTextColorProperty,value); }
        public static readonly BindableProperty TitleTextColorProperty = BindableProperty.Create("TitleTextColor", typeof(Color), typeof(Color), _titleTextColor);
        public ExpandModeEnum ExpandMode { get =>(ExpandModeEnum)GetValue(ExpandModeProperty); set=>SetValue(ExpandModeProperty,value); }
        public static readonly BindableProperty ExpandModeProperty = BindableProperty.Create("ExpandMode", typeof(ExpandModeEnum), typeof(ExpandModeEnum), ExpandModeEnum.CollapseAll);
        public string Title { get; set; }

        public List<ExpandableItem> ExpandableItems { get; set; }

        public string TitleFontFamily { get=>(string)GetValue(TitleFontFamilyProperty); set=>SetValue(TitleFontFamilyProperty,value); }
        public static readonly BindableProperty TitleFontFamilyProperty = BindableProperty.Create("TitleFontFamily", typeof(string), typeof(string), "Arial");
        public AnimationEnum ExpandAnimation { get=>(AnimationEnum)GetValue(ExpandAnimationProperty); set=>SetValue(ExpandAnimationProperty,value); }
        public static readonly BindableProperty ExpandAnimationProperty = BindableProperty.Create("ExpandAnimation", typeof(AnimationEnum), typeof(AnimationEnum), AnimationEnum.Resize);
        public AnimationEnum CollapseAnimation { get=>(AnimationEnum)GetValue(CollapseAnimationProperty); set=>SetValue(CollapseAnimationProperty,value); }
        public static readonly BindableProperty CollapseAnimationProperty = BindableProperty.Create("CollapseAnimation", typeof(AnimationEnum), typeof(AnimationEnum), AnimationEnum.Resize);
        public Color ContentBackgroundColor { get=>(Color)GetValue(ContentBackgroundColorProperty); set=>SetValue(ContentBackgroundColorProperty,value); }
        public static readonly BindableProperty ContentBackgroundColorProperty = BindableProperty.Create("ContentBackgroundColor", typeof(Color), typeof(Color), Color.Transparent);
        public ImageSource ContentBackgroundImageSource { get=>(ImageSource)GetValue(ContentBackgroundImageSourceProperty); set=>SetValue(ContentBackgroundImageSourceProperty,value); }
        public static readonly BindableProperty ContentBackgroundImageSourceProperty = BindableProperty.Create("ContentBackgroundImageSource", typeof(ImageSource), typeof(ImageSource), null);
        public ImageSource TitleBackgroundImageSource { get=>(ImageSource)GetValue(TitleBackgroundImageSourceProperty); set=>SetValue(TitleBackgroundImageSourceProperty,value); }
        public static readonly BindableProperty TitleBackgroundImageSourceProperty = BindableProperty.Create("TitleBackgroundImageSource", typeof(ImageSource), typeof(ImageSource), null);



        public Image ContentBackgroundImage { get; private set; }
        private View ContentToAnimate { get; set; }
        private bool Animating { get => _animating; set => _animating = value; }
    }

    public class ExpandableItem
    {
        private bool _Expanded = false;
        internal string _Key;
        public string Title { get; set; }
        internal Image _TitleIcon { get; set; }
        internal Image _ContentBackgroundImage { get; set; }
        internal StackLayout _ContentLayout { get; set; }
        internal StackLayout _TitleBarLayout { get; set; }
        public View Content { get; set; }
        public double ItemHeight { get; set; }
        public bool Expanded { get => _Expanded; set { _Expanded = value; _ContentBackgroundImage.IsVisible = _Expanded; } }

        public Image _TitleBackgroundImage { get; internal set; }
        public StackLayout _SubContainer { get; internal set; }
    }
}
