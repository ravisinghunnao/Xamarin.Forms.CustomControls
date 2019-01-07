using System;
using System.Collections.Generic;
using System.IO;
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
        private Color _titlebarBackgroundColor = Color.FromHex("#0066cc");
        private double _titlebarHeight = 50;
        private string _defaultIconBase64 = "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";
        private ImageSource _titleIcon;

        public Wizard()
        {
            WizardSteps = new List<WizardStep>();
            _titleIcon = ImageSource.FromStream(
            () => new MemoryStream(Convert.FromBase64String(_defaultIconBase64)));


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
            _IconImage = new Image { Source = TitleIcon, Aspect = Aspect.AspectFit, Margin = new Thickness(5, 0, 0, 0) };
            _TitleLabel = new Label { Text = TitleText, FontAttributes = TitleFontAttributes, FontFamily = TitleFontFamily, FontSize = TitleFontSize, TextColor = TitleTextColor };


            ContentLayout = new StackLayout
            {

                Padding = ContentPadding,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = ContentBackgroundColor,
               
            };

            ContentLayout.SizeChanged += ContentLayout_SizeChanged;

            FirstButton.Clicked += FirstButton_Clicked;
            PreviousButton.Clicked += PreviousButton_Clicked;
            NextButton.Clicked += NextButton_Clicked;
            LastButton.Clicked += LastButton_Clicked;
            ContentBackgroundImage = new Image { Source = ContentBackgroundImageSource, HorizontalOptions = LayoutOptions.FillAndExpand, Aspect = Aspect.Fill, HeightRequest = 0 };

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
                      BackgroundColor = TitlebarBackgroundColor,
                      Spacing=0,
                      HorizontalOptions=LayoutOptions.FillAndExpand,
                      HeightRequest=TitlebarHeight,
                        Children   =   {
                            new Grid { HorizontalOptions=LayoutOptions.FillAndExpand,VerticalOptions=LayoutOptions.FillAndExpand, Children =
                                {
                                    new Image {Source=TitileBackgroundImageSource,VerticalOptions=LayoutOptions.CenterAndExpand, HorizontalOptions=LayoutOptions.FillAndExpand,Aspect=Aspect.Fill},
                        new StackLayout{
                                        VerticalOptions=LayoutOptions.CenterAndExpand,    
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

            ChangeContent();
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
            else
            {
                _IconImage.Source = TitleIcon;
            }

            if (wizardStep.Title != null)
            {
                _TitleLabel.Text = wizardStep.Title;
            }
            else if (this.TitleText != null)
            {
                _TitleLabel.Text = TitleText;
            }
            else
            {
                _TitleLabel.Text = string.Format("Step {0} of {1}", ActiveIndex + 1, WizardSteps.Count);
            }

        }

        public ImageSource TitleIcon { get => _titleIcon; set => _titleIcon = value; }
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
        public ImageSource ContentBackgroundImageSource { get; set; }
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