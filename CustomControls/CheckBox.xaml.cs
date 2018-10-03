using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HS.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class CheckBox : ContentView
    {

        public event EventHandler CheckedChange;


        public CheckBox()
        {
            InitializeComponent();



        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            init();
            return base.OnMeasure(widthConstraint, heightConstraint);

        }
        void init()
        {
            if (Checked)
            {
                CheckImage.Source = CheckedImage;
            }
            else
            {
                CheckImage.Source = UnCheckedImage;
            }
            if (this.Checked)
            {
                OnCheckedChange(null);
            }


        }





        protected virtual void OnCheckedChange(EventArgs e)
        {
            if (Checked)
            {
                CheckImage.Source = CheckedImage;
            }
            else
            {
                CheckImage.Source = UnCheckedImage;
            }
            CheckedChange?.Invoke(this, e);
        }

        protected void Check_Tapped(object sender, EventArgs e)
        {
            Checked = !Checked;
           
           
        }

        private string _Value = "";
        public string Value
        {
            get
            {
                return _Value;
            }

            set
            {
                _Value = value;

            }
        }

        private string _Text = "";
        public string Text
        {
            get
            {
                return _Text;
            }

            set
            {
                _Text = value;
                CheckLabel.Text = _Text;

            }
        }

        private string _CheckedImage = "Checkchecked.png";
        public string CheckedImage
        {
            get
            {
                return _CheckedImage;
            }

            set
            {
                _CheckedImage = value;
                CheckImage.Source = _CheckedImage;
            }

        }


        private string _UnCheckedImage = "Checkunchecked.png";
        public string UnCheckedImage
        {
            get
            {
                return _UnCheckedImage;
            }

            set
            {
                _UnCheckedImage = value;
                CheckImage.Source = _UnCheckedImage;
            }
        }



        public new Double Height
        {
            get
            {
                return CheckViewCell.Height;
            }

            set
            {
                CheckViewCell.Height = value;
                if (ShowUnderLine == true)
                {
                    CheckTableView.HeightRequest = value + 1;
                }
                else
                {
                    CheckTableView.HeightRequest = value;
                }


                }
            }

        private bool _ShowUnderLine = false;
        public Boolean ShowUnderLine
        {

            get
            {
                return _ShowUnderLine;
            }
            set
            {
                _ShowUnderLine = value;
                if (_ShowUnderLine == true)
                {
                    CheckTableView.HeightRequest = Height + 1;
                }
                else
                {
                    CheckTableView.HeightRequest = Height;
                }

            }
        }

       

      
        public bool Checked
        {
            get
            {
                return (Boolean)GetValue(CheckedProperty);
            }
            set
            {
                SetValue(CheckedProperty, value);

                if (Checked)
                {

                    CheckImage.Source = CheckedImage;

                }
                else
                {

                    CheckImage.Source = UnCheckedImage;


                }

                OnCheckedChange(null);

            }
        }

        public static readonly BindableProperty CheckedProperty = BindableProperty.Create("Checked", typeof(bool), typeof(bool), false);

        public Label LabelControl
        {
            get
            {
                return CheckLabel;
            }
        }
        public Image ImageControl
        {
            get
            {
                return CheckImage;
            }
        }


        public StackLayout StackLayoutControl
        {
            get
            {
                return CheckParent;
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName.ToUpper()) 
            {
                case "CHECKED":
                    OnCheckedChange(null);
                    break;
            }
        }

    }
}