using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RSPLMarketSurvey.CustomControls
{
    public class Validator : Label
    {
        private bool _setFocusOnError=true;

        public Validator()
        {


        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            this.IsVisible = false;

            View view = this.Parent.FindByName<View>(ControlToValidate);
            if (view is Entry)
            {
                ControlType = "Entry";
                this.Parent.FindByName<Entry>(ControlToValidate).Completed += Entry_Completed;
            }

            if (view is Picker)
            {
                ControlType = "Picker";
                this.Parent.FindByName<Picker>(ControlToValidate).SelectedIndexChanged += Picker_SelectedIndexChanged;
            }

        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsValid())
            {
                return;
            }

        }

        private void Button_Click(object sender, EventArgs e)
        {
            try
            {

                if (!IsValid())
                {
                    return;
                }

            }
            catch (Exception ex)
            {


            }
        }

        private void Entry_Completed(object sender, EventArgs e)
        {

            if (!IsValid())
            {
                return;
            }



        }

        public Boolean IsValid()
        {

            if (ControlType == "Entry")
            {
                Entry entry = this.Parent.FindByName<Entry>(ControlToValidate);

                foreach (string type in ValidationTypes.Split(','))
                {
                    switch (Convert.ToInt16(type))
                    {
                        case 0://required
                            if (entry.Text == null || entry.Text.Trim() == "")
                            {
                                SetDisplay(entry);
                                return false;
                            }
                            break;
                        case 1://Length range

                            if (entry.Text.Length < MinLength || entry.Text.Length > MaxLength)
                            {
                                SetDisplay(entry);
                                return false;
                            }
                            break;
                        case 2://Compare validation
                            Entry EntryToCompre = this.Parent.FindByName<Entry>(ControlToCompare);
                            if (entry.Text != EntryToCompre.Text)
                            {
                                SetDisplay(entry);
                                return false;
                            }
                            break;
                        case 3://Numeric Validation

                            if (Int64.TryParse(entry.Text, out Int64 i) == false)
                            {
                                SetDisplay(entry);
                                return false;
                            }
                            break;

                        default:
                            break;
                    }

                }

                this.IsVisible = false;


                return true;
            }

            if (ControlType == "Picker")
            {
                Picker control = this.Parent.FindByName<Picker>(ControlToValidate);

                foreach (string type in ValidationTypes.Split(','))
                {
                    switch (Convert.ToInt16(type))
                    {
                        case 0://required
                            if (control.SelectedIndex < 0)
                            {
                                SetDisplay(control);
                                return false;
                            }
                            break;

                        default:
                            break;
                    }

                }

                this.IsVisible = false;


                return true;
            }

            return true;
        }

        private void SetDisplay(Picker control)
        {
            this.Text = ErrorMessage;
            this.IsVisible = true;

            if (this.SetFocusOnError == true)
            {
                control.Focus();
            }
        }

        private void SetDisplay(Entry entry)
        {
            this.Text = ErrorMessage;
            this.IsVisible = true;

            if (this.SetFocusOnError == true)
            {
                entry.Focus();
            }
        }

        public string ControlToValidate { get; set; }
        public string ControlToCompare { get; set; }
        public string ControlType { get; set; }
        public string ErrorMessage { get; set; }
        public Boolean SetFocusOnError { get => _setFocusOnError; set => _setFocusOnError = value; }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }


        /// <summary>
        /// comma separated list of Type of validations values are
        /// 0=required
        /// </summary>
        public string ValidationTypes { get; set; }

    }
}
