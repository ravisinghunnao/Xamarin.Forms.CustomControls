using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HS.Controls
{
    public class Popup : Xamarin.Forms.StackLayout
    {
        private ContentPage _CurrentPage;

        private View _OldContent=null;

        private bool _closeOnBackGroundClicked = true;
        private bool _IsOpen;

        List<Element> elements = new List<Element>();


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
                StackLayout BackGroundLayout = new StackLayout { BackgroundColor = BackgroundLayerColor };

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
                _IsOpen = true;

            }
        }

        public void ClosePopup()
        {

            _CurrentPage.Content = _OldContent;
            _IsOpen = false;
        }



        public bool CloseOnBackGroundClicked
        {
            get => _closeOnBackGroundClicked; set
            {
                _closeOnBackGroundClicked = value;
            }
        }


        public Color BackgroundLayerColor { get; private set; }


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ClosePopup();
        }
    }
}
