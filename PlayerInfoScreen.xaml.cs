using Microsoft.Maui.Storage;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TriviaGameProject;

public partial class PlayerInfoScreen : ContentPage
{
    public string Player1Name;
    public string Player2Name;
    public string Player3Name;
    public string Player4Name;
    int nullcheck = 0;
    string PlayerNameFile = Path.Combine(FileSystem.Current.AppDataDirectory, "PlayerNames.json");
    string GameStateFile = Path.Combine(FileSystem.Current.AppDataDirectory, "GameState.json");
    string fileString;
    string FontStyle;
    string FontSizePref;
    private int PlayerNum;
    private ViewModel viewmodel;
    List<Game> games = new List<Game>();
    List<PlayerNames> playerNames = new List<PlayerNames>();
    
   
	public PlayerInfoScreen()
	{
		InitializeComponent();
        viewmodel = new ViewModel();
        BindingContext = viewmodel;
        CheckFontStyle();
        CheckFontSize();
	}
    //Player Buttons control the visibility of the Name entries and clear onws you may have written in and changed your mind, they also change the
    //PlayerNum variable so I can set the game up for the correct amount of players.
    private void OnePlayerBtn_Clicked(object sender, EventArgs e)
    {
        PlayerNameEntry1.IsVisible = true;
        PlayerNameEntry2.IsVisible = false;
        PlayerNameEntry3.IsVisible = false;
        PlayerNameEntry4.IsVisible = false;
        PlayerNameEntry2.Text = string.Empty;
        PlayerNameEntry3.Text = string.Empty;
        PlayerNameEntry4.Text = string.Empty;
        BeginBtn.IsVisible = true;
        PlayerNum = 1;
    }

    private void TwoPlayerBtn_Clicked(object sender, EventArgs e)
    {
        PlayerNameEntry1.IsVisible = true;
        PlayerNameEntry2.IsVisible =true;
        PlayerNameEntry3.IsVisible = false;
        PlayerNameEntry4.IsVisible = false;
        PlayerNameEntry3.Text = string.Empty;
        PlayerNameEntry4.Text = string.Empty;
        BeginBtn.IsVisible = true;
        PlayerNum = 2;
    }

    private void ThreePlayerBtn_Clicked(object sender, EventArgs e)
    {
        PlayerNameEntry1.IsVisible = true;
        PlayerNameEntry2.IsVisible = true;
        PlayerNameEntry3.IsVisible =true;
        PlayerNameEntry4.IsVisible = false;
        PlayerNameEntry4.Text = string.Empty;
        BeginBtn.IsVisible = true;
        PlayerNum = 3;
    }

    private void FourPlayerBtn_Clicked(object sender, EventArgs e)
    {
        PlayerNameEntry1.IsVisible = true;
        PlayerNameEntry2.IsVisible = true;
        PlayerNameEntry3.IsVisible = true;
        PlayerNameEntry4.IsVisible = true;
        BeginBtn.IsVisible = true;
        PlayerNum = 4;
    }
    //Each page has these for persistence of certain options as I found that doing options.CheckOptions() just changes things for the options page
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
    //Each page has these for persistence of certain options as I found that doing options.CheckOptions() just changes things for the options page
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
    //Just saves Player names to a JSON with the int input their to tell the game how many names its saving.
    private void SaveNames(int playnum)
    {
        if (playnum == 1 && PlayerNameEntry1.Text != null)
        {
            playerNames.Add(new PlayerNames(PlayerNameEntry1.Text));
            nullcheck = 0;
        }
        else if (playnum == 2 && PlayerNameEntry1.Text != null && PlayerNameEntry2.Text != null)
        {
            playerNames.Add(new PlayerNames(PlayerNameEntry1.Text, PlayerNameEntry2.Text));
            nullcheck = 0;
        }
        else if (playnum == 3 && PlayerNameEntry1.Text != null && PlayerNameEntry2.Text != null && PlayerNameEntry3 != null)
        {
            playerNames.Add(new PlayerNames(PlayerNameEntry1.Text, PlayerNameEntry2.Text, PlayerNameEntry3.Text));
            nullcheck = 0;
        }
        else if (playnum == 4 && PlayerNameEntry1.Text != null && PlayerNameEntry2.Text != null && PlayerNameEntry3 != null && PlayerNameEntry4.Text != null)
        {
            playerNames.Add(new PlayerNames(PlayerNameEntry1.Text, PlayerNameEntry2.Text, PlayerNameEntry3.Text, PlayerNameEntry4.Text));
            nullcheck = 0;
        }
        else
        {
            DisplayAlert("Name Entry Error", "Please make sure all Players have a name!", "Ok");
            nullcheck = 1;
        }
       
        
            fileString = JsonSerializer.Serialize(playerNames);

            using (StreamWriter writer = new StreamWriter(PlayerNameFile))
            {
                writer.Write(fileString);
            }
        
    }
    //Creates new game instance and overwrites the previous save
    private void NewGame(int playnum)
    {
        games.Add(new Game(playnum));
        
            fileString = JsonSerializer.Serialize(games);

            using (StreamWriter writer = new StreamWriter(GameStateFile))
            {
                writer.Write(fileString);
            }
        
    }
    //Saves PlayerNames and amount of players playing ans Serializes them so I can access them later.
    //Also overwrites the game save with using the game class' 1 input constructor
    //This overwrites so when the JSON is read in the GameScreen it starts the round from the beginning
    //Sends you to the GameScreen that corresponds with how many players are playing.
    private void BeginBtn_Clicked(object sender, EventArgs e)
    {
        SaveNames(PlayerNum);
        NewGame(PlayerNum);
        if (nullcheck == 0)
        {
            if(PlayerNum == 1)
            {
                Navigation.PushAsync(new SPGameScreen());
            }
            else
            {
                Navigation.PushAsync(new MPGameScreen());
            }
        }
    }
}