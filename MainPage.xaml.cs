using System.Text.Json;

namespace TriviaGameProject
{
    public partial class MainPage : ContentPage
    {
        string FontStyle;
        string FontSizePref;
        string GameStateFile = Path.Combine(FileSystem.Current.AppDataDirectory, "GameState.json");
        string jsonString;
        List<Game> games = new List<Game>();
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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
        //This button reads the GameState file and deserializes it to a list of games, then checks the player number to determine which game screen to push to.
        //The setupGame method is called in the game screen to set up the game for the correct number of players and restore the previous Games state.
        private void ContinueGameBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (StreamReader reader = new StreamReader(GameStateFile))
                {
                    jsonString = reader.ReadToEnd();
                    games = JsonSerializer.Deserialize<List<Game>>(jsonString);
                    if (games[0].PlayerNum == 1)
                    {
                        Navigation.PushAsync(new SPGameScreen());
                    }
                    else if (games[0].PlayerNum > 2)
                    {
                        Navigation.PushAsync(new MPGameScreen());
                    }
                }
                
            }
            catch (Exception ex)
            {
                DisplayAlert("Game state deserializing issue", "There has been an issue accessing the GameState file", "ok");
            }
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
