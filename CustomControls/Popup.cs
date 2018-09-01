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

        private View _OldContent=null;

        private bool _closeOnBackGroundClicked = true;
        private bool _IsOpen;
        private List<Element> elements = new List<Element>();
        StackLayout BackGroundLayout = null;
        public enum AnimationEnum
        {
            None=0,
            SlideLeft=1,
            SlideRight=2,
            SlideTop=3,
            SlideBottom=4
        }

        public AnimationEnum EnterAnimation { get; set; }
        public AnimationEnum ExitAnimation { get; set; }

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
                BackGroundLayout = new StackLayout { BackgroundColor = BackgroundLayerColor };

                AbsoluteLayout.SetLayoutBounds(BackGroundLayout, Rectangle.FromLTRB(0, 0, 1, 1));
                AbsoluteLayout.SetLayoutFlags(BackGroundLayout, AbsoluteLayoutFlags.All);
                if (CloseOnBackGroundClicked)
                {
                    TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
                    BackGroundLayout.GestureRecognizers.Add(tapGestureRecognizer);
                }


                foreach (var item in elements)
                {
                    BackGroundLayout.Children.Add((View)item);
                }
                absoluteLayout.Children.Add(BackGroundLayout);
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
                    BackGroundLayout.TranslationX = BackGroundLayout.Width;
                    BackGroundLayout.TranslateTo(0, 0);
                    break;
                case AnimationEnum.SlideRight:
                    BackGroundLayout.TranslationX = 0-BackGroundLayout.Width;
                    BackGroundLayout.TranslateTo(0, 0);
                    break;
                case AnimationEnum.SlideTop:
                    BackGroundLayout.TranslationY = BackGroundLayout.Height;
                    BackGroundLayout.TranslateTo(0, 0);
                    break;
                case AnimationEnum.SlideBottom:
                    BackGroundLayout.TranslationY = 0-BackGroundLayout.Height;
                    BackGroundLayout.TranslateTo(0, 0);
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

        private  Task<bool> ShowExitAnimation()
        {
            Task<bool> task = null;
            switch (ExitAnimation)
            {
                    
                case AnimationEnum.SlideLeft:
                    task = BackGroundLayout.TranslateTo(0-BackGroundLayout.Width, 0);
                    break;
                case AnimationEnum.SlideRight:
                  
                  task=  BackGroundLayout.TranslateTo(BackGroundLayout.Width, 0);
                    break;
                case AnimationEnum.SlideTop:
                    task = BackGroundLayout.TranslateTo(0, 0-BackGroundLayout.Height);
                    break;
                case AnimationEnum.SlideBottom:
                    task = BackGroundLayout.TranslateTo(0, BackGroundLayout.Height);
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


        public Color BackgroundLayerColor { get;  set; }


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ClosePopup();
        }
    }
}
