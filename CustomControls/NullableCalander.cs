using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace RSPLMarketSurvey.CustomControls
{
	public class NullableCalander : ContentView
	{
        Entry txtPicker = new Entry { HorizontalOptions=LayoutOptions.FillAndExpand ,Placeholder="Enter Date"};
        Button btnClear = new Button { Text = "X", TextColor = Color.Red,WidthRequest=50,HorizontalOptions=LayoutOptions.End,VerticalOptions=LayoutOptions.FillAndExpand };
        DatePicker DPHidden = new DatePicker { IsVisible = false };
        public NullableCalander ()
		{
			Content = new StackLayout {
				Children = {
					new StackLayout {HorizontalOptions=LayoutOptions.FillAndExpand,VerticalOptions =LayoutOptions.FillAndExpand,Orientation=StackOrientation.Horizontal,Spacing=0,
                        Children =
                        {
                            txtPicker,
                            btnClear,
                            DPHidden
                        }
                    
                    }
                    
				}
			};

            txtPicker.Focused += TxtPicker_Focused;
            DPHidden.DateSelected += DPHidden_DateSelected;
            btnClear.Clicked += BtnClear_Clicked;

        }

        private void BtnClear_Clicked(object sender, EventArgs e)
        {
            txtPicker.Text = "";
        }

        private void DPHidden_DateSelected(object sender, DateChangedEventArgs e)
        {
            try
            {
                txtPicker.Text =string.Format("{0:MM/dd/yyyy}",DPHidden.Date);
            }
            catch (Exception)
            {

                
            }
        }

        private void TxtPicker_Focused(object sender, FocusEventArgs e)
        {
            try
            {
                DPHidden.Focus();
            }
            catch (Exception ex)
            {

                
            }
        }

        public string Value { get=>txtPicker.Text; set=>txtPicker.Text=value; }
    }
}