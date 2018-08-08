using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HS.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FieldView : ContentView
    {
        public FieldView()
        {
            InitializeComponent();
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


        private string _FieldValue = "";
        public string FieldValue
        {
            get
            {
                return _FieldValue;
            }
            set
            {
                _FieldValue = value;
                FieldViewValue.Text = _FieldValue;
            }
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


        private Style _FieldValueStyle ;
        public Style FieldValueStyle
        {
            get
            {
                return _FieldValueStyle;
            }
            set
            {
                _FieldValueStyle = value;
                FieldViewValue.Style = _FieldValueStyle;
            }
        }


        private int _ColumnWidth = 200;
        public int ColumnWidth
        {
            get
            {
                return _ColumnWidth;
            }
            set
            {
                _ColumnWidth = value;
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
                switch (_RenderMode)
                {
                    case RenderModeEnum.Horizontol:
                        slMain.Orientation = StackOrientation.Horizontal;
                        break;
                    case RenderModeEnum.Vertical:
                        slMain.Orientation = StackOrientation.Vertical;
                        break;
                }
            }
        }


        public enum RenderModeEnum
        {
            Horizontol=0,
            Vertical=1
        }

    }
}