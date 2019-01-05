using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace RSPLMarketSurvey.CustomControls
{
    public class Wizard : ContentView
    {
        private string _firstButtonText = "First";
        private string _previousButtonText = "Previous";
        private string _nextButtonText = "Next";
        private string _lastButtonText = "Last";
        private Thickness _contentPadding = 10;
        private int _activeIndex=0;

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
            Button FirstButton = new Button { Text = FirstButtonText };
            Button PreviousButton = new Button { Text = PreviousButtonText };
            Button NextButton = new Button { Text = NextButtonText };
            Button LastButton = new Button { Text = LastButtonText };
            _IconImage = new Image { Source = TitleIcon, Aspect = Aspect.Fill };
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

            FirstButton.Clicked += FirstButton_Clicked;
            PreviousButton.Clicked += PreviousButton_Clicked;
            NextButton.Clicked += NextButton_Clicked;
            LastButton.Clicked += LastButton_Clicked;
            

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
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Orientation=StackOrientation.Horizontal,
                        Children =
                        {
                            _IconImage ,
                            _TitleLabel
                        }
                    },
                    ContentLayout,
                    new StackLayout
                    {
                        HorizontalOptions=LayoutOptions.FillAndExpand,
                        Children =
                        {
                            FooterGrid
                        }
                    }
                }
            };
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
        public FontAttributes TitleFontAttributes { get; set; }
        public string TitleFontFamily { get; set; }
        public double TitleFontSize { get; set; }
        public Color TitleTextColor { get; set; }
        public Color ContentBackgroundColor { get; set; }
        public string FirstButtonText { get => _firstButtonText; set => _firstButtonText = value; }
        public string PreviousButtonText { get => _previousButtonText; set => _previousButtonText = value; }
        public string NextButtonText { get => _nextButtonText; set => _nextButtonText = value; }
        public string LastButtonText { get => _lastButtonText; set => _lastButtonText = value; }
        public List<WizardStep> WizardSteps { get; set; }
        public Thickness ContentPadding { get => _contentPadding; set => _contentPadding = value; }
        public int ActiveIndex { get => _activeIndex; set => _activeIndex = value; }

        private StackLayout ContentLayout { get; set; }
        private Image _IconImage { get;  set; }
        private Label _TitleLabel { get;  set; }
    }

    public class WizardStep
    {
        public string Title { get; set; }
        public ImageSource Icon { get; set; }
        public View Content { get; set; }
        public int StepIndex { get; internal set; }
    }
}