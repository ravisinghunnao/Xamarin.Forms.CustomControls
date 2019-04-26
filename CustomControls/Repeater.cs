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

        private View CreateClone(View view)
        {
            View ClonedView =(View) Activator.CreateInstance(view.GetType());
            foreach (PropertyInfo propertyInfo in view.GetType().GetProperties())
            {
                if (propertyInfo.CanRead && propertyInfo.CanWrite)
                {
                    object PropertyValue = propertyInfo.GetValue(view, null);

                    propertyInfo.SetValue(ClonedView, PropertyValue);

                }
            }
            return ClonedView;
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



                            if (ContainerName == "")
                            {
                                throw new Exception("ContainerName property is not set.");

                            }
                            else
                            {
                                Layout<View> OriginalContainer = this.FindByName<Layout<View>>(ContainerName);
                                container =(Layout<View>) CreateClone(OriginalContainer);

                              


                                container.Children.Clear();

                                foreach (var dataItem in ItemSource)
                                {

                                    foreach (var item in OriginalContainer.Children)
                                    {
                                        View clonedview = CreateClone(item);
                                        BindChildrens(item, clonedview, dataItem);
                                        container.Children.Add(clonedview);
                                    }
                                    



                            
                                }
                             
                                Content = container;

                            }




                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }



        void BindChildrens(View OriginalView, View ClonedView, object BindingContext)
        {
         

            if (OriginalView is ContentView contentView)
            {
                ((ContentView)ClonedView).Content = null;

                var item = contentView.Content;
                item.BindingContext = BindingContext;
                View v = CreateClone(item);

                
                ((ContentView)ClonedView).Content = v;

                BindChildrens(item, v,BindingContext);
            }
            else if (OriginalView is Layout<View> layout)
            {
                ((Layout<View>)ClonedView).Children.Clear();
                foreach (var item in layout.Children)
                {
                    item.BindingContext = BindingContext;
                    View v = CreateClone(item);
                    
                    ((Layout<View>)ClonedView).Children.Add(v);

                    BindChildrens(item, v, BindingContext);
                }
            }
            


        }

        public List<View> DataTemplate { get => _dataTemplate; set => _dataTemplate = value; }

        public string ContainerName { get => _containerName; set => _containerName = value; }

        public static BindableProperty ItemSourceProperty = BindableProperty.Create("ItemSource", typeof(IEnumerable), typeof(IEnumerable), null, BindingMode.TwoWay);
        private string _containerName = "";

        public IEnumerable ItemSource { get => (IEnumerable)GetValue(ItemSourceProperty); set { SetValue(ItemSourceProperty, value); } }

    }




}