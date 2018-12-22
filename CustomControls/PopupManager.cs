using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static NIFShopping.CustomControls.PopupExtended;

namespace NIFShopping.CustomControls
{
    class PopupManager : Xamarin.Forms.View
    {
        
     



              StackLayout ContentLayout = null;
        StackLayout BackgroundLayout = null;

        private List<View> _popupControls;





        public PopupManager()
        {
            this.IsVisible = false;

         


        }


        public void Init(ContentPage page)
        {
            try
            {
                RootPage = page;

              
                    AbsoluteLayout absoluteLayout = new AbsoluteLayout();
                    StackLayout oldContentStack = new StackLayout { };
                    AbsoluteLayout.SetLayoutBounds(oldContentStack, Rectangle.FromLTRB(0, 0, 1, 1));
                    AbsoluteLayout.SetLayoutFlags(oldContentStack, AbsoluteLayoutFlags.All);
                    oldContentStack.Children.Add(RootPage.Content);
                    absoluteLayout.Children.Add(oldContentStack);
                    BackgroundLayout = new StackLayout { BackgroundColor = Color.FromRgba(0, 0, 0, 0.5) };
                    absoluteLayout.Children.Add(BackgroundLayout);
                    AbsoluteLayout.SetLayoutBounds(BackgroundLayout, Rectangle.FromLTRB(0, 0, 1, 1));
                    AbsoluteLayout.SetLayoutFlags(BackgroundLayout, AbsoluteLayoutFlags.All);
                    ContentLayout = new StackLayout { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
                    BackgroundLayout.IsVisible = false;
                    RootPage.Content = absoluteLayout;
                 
            }
            catch (Exception ex)
            {

                RootPage.DisplayAlert("Exception", ex.Message, "OK");
            }
        }
       
    

        public ContentPage RootPage { get; set; }
        public List<View> PopupControls
        {
            get
            {
                if (_popupControls == null)
                {
                    _popupControls = new List<View>();
                }
                return _popupControls;

            }
            set
            {
                _popupControls = value;

            }

        }


        public void ShowPopup(PopupExtended popup)
        {

            try
            {
                if (CurrentPopup != null)
                {
                    return;
                }

                if (popup.IsVisible == false)
                {


                    CurrentPopup = popup;
                    BackgroundLayout.BackgroundColor = popup.BackgroundLayerColor;


                    foreach (var item in popup.Children)
                    {
                        PopupControls.Add(item);

                    }

                    foreach (var item in PopupControls)
                    {
                        popup.Children.Remove(item);
                        ContentLayout.Children.Add(item);
                    }


                    if (popup.CloseOnBackGroundClicked)
                    {

                        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
                        ContentLayout.GestureRecognizers.Add(tapGestureRecognizer);
                    }



                    BackgroundLayout.Children.Add(ContentLayout);

                    popup.IsVisible = true;
                    BackgroundLayout.IsVisible = true;
                    ShowEnterAnimation();

                    CurrentPopup.IsOpen = true;





                }
            }
            catch (Exception ex)
            {

                RootPage.DisplayAlert("Exception", ex.Message, "OK");
            }
          
        }

        public PopupExtended CurrentPopup { get; set; }
        private void ShowEnterAnimation()
        {
            switch (CurrentPopup.EnterAnimation)
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

        public async void ClosePopup(PopupExtended popup)
        {
            try
            {
                if (CurrentPopup == null)
                {
                    return;
                }

                if (CurrentPopup.ExitAnimation != AnimationEnum.None)
                {
                  await   ShowExitAnimation();
                }
               
               
            }
            catch (Exception ex)
            {

              await  RootPage.DisplayAlert("Exception", ex.Message, "OK");
            }

         


        }

        private Task<bool> ShowExitAnimation()
        {
            Task<bool> task = null;
            switch (CurrentPopup.ExitAnimation)
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

            task.ContinueWith(b => {
                BackgroundLayout.IsVisible = false;
                CurrentPopup.IsVisible = false;
                CurrentPopup.IsOpen = false;
                foreach (var item in PopupControls)
                {

                    BackgroundLayout.Children.Remove(item);
                    CurrentPopup.Children.Add(item);
                }

                PopupControls.Clear();

                CurrentPopup = null;

            },TaskScheduler.FromCurrentSynchronizationContext());

            return task;
        }







        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ClosePopup(CurrentPopup);
        }
    }
}
