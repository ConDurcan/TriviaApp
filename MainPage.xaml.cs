namespace TriviaGameProject
{
    public partial class MainPage : ContentPage
    {
        string FontStyle;
        string FontSizePref;
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Options options = new Options();
            options.CheckOptions();
            CheckFontStyle();
            CheckFontSize();

        }

        private void CheckFontSize()
        {
            FontSizePref = Preferences.Default.Get("FontSize_Choice", "MEDIUM");
            if (FontSizePref == "SMALL")
            {
                Resources["FontSize"] = 10;
            }
            else if (FontSizePref == "MEDIUM")
            {
                Resources["FontSize"] = 20;
            }
            else if (FontSizePref == "LARGE")
            {
                Resources["FontSize"] = 30;
            }
        }
    

        private void NewGameBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PlayerInfoScreen());
        }

        private void ContinueGameBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void OptionsBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Options());
        }

        private void CheckFontStyle()
        {
            FontStyle = Preferences.Default.Get("FontStyle_Choice", "DEFAULT");
            if (FontStyle == "DEFAULT")
            {
                Resources["FontStyle"] = "OpenSansRegular";
            }
            else if (FontStyle == "PIXEL")
            {
                Resources["FontStyle"] = "RetroFont";
            }
            else if (FontStyle == "FANCY")
            {
                Resources["FontStyle"] = "FancyFont";
            }
        }
    
    }

}
