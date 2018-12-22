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





        View PopupContentLayout = null;
        StackLayout BackgroundLayout = null;
        BoxView boxView = null;
        Grid grid = null;
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

                var tempContent = RootPage.Content;
                RootPage.Content = null;
                AbsoluteLayout absoluteLayout = new AbsoluteLayout { };
                StackLayout oldContentStack = new StackLayout { Spacing = 0 };
                AbsoluteLayout.SetLayoutBounds(oldContentStack, Rectangle.FromLTRB(0, 0, 1, 1));
                AbsoluteLayout.SetLayoutFlags(oldContentStack, AbsoluteLayoutFlags.All);
                oldContentStack.Children.Add(tempContent);
                absoluteLayout.Children.Add(oldContentStack);
                BackgroundLayout = new StackLayout { BackgroundColor = Color.FromRgba(0, 0, 0, 0.5), Spacing = 0 };
                absoluteLayout.Children.Add(BackgroundLayout);
                AbsoluteLayout.SetLayoutBounds(BackgroundLayout, Rectangle.FromLTRB(0, 0, 1, 1));
                AbsoluteLayout.SetLayoutFlags(BackgroundLayout, AbsoluteLayoutFlags.All);

                BackgroundLayout.IsVisible = false;
                grid = new Grid { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
                boxView = new BoxView { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
                grid.Children.Add(boxView);
                // PopupContentLayout = new StackLayout { Spacing=0,BackgroundColor=Color.Green };
                // grid.Children.Add(PopupContentLayout);
                BackgroundLayout.Children.Add(grid);
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



                    PopupControls.Add(popup.Content);





                    PopupContentLayout = popup.Content;

                    popup.Content = null;
                    grid.Children.Add(PopupControls[0]);



                    if (popup.CloseOnBackGroundClicked)
                    {

                        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;

                        boxView.GestureRecognizers.Add(tapGestureRecognizer);
                    }





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
                    PopupContentLayout.TranslationX = PopupContentLayout.Width;
                    PopupContentLayout.TranslateTo(0, 0);
                    break;
                case AnimationEnum.SlideRight:
                    PopupContentLayout.TranslationX = 0 - PopupContentLayout.Width;
                    PopupContentLayout.TranslateTo(0, 0);
                    break;
                case AnimationEnum.SlideUp:
                    PopupContentLayout.TranslationY = PopupContentLayout.Height;
                    PopupContentLayout.TranslateTo(0, 0);
                    break;
                case AnimationEnum.SlideDown:
                    PopupContentLayout.TranslationY = 0 - PopupContentLayout.Height;
                    PopupContentLayout.TranslateTo(0, 0);
                    break;
                default:
                    break;
            }
        }

        public async Task ClosePopup(PopupExtended popup)
        {
            try
            {
                if (CurrentPopup == null)
                {
                    return;
                }

                if (CurrentPopup.ExitAnimation != AnimationEnum.None)
                {
                    await ShowExitAnimation();
                }


            }
            catch (Exception ex)
            {

                await RootPage.DisplayAlert("Exception", ex.Message, "OK");
            }




        }

        private Task<bool> ShowExitAnimation()
        {
            Task<bool> task = null;
            switch (CurrentPopup.ExitAnimation)
            {

                case AnimationEnum.SlideLeft:
                    task = PopupContentLayout.TranslateTo(0 - PopupContentLayout.Width, 0);
                    break;
                case AnimationEnum.SlideRight:

                    task = PopupContentLayout.TranslateTo(PopupContentLayout.Width, 0);
                    break;
                case AnimationEnum.SlideUp:
                    task = PopupContentLayout.TranslateTo(0, 0 - PopupContentLayout.Height);
                    break;
                case AnimationEnum.SlideDown:
                    task = PopupContentLayout.TranslateTo(0, PopupContentLayout.Height);
                    break;

            }

            task.ContinueWith(b =>
            {
                BackgroundLayout.IsVisible = false;
                CurrentPopup.IsVisible = false;
                CurrentPopup.IsOpen = false;


                BackgroundLayout.Children.Remove(PopupControls[0]);
                CurrentPopup.Content = PopupControls[0];


                PopupControls.Clear();

                CurrentPopup = null;
                PopupContentLayout = null;

            }, TaskScheduler.FromCurrentSynchronizationContext());

            return task;
        }







        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

            await ClosePopup(CurrentPopup);

        }
    }
}
