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
    public partial class Stepper : ContentView
    {
        private int _step = 1;
        private int _min = 0;
        private int _max = 10;

        public event EventHandler PlusButtonClicked;
        public event EventHandler MinusButtonClicked;

        public virtual void OnMinusButtonClicked(EventArgs e)
        {
            MinusButtonClicked?.Invoke(this, e);
        }

        public virtual void OnPlusButtonClicked(EventArgs e)
        {
            PlusButtonClicked?.Invoke(this, e);
        }
        public Stepper()
        {
            InitializeComponent();
            btnPlus.Clicked += BtnPlus_Clicked;
            btnMinus.Clicked += BtnMinus_Clicked;
        }

        private void BtnMinus_Clicked(object sender, EventArgs e)
        {
            Value -= Step;
            if (MinusButtonClicked != null)
            {
                MinusButtonClicked(this, e);
            }
        }

        private void BtnPlus_Clicked(object sender, EventArgs e)
        {
            Value += Step;
            if (PlusButtonClicked != null)
            {
                PlusButtonClicked(this, e);
            }

        }
        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName.ToUpper())
            {
                case "VALUE":
                    UpdateValue();
                    break;
                default:
                    break;
            }
        }

        private void UpdateValue()
        {
            if (Value > Max)
            {
                Value = Max;
            }
            if (Value < Min)
            {
                Value = Min;
            }
            txtValue.Text = Value.ToString();
        }

        public int Min { get => _min; internal set => _min = value; }
        public int Max { get => _max; internal set => _max = value; }
        public static BindableProperty ValueProperty = BindableProperty.Create("Value", typeof(int), typeof(int), 1);
        private bool _isReadOnly;

        public int Value { get => (int)GetValue(ValueProperty); set => SetValue(ValueProperty, value); }

        public int Step { get => _step; internal set => _step = value; }

        public bool IsReadOnly { get => _isReadOnly; set { _isReadOnly = value; txtValue.IsEnabled = !IsReadOnly; } }
        private void txtValue_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(txtValue.Text, out int v))
            {
                Value = v;

            }
            else
            {
                txtValue.Text = Value.ToString();
            }
        }
        public double InputWidth { get => txtValue.Width; set => txtValue.WidthRequest = value; }
        public object CommandParameter { get; internal set; }
    }
}