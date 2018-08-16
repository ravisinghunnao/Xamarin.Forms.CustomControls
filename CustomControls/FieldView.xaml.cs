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
    public partial class FieldView : ContentView
    {
        Label FieldViewColumn = new Label();
        Label FieldViewValue = new Label();

        public FieldView()
        {
            InitializeComponent();
            ArrangeControls();

        }

        private void ArrangeControls()
        {
            if (_RenderMode == RenderModeEnum.Horizontol)
            {
                Grid grid = new Grid { ColumnDefinitions = new ColumnDefinitionCollection { new ColumnDefinition { Width = new GridLength(ColumnWidth, GridUnitType.Absolute) }, new ColumnDefinition { Width = new GridLength(50, GridUnitType.Star) } }, RowDefinitions = new RowDefinitionCollection { } };
                
                grid.Children.Add(FieldViewColumn);
                grid.Children.Add(FieldViewValue);
                Grid.SetColumn(FieldViewColumn, 0);
                Grid.SetColumn(FieldViewValue, 1);
                slMain.Children.Add(grid);
            }
            else
            {
                slMain.Children.Add(FieldViewColumn);
                slMain.Children.Add(FieldViewValue);
            }
        }

        private string _FieldName = "";
        public string FieldName
        {
            get
            {
                return _FieldName;
            }
            set
            {
                _FieldName = value;
                FieldViewColumn.Text = _FieldName;
            }
        }


       
        public string FieldValue
        {
            get
            {
               return GetValue(FieldValueProperty).ToString();
            }
            set
            {
                SetValue(FieldValueProperty, value);
                
            }
        }

        
        public static readonly BindableProperty FieldValueProperty = BindableProperty.Create("FieldValue", typeof(string), typeof(FieldView), "",BindingMode.TwoWay,propertyChanged:HandleFieldValuePropertyChanged);

        private static void HandleFieldValuePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {

            FieldView fieldView = (FieldView)bindable;
            fieldView.FieldViewValue.Text = newValue.ToString();
        }

        private Style _FieldNameStyle ;
        public Style FieldNameStyle
        {
            get
            {
                return _FieldNameStyle;
            }
            set
            {
                _FieldNameStyle = value;
                FieldViewColumn.Style =_FieldNameStyle;
            }
        }


        

        public static BindableProperty FieldValueStyleProperty = BindableProperty.Create("FieldValueStyle", typeof(Style), typeof(Style),null,BindingMode.TwoWay,propertyChanged: HandleFieldValueStyleChanged);
        public Style FieldValueStyle { get => (Style)GetValue(FieldValueStyleProperty); set  { SetValue(FieldValueStyleProperty, value); }  }

        private static void HandleFieldValueStyleChanged(BindableObject bindable, object oldValue, object newValue)
        {

            FieldView fieldView = (FieldView)bindable;
            fieldView.FieldViewValue.Style = newValue as Style;
        }
        private int _ColumnWidth = 125;
        public int ColumnWidth
        {
            get
            {
                return _ColumnWidth;
            }
            set
            {
                _ColumnWidth = value;
                FieldViewColumn.MinimumHeightRequest = _ColumnWidth;
                FieldViewColumn.WidthRequest = _ColumnWidth;
            }
        }


        private RenderModeEnum _RenderMode = RenderModeEnum.Vertical;
        public RenderModeEnum RenderMode
        {
            get
            {
                return _RenderMode;
            }
            set
            {
                _RenderMode = value;
                ArrangeControls();
            }
        }

        

        public enum RenderModeEnum
        {
            Horizontol=0,
            Vertical=1
        }

    }
}