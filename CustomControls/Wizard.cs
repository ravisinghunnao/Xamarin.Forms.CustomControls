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
        private string _firstButtonText = "⇤";
        private string _previousButtonText = "↤";
        private string _nextButtonText = "↦";
        private string _lastButtonText = "⇥";

        private Thickness _contentPadding = 10;
        private int _activeIndex = 0;
        private FontAttributes _titleFontAttributes = FontAttributes.Bold;
        private double _titleFontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
        private Color _titleTextColor = Color.White;
        private Color _titlebarBackgroundColor = Color.FromHex("#0066cc");
        private double _titlebarHeight = 50;
        private string _defaultIconBase64 = "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";
        private ImageSource _titleIcon;
        private Thickness _footerPadding = 10;
        private Color _footerBackgroundColor = Color.FromHex("#0066cc");
        private double _buttonFontSize=Device.GetNamedSize(NamedSize.Large,typeof(Button));
        private Color _buttonTextColor= Color.FromHex("#0066cc");
        private Color _buttonBackgroundColor=Color.White;
        private int _buttonCornerRadius=25;

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
            FirstButton = new Button { Text = FirstButtonText, FontSize = ButtonFontSize, TextColor = ButtonTextColor, BackgroundColor = ButtonBackgroundColor, CornerRadius = ButtonCornerRadius };
            PreviousButton = new Button { Text = PreviousButtonText, FontSize = ButtonFontSize, TextColor = ButtonTextColor, BackgroundColor = ButtonBackgroundColor, CornerRadius = ButtonCornerRadius };
            NextButton = new Button { Text = NextButtonText, FontSize = ButtonFontSize, TextColor = ButtonTextColor, BackgroundColor = ButtonBackgroundColor, CornerRadius = ButtonCornerRadius };
            LastButton = new Button { Text = LastButtonText, FontSize = ButtonFontSize, TextColor = ButtonTextColor, BackgroundColor = ButtonBackgroundColor, CornerRadius = ButtonCornerRadius };
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
                        Padding=FooterPadding,
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
        public Color FooterBackgroundColor { get => _footerBackgroundColor; set => _footerBackgroundColor = value; }
        public ImageSource TitileBackgroundImageSource { get; set; }
        public double TitlebarHeight { get => _titlebarHeight; set => _titlebarHeight = value; }
        public ImageSource ContentBackgroundImageSource { get; set; }
        public View ContentBackgroundImage { get; private set; }
        public Thickness FooterPadding { get => _footerPadding; set => _footerPadding = value; }
        public double ButtonFontSize { get => _buttonFontSize; set => _buttonFontSize = value; }
        public Color ButtonTextColor { get => _buttonTextColor; set => _buttonTextColor = value; }
        public Color ButtonBackgroundColor { get => _buttonBackgroundColor; set => _buttonBackgroundColor = value; }
        public int ButtonCornerRadius { get => _buttonCornerRadius; set => _buttonCornerRadius = value; }
    }

    public class WizardStep
    {
        public string Title { get; set; }
        public ImageSource Icon { get; set; }
        public View Content { get; set; }
        public int StepIndex { get; internal set; }
    }
}