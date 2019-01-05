using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace RSPLMarketSurvey.CustomControls
{
    public class CollapsibleExpander : ContentView
    {
        private double _imageWidth = 32;
        private double _imageHeight = 32;
        private double _titleFontSize = 14;
        private double _titleHeight = 50;
        private Color _titleBackColor = Color.DarkGray;
        private Color _titleTextColor = Color.White;
        private FontAttributes _titleFontAttributes = FontAttributes.Bold;
        private Thickness _contentPadding = 5;
        private bool _displayOnlySingleContent = true;
        StackLayout mainContainer = null;
        private bool _animating = false;

        public CollapsibleExpander()
        {

            ExpandableItems = new List<ExpandableItem>();

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
            this.Content = null;
            mainContainer = new StackLayout { };
            if (ExpandableItems != null)
            {
                int itemIndex = 0;
                foreach (var item in ExpandableItems)
                {

                    item._Key = itemIndex.ToString();

                    StackLayout subContainer = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand, IsClippedToBounds = true };
                    StackLayout titleBar = new StackLayout { StyleId = "titlebar_" + itemIndex.ToString(), HeightRequest = TitleHeight, BackgroundColor = TitleBackColor, Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand };

                    item._TitleBarLayout = titleBar;

                    TapGestureRecognizer titleBar_TapGestureRecognizer = new TapGestureRecognizer();
                    titleBar_TapGestureRecognizer.Tapped += TitleBar_TapGestureRecognizer_Tapped;
                    titleBar.GestureRecognizers.Add(titleBar_TapGestureRecognizer);
                    Image image = new Image { Source = CollapseImage, WidthRequest = ImageWidth, HeightRequest = ImageHeight };
                    item._TitleIcon = image;


                    Label TitleLabel = new Label { Text = item.Title, TextColor = TitleTextColor, FontAttributes = TitleFontAttributes, FontFamily = TitleFontFamily, FontSize = TitleFontSize, LineBreakMode = LineBreakMode.TailTruncation };
                    titleBar.Children.Add(image);
                    titleBar.Children.Add(TitleLabel);
                    StackLayout ContentLayout = new StackLayout { StyleId = "content_" + itemIndex.ToString(), Padding = ContentPadding, HorizontalOptions = LayoutOptions.FillAndExpand };

                    item._ContentLayout = ContentLayout;

                    ContentLayout.SizeChanged += ContentLayout_SizeChanged;

                    ContentLayout.Children.Add(item.Content);


                    subContainer.Children.Add(titleBar);
                    subContainer.Children.Add(ContentLayout);
                    mainContainer.Children.Add(subContainer);

                    
                    
                    
                    

                    itemIndex += 1;
                }
            }
            this.Content = mainContainer;
        }

        private void ContentLayout_SizeChanged(object sender, EventArgs e)
        {
            if (Animating == false)
            {
                int itemIndex = Convert.ToInt32(((StackLayout)sender).StyleId.Split('_')[1]);
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

        private void TitleBar_TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (Animating)
                {
                    return;
                }
                StackLayout subContainer = (StackLayout)((StackLayout)sender).Parent;
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
                            item.Expanded = true;
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

     
        public ExpandModeEnum ExpandMode { get; set; }

        public bool AutoCollapseInactiveItems { get => _displayOnlySingleContent; set => _displayOnlySingleContent = value; }

        public string Title { get; set; }
        public ImageSource CollapseImage { get; set; }
        public ImageSource ExpandImage { get; set; }
        public List<ExpandableItem> ExpandableItems { get; set; }
        public double TitleHeight { get => _titleHeight; set => _titleHeight = value; }
        public Color TitleBackColor { get => _titleBackColor; set => _titleBackColor = value; }
        public double ImageWidth { get => _imageWidth; set => _imageWidth = value; }
        public double ImageHeight { get => _imageHeight; set => _imageHeight = value; }

        public FontAttributes TitleFontAttributes { get => _titleFontAttributes; set => _titleFontAttributes = value; }
        public string TitleFontFamily { get; set; }
        public double TitleFontSize { get => _titleFontSize; set => _titleFontSize = value; }
        public Color TitleTextColor { get => _titleTextColor; set => _titleTextColor = value; }
        public Thickness ContentPadding { get => _contentPadding; set => _contentPadding = value; }

        public AnimationEnum ExpandAnimation { get; set; }
        public AnimationEnum CollapseAnimation { get; set; }
        private View ContentToAnimate { get; set; }
        private bool Animating { get => _animating; set => _animating = value; }
    }

    public class ExpandableItem
    {
        private bool _Expanded = false;
        internal string _Key;
        public string Title { get; set; }
        internal Image _TitleIcon { get; set; }
        internal StackLayout _ContentLayout { get; set; }
        internal StackLayout _TitleBarLayout { get; set; }
        public View Content { get; set; }
        public double ItemHeight { get; set; }
        public bool Expanded { get => _Expanded; set => _Expanded = value; }
    }
}
