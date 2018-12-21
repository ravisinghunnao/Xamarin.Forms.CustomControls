using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NIFShopping.CustomControls
{
    public class PopupExtended : Xamarin.Forms.StackLayout
    {
   

       

        private bool _closeOnBackGroundClicked = true;
        public bool IsOpen { get; set; }
        private List<Element> elements = new List<Element>();
        public Color BackgroundLayerColor { get; set; }

        public bool CloseOnBackGroundClicked
        {
            get => _closeOnBackGroundClicked; set
            {
                _closeOnBackGroundClicked = value;
            }
        }

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

        public PopupExtended()
        {
            this.IsVisible = false;
            this.HeightRequest = 0;
            this.WidthRequest = 0;
        
 


        }


       
     

        

 

 

 

 

     

 
    }
}
