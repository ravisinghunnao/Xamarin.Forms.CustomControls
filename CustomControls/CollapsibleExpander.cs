using System;
using System.Collections.Generic;
using System.Text;
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
        private bool _displayOnlySingleContent=true;
        StackLayout mainContainer =null;
        public CollapsibleExpander()
        {

            ExpandableItems = new List<ExpandableItem>();

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
                    StackLayout subContainer = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand };
                    StackLayout titleBar = new StackLayout { StyleId = "titlebar_" + itemIndex.ToString(), HeightRequest = TitleHeight, BackgroundColor = TitleBackColor, Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand };
                    TapGestureRecognizer titleBar_TapGestureRecognizer = new TapGestureRecognizer();
                    titleBar_TapGestureRecognizer.Tapped += TitleBar_TapGestureRecognizer_Tapped;
                    titleBar.GestureRecognizers.Add(titleBar_TapGestureRecognizer);
                    Image image = new Image { Source = CollapseImage, WidthRequest = ImageWidth, HeightRequest = ImageHeight };
                    Label TitleLabel = new Label { Text = item.Title, TextColor = TitleTextColor, FontAttributes = TitleFontAttributes, FontFamily = TitleFontFamily, FontSize = TitleFontSize, LineBreakMode = LineBreakMode.TailTruncation };
                    titleBar.Children.Add(image);
                    titleBar.Children.Add(TitleLabel);
                    StackLayout ContentLayout = new StackLayout { StyleId = "content_" + itemIndex.ToString(), Padding = ContentPadding, IsVisible = item.Expanded, HorizontalOptions = LayoutOptions.FillAndExpand };
                    if (ExpandMode != ExpandModeEnum.UserDefined)
                    {
                        switch (ExpandMode)
                        {
                            case ExpandModeEnum.CollapseAll:
                                ContentLayout.IsVisible = false;
                                break;
                            case ExpandModeEnum.ExpandAll:
                                ContentLayout.IsVisible = true;
                                break;

                        }
                    }
                    ContentLayout.Children.Add(item.Content);


                    subContainer.Children.Add(titleBar);
                    subContainer.Children.Add(ContentLayout);
                    mainContainer.Children.Add(subContainer);
                    itemIndex += 1;
                }
            }
            this.Content = mainContainer;
        }

        private void TitleBar_TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                StackLayout subContainer = (StackLayout)((StackLayout)sender).Parent;
                int itemIndex = Convert.ToInt32(((StackLayout)sender).StyleId.Split('_')[1]);
                bool ContentVisible = false;
                foreach (var item in subContainer.Children)
                {
                    if (item.StyleId == "content_" + itemIndex.ToString())
                    {
                        item.IsVisible = !item.IsVisible;
                        ContentVisible = item.IsVisible;
                        ExpandableItems[itemIndex].Expanded = item.IsVisible;
                        break;
                    }

                }

                foreach (var item in subContainer.Children)
                {

                    if (item.StyleId == "titlebar_" + itemIndex.ToString())
                    {
                        StackLayout titleBar = (StackLayout)item;
                        foreach (var titleview in titleBar.Children)
                        {
                            if (titleview is Image img)
                            {
                                if (ContentVisible)
                                {
                                    img.Source = ExpandImage;
                                }
                                else
                                {
                                    img.Source = CollapseImage;
                                }
                                break;
                            }
                        }

                    }
                }

                if (DisplayOnlySingleContent)
                {
                    foreach (var item in mainContainer.Children)
                    {
                        if(item is StackLayout subcont)
                        {
                            foreach (StackLayout sl in subcont.Children)
                            {
                                if (sl.StyleId.Contains("content_"))
                                {
                                    if(sl.StyleId!= "content_" + itemIndex.ToString())
                                    {
                                        sl.IsVisible = false;
                                        ExpandableItems[Convert.ToInt32(sl.StyleId.Split('_')[1])].Expanded = item.IsVisible;
                                    }
                                }
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public enum ExpandModeEnum
        {

            CollapseAll = 0,
            ExpandAll = 1,
            UserDefined = 2
        }
        public ExpandModeEnum ExpandMode { get; set; }

        public bool DisplayOnlySingleContent { get => _displayOnlySingleContent; set => _displayOnlySingleContent = value; }

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
    }

    public class ExpandableItem
    {
        private bool _Expanded=false;

        public string Title { get; set; }
        public View Content { get; set; }
        public bool Expanded { get => _Expanded; set => _Expanded = value; }
    }
}
