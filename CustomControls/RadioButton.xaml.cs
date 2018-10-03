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

    public partial class RadioButton : ContentView
    {

        public event EventHandler CheckedChange;


        public RadioButton()
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
                RadioImage.Source = CheckedImage;
            }
            else
            {
                RadioImage.Source = UnCheckedImage;
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
                RadioImage.Source = CheckedImage;
            }
            else
            {
                RadioImage.Source = UnCheckedImage;
            }
            if (this.Checked)
            {
                OnCheckedChange(null);
            }
            CheckedChange?.Invoke(this, e);
        }

        protected void Radio_Tapped(object sender, EventArgs e)
        {

            if (this.Checked == false)
            {

                foreach (View item in ((Layout<View>)RadioTableView.Parent.Parent).Children)
                {
                    if (item is RadioButton)
                    {
                        RadioButton rb = (RadioButton)item;
                        if (rb.GroupName == this.GroupName && rb != sender)
                        {
                            rb.Checked = false;

                        }
                    }
                }
                this.Checked = true;
                CheckedChange?.Invoke(this, e);


            }

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
                RadioLabel.Text = _Text;

            }
        }

        private string _CheckedImage = "radiochecked.png";
        public string CheckedImage
        {
            get
            {
                return _CheckedImage;
            }

            set
            {
                _CheckedImage = value;
                RadioImage.Source = _CheckedImage;
            }

        }


        private string _UnCheckedImage = "radiounchecked.png";
        public string UnCheckedImage
        {
            get
            {
                return _UnCheckedImage;
            }

            set
            {
                _UnCheckedImage = value;
                RadioImage.Source = _UnCheckedImage;
            }
        }



        public new Double Height
        {
            get
            {
                return RadioViewCell.Height;
            }

            set
            {
                RadioViewCell.Height = value;

                if (ShowUnderLine == true)
                {
                    RadioTableView.HeightRequest = value + 1;
                }
                else
                {
                    RadioTableView.HeightRequest = value;
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
                if (ShowUnderLine == true)
                {
                    RadioTableView.HeightRequest = Height + 1;
                }
                else
                {
                    RadioTableView.HeightRequest = Height;
                }

            }
        }

       
        public Boolean Checked
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

                    RadioImage.Source = CheckedImage;

                }
                else
                {

                    RadioImage.Source = UnCheckedImage;


                }

                OnCheckedChange(null);

            }
        }


        public static readonly BindableProperty CheckedProperty = BindableProperty.Create("Checked", typeof(Boolean), typeof(Boolean), false);

        public Label LabelControl
        {
            get
            {
                return RadioLabel;
            }
        }
        public Image ImageControl
        {
            get
            {
                return RadioImage;
            }
        }

        public string GroupName { get; set; }
        public StackLayout StackLayoutControl
        {
            get
            {
                return RadioParent;
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