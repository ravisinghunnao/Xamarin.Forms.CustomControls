using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace NIFShopping.CustomControls
{
    public class Repeater : ContentView
    {
        private List<View> _dataTemplate = new List<View>();

        public Repeater()
        {

        }
        protected override void OnParentSet()
        {
            base.OnParentSet();


        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            try
            {
                switch (propertyName.ToUpper())
                {
                    case "ITEMSOURCE":
                        if (ItemSource != null)
                        {

                            Content = null;

                            Layout<View> container = null;

                            if (ContainerTemplate.Count <= 0)
                            {
                                container= new StackLayout { Spacing = 0 };
                            }
                            else
                            {
                                container = ContainerTemplate[0];
                            }

                            
                            
                            foreach (var dataItem in ItemSource)
                            {
                                foreach (var item in DataTemplate)
                                {

                                    item.BindingContext = dataItem;
                                    View view = (View)Activator.CreateInstance(item.GetType());
                                    foreach (PropertyInfo propertyInfo in item.GetType().GetProperties())
                                    {
                                        if (propertyInfo.CanRead && propertyInfo.CanWrite)
                                        {
                                            object PropertyValue = propertyInfo.GetValue(item, null);

                                            propertyInfo.SetValue(view, PropertyValue);

                                        }
                                    }


                                    container.Children.Add(view);

                                }
                            }
                            Content = container;


                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                Application.Current.MainPage.DisplayAlert("Repeater Error", ex.Message, "OK");
            }

        }

        public List<View> DataTemplate { get => _dataTemplate; set => _dataTemplate = value; }

        public List<Layout<View>> ContainerTemplate { get => _container; set => _container = value; }

        public static BindableProperty ItemSourceProperty = BindableProperty.Create("ItemSource", typeof(IEnumerable), typeof(IEnumerable), null, BindingMode.TwoWay);
        private List<Layout<View>> _container=new List<Layout<View>>();

        public IEnumerable ItemSource { get => (IEnumerable)GetValue(ItemSourceProperty); set { SetValue(ItemSourceProperty, value); } }

    }




}