using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using Xamarin.Forms;

namespace RSPLMarketSurvey.CustomControls
{
    public class Wizard : ContentView
    {
        private string _firstButtonText = "⏮";
        private string _previousButtonText = "⏪";
        private string _nextButtonText = "⏩";
        private string _lastButtonText = "⏭";
        private Thickness _contentPadding = 10;
        private int _activeIndex = 0;
        private FontAttributes _titleFontAttributes = FontAttributes.Bold;
        private double _titleFontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
        private Color _titleTextColor = Color.White;
        private Color _titlebarBackgroundColor = Color.DarkGray;
        private double _titlebarHeight=50;

        public Wizard()
        {
            WizardSteps = new List<WizardStep>();

        }

        protected override void OnParentSet()
        {
            base.OnParentSet();

            Init();
        }

        void Init()
        {
            FirstButton = new Button { Text = FirstButtonText };
            PreviousButton = new Button { Text = PreviousButtonText };
            NextButton = new Button { Text = NextButtonText };
            LastButton = new Button { Text = LastButtonText };
            _IconImage = new Image { Source = TitleIcon, Aspect = Aspect.AspectFit,Margin=new Thickness(5,0,0,0) };
            _TitleLabel = new Label { Text = TitleText, FontAttributes = TitleFontAttributes, FontFamily = TitleFontFamily, FontSize = TitleFontSize, TextColor = TitleTextColor };


            ContentLayout = new StackLayout
            {

                Padding = ContentPadding,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = ContentBackgroundColor,
                Children ={
                            WizardSteps[ActiveIndex].Content
                        }
            };

            ContentLayout.SizeChanged += ContentLayout_SizeChanged;

            FirstButton.Clicked += FirstButton_Clicked;
            PreviousButton.Clicked += PreviousButton_Clicked;
            NextButton.Clicked += NextButton_Clicked;
            LastButton.Clicked += LastButton_Clicked;
            ContentBackgroundImage = new Image { Source = ContentBackgroundImageSource, HorizontalOptions = LayoutOptions.FillAndExpand, Aspect = Aspect.Fill,HeightRequest=0 };

            Grid FooterGrid = new Grid
            {
                RowDefinitions =
                                {
                                    new RowDefinition()
                                },
                ColumnDefinitions =
                                {
                                    new ColumnDefinition(),
                                    new ColumnDefinition (),
                                    new ColumnDefinition(),
                                    new ColumnDefinition ()
                                },
                Children =
                                {
                                  FirstButton,PreviousButton,NextButton,LastButton
                                }

            };

            Grid.SetColumn(FirstButton, 0);
            Grid.SetRow(FirstButton, 0);

            Grid.SetColumn(PreviousButton, 1);
            Grid.SetRow(PreviousButton, 0);

            Grid.SetColumn(NextButton, 2);
            Grid.SetRow(NextButton, 0);

            Grid.SetColumn(LastButton, 3);
            Grid.SetRow(LastButton, 0);


            Content = new StackLayout
            {
                Spacing = 0,
                Children = {
                    new StackLayout
                    {
                      Spacing=0,
                      HorizontalOptions=LayoutOptions.FillAndExpand,
                      HeightRequest=TitlebarHeight,
                        Children   =   {
                            new Grid {Children =
                                {
                                    new Image {Source=TitileBackgroundImageSource,HorizontalOptions=LayoutOptions.FillAndExpand,Aspect=Aspect.Fill},
                        new StackLayout{
                                        BackgroundColor = TitlebarBackgroundColor,
                                        HorizontalOptions = LayoutOptions.FillAndExpand,
                                        Orientation = StackOrientation.Horizontal,
                                        Children = {
                                            _IconImage ,
                                            _TitleLabel
                                                    }
                                    }

                        }
                        }
                        }
                    }
                    ,
                    new StackLayout {
                        Children =
                        {
                            new Grid
                            {
                                Children =
                                {
                                    ContentBackgroundImage,
                                    ContentLayout
                                }
                            }
                            
                        }
                    }
                    ,
                    new StackLayout
                    {
                        BackgroundColor=FooterBackgroundColor,
                        HorizontalOptions=LayoutOptions.FillAndExpand,
                        Children =
                        {
                            FooterGrid
                        }
                    }
                }
            };
        }

