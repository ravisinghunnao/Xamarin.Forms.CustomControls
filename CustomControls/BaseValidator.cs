using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HS.Controls
{
  public static class BaseValidator
    {
        public static Boolean NotValid(List<Validator> Validators)
        {
            foreach (Validator item in Validators)
            {
                if (item.IsValid() == false)
                {
                    return true;
                    
                }
            }
            return false;
        }

       public static Boolean IsValid(Layout<View> layout)
        {

            List<Validator> validators = new List<Validator>();
            foreach (View item in layout.Children)
            {
                if (item is Validator)
                {
                    validators.Add((Validator)item);
                }
            }


            foreach (Validator item in validators)
            {
                if (item.IsValid() == false)
                {
                    return false;

                }
            }
            return true;
        }

        
    }
}
