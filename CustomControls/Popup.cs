using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HS.Controls
{
    public class Popup : Xamarin.Forms.StackLayout
    {
        private ContentPage _CurrentPage;

        private View _OldContent = null;

        private bool _closeOnBackGroundClicked = true;
        private bool _IsOpen;
        private List<Element> elements = new List<Element>();
        StackLayout ContentLayout = null;
        StackLayout BackgroundLayout = null;
        private AnimationEnum _enterAnimation=AnimationEnum.SlideDown;
        private AnimationEnum _exitAnimation=AnimationEnum.SlideUp;

        public enum AnimationEnum
        {
            None = 0,
            SlideLeft = 1,
            SlideRight = 2,
            SlideUp = 3,
            SlideDown = 4
        }

        public AnimationEnum EnterAnimation { get => _enterAnimation; set => _enterAnimation = value; }
        public AnimationEnum ExitAnimation { get => _exitAnimation; set => _exitAnimation = value; }

        public Popup()
        {
            this.IsVisible = false;
            this.HeightRequest = 0;
            this.WidthRequest = 0;
            Application.Current.PageAppearing += Current_PageAppearing;

            BackgroundLayerColor = Color.FromRgba(0, 0, 0, 0.5);


        }


        private void Current_PageAppearing(object sender, Xamarin.Forms.Page e)
        {
            try
            {
                _CurrentPage = (ContentPage)e;
            }
            catch (Exception ex)
            {


            }


        }

        protected override void OnChildAdded(Element child)
        {
            base.OnChildAdded(child);
            try
            {

                elements.Add(child);
            }
            catch (Exception ex)
            {


            }

        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            try
            {
                this.Children.Clear();
                this.HeightRequest = 0;
            }
            catch (Exception ex)
            {


            }

        }

        public void ShowPopup()
        {
            if (_IsOpen == false)
            {

                if (_OldContent == null)
                {
                    _OldContent = _CurrentPage.Content;
                }

                AbsoluteLayout absoluteLayout = new AbsoluteLayout();
                StackLayout oldContentStack = new StackLayout { };
                AbsoluteLayout.SetLayoutBounds(oldContentStack, Rectangle.FromLTRB(0, 0, 1, 1));
                AbsoluteLayout.SetLayoutFlags(oldContentStack, AbsoluteLayoutFlags.All);
                oldContentStack.Children.Add(_OldContent);
                absoluteLayout.Children.Add(oldContentStack);
                BackgroundLayout = new StackLayout { BackgroundColor = BackgroundLayerColor };
                absoluteLayout.Children.Add(BackgroundLayout);
                AbsoluteLayout.SetLayoutBounds(BackgroundLayout, Rectangle.FromLTRB(0, 0, 1, 1));
                AbsoluteLayout.SetLayoutFlags(BackgroundLayout, AbsoluteLayoutFlags.All);

                ContentLayout = new StackLayout {VerticalOptions=LayoutOptions.FillAndExpand,HorizontalOptions=LayoutOptions.FillAndExpand  };

                
                if (CloseOnBackGroundClicked)
                {
                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
                    ContentLayout.GestureRecognizers.Add(tapGestureRecognizer);
                }


                foreach (var item in elements)
                {
                    ContentLayout.Children.Add((View)item);
                }
                BackgroundLayout.Children.Add(ContentLayout);
                _CurrentPage.Content = absoluteLayout;
                this.IsVisible = true;
                ShowEnterAnimation();





                _IsOpen = true;

            }
        }

        private void ShowEnterAnimation()
        {
            switch (EnterAnimation)
            {
                case AnimationEnum.None:
                    break;
                case AnimationEnum.SlideLeft:
                    ContentLayout.TranslationX = ContentLayout.Width;
                    ContentLayout.TranslateTo(0, 0);
                    break;
                case AnimationEnum.SlideRight:
                    ContentLayout.TranslationX = 0 - ContentLayout.Width;
                    ContentLayout.TranslateTo(0, 0);
                    break;
                case AnimationEnum.SlideUp:
                    ContentLayout.TranslationY = ContentLayout.Height;
                    ContentLayout.TranslateTo(0, 0);
                    break;
                case AnimationEnum.SlideDown:
                    ContentLayout.TranslationY = 0 - ContentLayout.Height;
                    ContentLayout.TranslateTo(0, 0);
                    break;
                default:
                    break;
            }
        }

        public async void ClosePopup()
        {
            if (ExitAnimation != AnimationEnum.None)
            {
                await ShowExitAnimation();
            }
            _CurrentPage.Content = _OldContent;
            _IsOpen = false;

        }

        private Task<bool> ShowExitAnimation()
        {
            Task<bool> task = null;
            switch (ExitAnimation)
            {

                case AnimationEnum.SlideLeft:
                    task = ContentLayout.TranslateTo(0 - ContentLayout.Width, 0);
                    break;
                case AnimationEnum.SlideRight:

                    task = ContentLayout.TranslateTo(ContentLayout.Width, 0);
                    break;
                case AnimationEnum.SlideUp:
                    task = ContentLayout.TranslateTo(0, 0 - ContentLayout.Height);
                    break;
                case AnimationEnum.SlideDown:
                    task = ContentLayout.TranslateTo(0, ContentLayout.Height);
                    break;

            }

            return task;
        }

        public bool CloseOnBackGroundClicked
        {
            get => _closeOnBackGroundClicked; set
            {
                _closeOnBackGroundClicked = value;
            }
        }


        public Color BackgroundLayerColor { get; set; }


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ClosePopup();
        }
    }
}