        private void ContentLayout_SizeChanged(object sender, EventArgs e)
        {
            ContentBackgroundImage.HeightRequest = ContentLayout.Height;
        }

        private void LastButton_Clicked(object sender, EventArgs e)
        {
            ActiveIndex = WizardSteps.Count - 1;
            ChangeContent();
        }

        private void FirstButton_Clicked(object sender, EventArgs e)
        {
            ActiveIndex = 0;
            ChangeContent();
        }

        private void PreviousButton_Clicked(object sender, EventArgs e)
        {
            ActiveIndex -= 1;
            if (ActiveIndex <= 0)
            {
                ActiveIndex = 0;
            }
            ChangeContent();
        }



        private void NextButton_Clicked(object sender, EventArgs e)
        {
            ActiveIndex += 1;
            if (ActiveIndex > WizardSteps.Count - 1)
            {
                ActiveIndex = WizardSteps.Count - 1;
            }
            ChangeContent();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            switch (propertyName.ToUpper())
            {
                case "FIRSTBUTTONTEXT":
                    FirstButton.Text = FirstButtonText;
                    break;
                case "NEXTBUTTONTEXT":
                    NextButton.Text = NextButtonText;
                    break;
                case "PREVIOUSBUTTONTEXT":
                    PreviousButton.Text = PreviousButtonText;
                    break;
                case "LASTBUTTONTEXT":
                    LastButton.Text = LastButtonText;
                    break;
                case "ACTIVEINDEX":
                    ChangeContent();
                    break;
                default:
                    break;
            }
        }

        void ChangeContent()
        {
            ContentLayout.Children.Clear();

            WizardStep wizardStep = WizardSteps[ActiveIndex];

            ContentLayout.Children.Add(wizardStep.Content);
            if (wizardStep.Icon != null)
            {
                _IconImage.Source = wizardStep.Icon;
            }
            if (wizardStep.Title != null)
            {
                _TitleLabel.Text = wizardStep.Title;
            }

        }

        public ImageSource TitleIcon { get; set; }
        public string TitleText { get; set; }
        public FontAttributes TitleFontAttributes { get => _titleFontAttributes; set => _titleFontAttributes = value; }
        public string TitleFontFamily { get; set; }
        public double TitleFontSize { get => _titleFontSize; set => _titleFontSize = value; }
        public Color TitleTextColor { get => _titleTextColor; set => _titleTextColor = value; }
        public Color ContentBackgroundColor { get; set; }
        public string FirstButtonText { get => _firstButtonText; set => _firstButtonText = value; }
        public string PreviousButtonText { get => _previousButtonText; set => _previousButtonText = value; }
        public string NextButtonText { get => _nextButtonText; set => _nextButtonText = value; }
        public string LastButtonText { get => _lastButtonText; set => _lastButtonText = value; }
        public List<WizardStep> WizardSteps { get; set; }
        public Thickness ContentPadding { get => _contentPadding; set => _contentPadding = value; }
        public int ActiveIndex { get => _activeIndex; set => _activeIndex = value; }

        private StackLayout ContentLayout { get; set; }
        private Image _IconImage { get; set; }
        private Label _TitleLabel { get; set; }
        public Button FirstButton { get; private set; }
        public Button PreviousButton { get; private set; }
        public Button NextButton { get; private set; }
        public Button LastButton { get; private set; }
        public Color TitlebarBackgroundColor { get => _titlebarBackgroundColor; set => _titlebarBackgroundColor = value; }
        public Color FooterBackgroundColor { get; set; }
        public ImageSource TitileBackgroundImageSource { get; set; }
        public double TitlebarHeight { get => _titlebarHeight; set => _titlebarHeight = value; }
        public ImageSource ContentBackgroundImageSource { get;  set; }
        public View ContentBackgroundImage { get; private set; }
    }

    public class WizardStep
    {
        public string Title { get; set; }
        public ImageSource Icon { get; set; }
        public View Content { get; set; }
        public int StepIndex { get; internal set; }
    }
}