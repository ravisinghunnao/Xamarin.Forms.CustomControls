using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace HS.Controls
{
    public class Calendar : ContentView
    {
        Grid CalendarGrid = null;
        Label lblDayName = null;
        Label lblCurrentMonthDayNumber = null;
        Label lblOtherMonthDayNumber = null;
        Button btnPrevious = null;
        Button btnNext = null;


        AbsoluteLayout container = null;
        Image imgBackground = null;
        public event EventHandler NextButtonClicked;
        public event EventHandler PreviousButtonClicked;
        public event EventHandler DateClicked;
        public event EventHandler BeforeRendering;
        public event EventHandler DateRendered;

        protected virtual void OnBeforeRendering(EventArgs e)
        {
            BeforeRendering?.Invoke(this, e);
        }
        protected virtual void OnDateRendering(EventArgs e)
        {
            DateRendered?.Invoke(this, e);
        }
        protected virtual void OnDateClicked(EventArgs e)
        {
            DateClicked?.Invoke(this, e);
        }

        protected virtual void OnNextButtonClicked(EventArgs e)
        {
            NextButtonClicked?.Invoke(this, e);
        }

        protected virtual void OnPreviousButtonClicked(EventArgs e)
        {
            PreviousButtonClicked?.Invoke(this, e);
        }

        public Calendar()
        {

            OnBeforeRendering(null);


        }


        protected override void OnParentSet()
        {
            base.OnParentSet();


        }

        public void Render()
        {
            BindCalendar();
        }


        private void BindCalendar()
        {

            this.Padding = 0;
            this.BackgroundColor = CalendarBackColor;
            int Year = DisplayYear;
            int Month = DisplayMonth;

            CalendarGrid = new Grid
            {
                RowSpacing = CellSpacing,
                ColumnSpacing = CellSpacing,
                RowDefinitions = new RowDefinitionCollection { new RowDefinition { }, new RowDefinition { }, new RowDefinition { }, new RowDefinition { }, new RowDefinition { }, new RowDefinition { }, new RowDefinition { }, },
                ColumnDefinitions = new ColumnDefinitionCollection { new ColumnDefinition { }, new ColumnDefinition { }, new ColumnDefinition { }, new ColumnDefinition { }, new ColumnDefinition { }, new ColumnDefinition { }, new ColumnDefinition { } }
            };

            btnPrevious = new Button { Text = "<", IsVisible = ShowPreviousMonth, BackgroundColor = NavigationBackgroundColor, TextColor = NavigationTextColor };
            btnPrevious.Clicked += BtnPrevious_Clicked;
            Label lblMonthName = new Label { TextColor = MonthTextColor, FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)), FontAttributes = FontAttributes.Bold, VerticalOptions = LayoutOptions.CenterAndExpand, Text = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Month).ToString() + " " + Year, HorizontalOptions = LayoutOptions.CenterAndExpand };
            btnNext = new Button { Text = ">", IsVisible = ShowNextMonth, BackgroundColor = NavigationBackgroundColor, TextColor = NavigationTextColor };
            btnNext.Clicked += BtnNext_Clicked;
            BoxView bvPrevious = new BoxView { BackgroundColor = TitleBarColor, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
            CalendarGrid.Children.Add(bvPrevious);
            Grid.SetColumn(bvPrevious, 0);
            Grid.SetRow(bvPrevious, 0);
            CalendarGrid.Children.Add(btnPrevious);
            Grid.SetColumn(btnPrevious, 0);
            Grid.SetRow(btnPrevious, 0);



            BoxView bvMonthName = new BoxView { BackgroundColor = TitleBarColor, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
            CalendarGrid.Children.Add(bvMonthName);
            Grid.SetColumn(bvMonthName, 1);
            Grid.SetRow(bvMonthName, 0);
            Grid.SetColumnSpan(bvMonthName, 5);
            CalendarGrid.Children.Add(lblMonthName);
            Grid.SetColumn(lblMonthName, 1);
            Grid.SetRow(lblMonthName, 0);
            Grid.SetColumnSpan(lblMonthName, 5);


            BoxView bvNext = new BoxView { BackgroundColor = TitleBarColor, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
            CalendarGrid.Children.Add(bvNext);
            Grid.SetColumn(bvNext, 6);
            Grid.SetRow(bvNext, 0);
            CalendarGrid.Children.Add(btnNext);
            Grid.SetColumn(btnNext, 6);
            Grid.SetRow(btnNext, 0);


            for (int i = 0; i < 7; i++)
            {
                BoxView dayNameBackground = new BoxView { BackgroundColor = DayNameBackgroundColor, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
                string DayName = ((DayOfWeek)i).ToString();
                lblDayName = new Label { Text = DayName.Substring(0, 3), TextColor = DayNameTextColor, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
                CalendarGrid.Children.Add(dayNameBackground);
                CalendarGrid.Children.Add(lblDayName);
                Grid.SetColumn(dayNameBackground, i);
                Grid.SetRow(dayNameBackground, 1);
                Grid.SetColumn(lblDayName, i);
                Grid.SetRow(lblDayName, 1);
            }

            List<CalanderDays> DateList = new List<CalanderDays>();

            int LastYear = Month == 1 ? Year - 1 : Year;
            int LastMonth = Month == 1 ? 12 : Month - 1;
            int PreviousMonthDays = DateTime.DaysInMonth(LastYear, LastMonth);

            DateTime Firstdate = new DateTime(Year, Month, 01);

            int FirstDayNumber = (int)Firstdate.DayOfWeek;

            int StartNumber = 0;
            if (FirstDayNumber == 0)
            {
                StartNumber = (PreviousMonthDays + 1) - (FirstDayNumber + 7);
            }
            else
            {
                StartNumber = (PreviousMonthDays + 1) - FirstDayNumber;
            }

            int DaysInMonth = DateTime.DaysInMonth(Year, Month);

            for (int i = StartNumber; i <= PreviousMonthDays; i++)
            {
                DateList.Add(new CalanderDays(1, i));
            }

            for (int i = 1; i <= DaysInMonth; i++)
            {
                DateList.Add(new CalanderDays(2, i));
            }
            int NextMonthDay = 1;
            for (int i = DateList.Count; i < 42; i++)
            {
                DateList.Add(new CalanderDays(3, NextMonthDay));
                NextMonthDay++;
            }
            int cn = 0, rn = 2;
            int TodayDate = DateTime.Today.Day;

            foreach (var item in DateList)
            {
                StackLayout slCellContainer = new StackLayout { BackgroundColor = CellBackgroundColor };

                switch (item.DayType)
                {
                    case 1:
                    case 3:

                        lblOtherMonthDayNumber = new Label { Text = item.DayNumber.ToString(), TextColor = OtherMonthDayColor, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
                        slCellContainer.Children.Add(lblOtherMonthDayNumber);
                        Grid.SetColumn(lblOtherMonthDayNumber, cn);
                        Grid.SetRow(lblOtherMonthDayNumber, rn);
                        break;
                    case 2:
                        lblCurrentMonthDayNumber = new Label { Text = item.DayNumber.ToString(), TextColor = DateColor, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
                        if (item.DayNumber == TodayDate && Month == DateTime.Today.Month)
                        {
                            slCellContainer.BackgroundColor = TodayBackgroundColor;
                            lblCurrentMonthDayNumber.TextColor = TodayTextColor;
                        }

                        slCellContainer.Children.Add(lblCurrentMonthDayNumber);
                        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
                        tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
                        slCellContainer.GestureRecognizers.Add(tapGestureRecognizer);
                        break;



                }

                slCellContainer.StyleId = item.DayNumber.ToString();
                CalendarGrid.Children.Add(slCellContainer);
                if (item.DayType == 2)
                {
                    DateRendered(slCellContainer, null);
                }
                Grid.SetColumn(slCellContainer, cn);
                Grid.SetRow(slCellContainer, rn);

                if (cn == 6)
                {
                    cn = 0;
                    rn++;
                }
                else
                {
                    cn++;
                }

            }
            container = new AbsoluteLayout();




            imgBackground = new Image { Source = CalendarBackgroundImage };


            container.Children.Add(imgBackground);
            AbsoluteLayout.SetLayoutBounds(imgBackground, Rectangle.FromLTRB(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(imgBackground, AbsoluteLayoutFlags.All);






            container.Children.Add(CalendarGrid);
            AbsoluteLayout.SetLayoutBounds(CalendarGrid, Rectangle.FromLTRB(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(CalendarGrid, AbsoluteLayoutFlags.All);
            Frame frame = new Frame { BorderColor = BorderColor, CornerRadius = BorderRadius, Padding = 5, HasShadow = HasShadow };
            frame.Content = container;

            this.Content = frame;






        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            SelectedDate = new DateTime(DisplayYear, DisplayMonth, Convert.ToInt32(((StackLayout)sender).StyleId));
            SelectedItem = (StackLayout)sender;

            DateClicked(sender, e);

            StackLayout stackLayout = (StackLayout)sender;
            _OldBGColor = stackLayout.BackgroundColor;
            stackLayout.BackgroundColor = SelectionColor;

            Device.StartTimer(TimeSpan.FromMilliseconds(250), () =>
            {


                stackLayout.BackgroundColor = _OldBGColor;
                return false;


            });
        }

        private void BtnNext_Clicked(object sender, EventArgs e)
        {
            if (DisplayMonth == 12)
            {
                DisplayMonth = 1;
                DisplayYear++;
            }
            else
            {
                DisplayMonth++;
            }

            NextButtonClicked(sender, e);
        }

        private void BtnPrevious_Clicked(object sender, EventArgs e)
        {
            if (DisplayMonth == 1)
            {
                DisplayMonth = 12;
                DisplayYear--;
            }
            else
            {
                DisplayMonth--;
            }

            PreviousButtonClicked(sender, e);
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName.ToUpper())
            {
                case "BACKGROUNDCOLOR":
                    this.BackgroundColor = CalendarBackColor;
                    break;
                case "CELLWIDTH":
                    if (lblCurrentMonthDayNumber != null)
                    {
                        lblCurrentMonthDayNumber.WidthRequest = CellWidth;
                    }
                    if (lblDayName != null)
                    {
                        lblDayName.WidthRequest = CellWidth;
                    }
                    if (lblOtherMonthDayNumber != null)
                    {
                        lblOtherMonthDayNumber.WidthRequest = CellWidth;
                    }
                    break;
                case "OTHERMONTHDAYCOLOR":
                    if (lblOtherMonthDayNumber != null)
                    {
                        lblOtherMonthDayNumber.TextColor = OtherMonthDayColor;
                    }
                    break;
                //case "DISPLAYMONTH":
                //case "DISPLAYYEAR":
                //    BindCalendar();
                //    break;
                case "DATECOLOR":
                    if (lblCurrentMonthDayNumber != null)
                    {
                        lblCurrentMonthDayNumber.TextColor = DateColor;
                    }
                    break;
                case "SHOWPREVIOUSMONTH":
                    if (btnPrevious != null)
                    {
                        btnPrevious.IsVisible = ShowPreviousMonth;
                    }
                    break;
                case "SHOWNEXTMONTH":
                    if (btnNext != null)
                    {
                        btnNext.IsVisible = ShowNextMonth;
                    }
                    break;
                case "CALENDARBACKGROUNDIMAGE":
                    if (imgBackground != null)
                    {
                        imgBackground.Source = CalendarBackgroundImage;
                    }
                    break;
            }
        }

        public new static BindableProperty BackgroundColorProperty = BindableProperty.Create("BackgroundColor", typeof(Color), typeof(Color), Color.White); public Color CalendarBackColor { get => (Color)GetValue(BackgroundColorProperty); set => SetValue(BackgroundColorProperty, value); }

        public static BindableProperty CellWidthProperty = BindableProperty.Create("CellWidth", typeof(double), typeof(double), 30.0); public double CellWidth { get => (double)GetValue(CellWidthProperty); set => SetValue(CellWidthProperty, value); }

        public static BindableProperty OtherMonthDayColorProperty = BindableProperty.Create("OtherMonthDayColor", typeof(Color), typeof(Color), Color.Silver); public Color OtherMonthDayColor { get => (Color)GetValue(OtherMonthDayColorProperty); set => SetValue(OtherMonthDayColorProperty, value); }

        public static BindableProperty DisplayMonthProperty = BindableProperty.Create("DisplayMonth", typeof(int), typeof(int), DateTime.Today.Month); public int DisplayMonth { get => (int)GetValue(DisplayMonthProperty); set => SetValue(DisplayMonthProperty, value); }

        public static BindableProperty BorderColorProperty = BindableProperty.Create("BorderColor", typeof(Color), typeof(Color), Color.Silver); public Color BorderColor { get => (Color)GetValue(BorderColorProperty); set => SetValue(BorderColorProperty, value); }

        public static BindableProperty DisplayYearProperty = BindableProperty.Create("DisplayYear", typeof(int), typeof(int), DateTime.Today.Year); public int DisplayYear { get => (int)GetValue(DisplayYearProperty); set => SetValue(DisplayYearProperty, value); }

        public static BindableProperty DateColorProperty = BindableProperty.Create("DateColor", typeof(Color), typeof(Color), Color.Black); public Color DateColor { get => (Color)GetValue(DateColorProperty); set => SetValue(DateColorProperty, value); }

        public static BindableProperty SelectedDateProperty = BindableProperty.Create("SelectedDate", typeof(DateTime), typeof(DateTime), DateTime.Today); public DateTime SelectedDate { get => (DateTime)GetValue(SelectedDateProperty); set => SetValue(SelectedDateProperty, value); }
        public StackLayout SelectedItem { get; private set; }

        public static BindableProperty ShowPreviousMonthProperty = BindableProperty.Create("ShowPreviousMonth", typeof(bool), typeof(bool), true); public bool ShowPreviousMonth { get => (bool)GetValue(ShowPreviousMonthProperty); set => SetValue(ShowPreviousMonthProperty, value); }
        public static BindableProperty ShowNextMonthProperty = BindableProperty.Create("ShowNextMonth", typeof(bool), typeof(bool), true); public bool ShowNextMonth { get => (bool)GetValue(ShowNextMonthProperty); set => SetValue(ShowNextMonthProperty, value); }

        public static BindableProperty TodayBackgroundColorProperty = BindableProperty.Create("TodayBackgroundColor", typeof(Color), typeof(Color), Color.Blue); public Color TodayBackgroundColor { get => (Color)GetValue(TodayBackgroundColorProperty); set => SetValue(TodayBackgroundColorProperty, value); }

        public static BindableProperty CellBackgroundColorProperty = BindableProperty.Create("CellBackgroundColor", typeof(Color), typeof(Color), Color.Transparent); public Color CellBackgroundColor { get => (Color)GetValue(CellBackgroundColorProperty); set => SetValue(CellBackgroundColorProperty, value); }

        public static BindableProperty NavigationTextColorProperty = BindableProperty.Create("NavigationTextColor", typeof(Color), typeof(Color), Color.Black); public Color NavigationTextColor { get => (Color)GetValue(NavigationTextColorProperty); set => SetValue(NavigationTextColorProperty, value); }
        public static BindableProperty NavigationBackgroundColorProperty = BindableProperty.Create("NavigationBackgroundColor", typeof(Color), typeof(Color), Color.Transparent); public Color NavigationBackgroundColor { get => (Color)GetValue(NavigationBackgroundColorProperty); set => SetValue(NavigationBackgroundColorProperty, value); }

        public static BindableProperty CellSpacingProperty = BindableProperty.Create("CellSpacing", typeof(double), typeof(double), 0.0); public double CellSpacing { get => (double)GetValue(CellSpacingProperty); set => SetValue(CellSpacingProperty, value); }
        public static BindableProperty TodayTextColorProperty = BindableProperty.Create("TodayTextColor", typeof(Color), typeof(Color), Color.White); public Color TodayTextColor { get => (Color)GetValue(TodayTextColorProperty); set => SetValue(TodayTextColorProperty, value); }

        public static BindableProperty MonthTextColorProperty = BindableProperty.Create("MonthTextColor", typeof(Color), typeof(Color), Color.Black); public Color MonthTextColor { get => (Color)GetValue(MonthTextColorProperty); set => SetValue(MonthTextColorProperty, value); }
        public static BindableProperty DayNameTextColorProperty = BindableProperty.Create("DayNameTextColor", typeof(Color), typeof(Color), Color.Black); public Color DayNameTextColor { get => (Color)GetValue(DayNameTextColorProperty); set => SetValue(DayNameTextColorProperty, value); }
        public static BindableProperty BorderRadiusProperty = BindableProperty.Create("BorderRadius", typeof(float), typeof(float), 0.0f); public float BorderRadius { get => (float)GetValue(BorderRadiusProperty); set => SetValue(BorderRadiusProperty, value); }

        public static BindableProperty CalendarBackgroundImageProperty = BindableProperty.Create("CalendarBackgroundImage", typeof(ImageSource), typeof(ImageSource), null); public ImageSource CalendarBackgroundImage { get => (ImageSource)GetValue(CalendarBackgroundImageProperty); set => SetValue(CalendarBackgroundImageProperty, value); }
        public static BindableProperty BorderWidthProperty = BindableProperty.Create("BorderWidth", typeof(double), typeof(double), 1.0); public double BorderWidth { get => (double)GetValue(BorderWidthProperty); set => SetValue(BorderWidthProperty, value); }
        public static BindableProperty HasShadowProperty = BindableProperty.Create("HasShadow", typeof(bool), typeof(bool), false);
        private Color _OldBGColor;
        private Color _selectionColor = Color.Orange;
        private Color _dayNameBackgroundColor = Color.White;
        private Color _titleBarColor = Color.LightBlue;
        

        public bool HasShadow { get => (bool)GetValue(HasShadowProperty); set => SetValue(HasShadowProperty, value); }
        public Color SelectionColor { get => _selectionColor; set => _selectionColor = value; }
        public Color DayNameBackgroundColor { get => _dayNameBackgroundColor; set => _dayNameBackgroundColor = value; }
        public Color TitleBarColor { get => _titleBarColor; set => _titleBarColor = value; }
        
    }


    class CalanderDays
    {
        public int DayType { get; set; }
        public int DayNumber { get; set; }

        public CalanderDays(int DT, int DN)
        {
            DayType = DT;
            DayNumber = DN;
        }
    }

}
