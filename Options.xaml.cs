using System.Text.Json;
using TriviaGameProject.Resources.Themes;
using static System.Formats.Asn1.AsnWriter;

namespace TriviaGameProject;

public partial class Options : ContentPage
{
    public string ThemeName;
    public string FontStyle;
    string FontSizePref;
    string QuestionsPerRoundPref;
    string SPScoresFile = Path.Combine(FileSystem.Current.AppDataDirectory, "SPScores.json");
    string jsonString;
    List<Scores> scores = new List<Scores>();

    public Options()
	{
		InitializeComponent();
        CheckOptions();
	}
    //All of the options button work off of preferences that are fed into the CheckOptions() method
    private void LightThemeBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("Theme_Choice", "LIGHT");
        CheckOptions();

    }

    private void DarkThemeBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("Theme_Choice", "DARK");
        CheckOptions();
    }

    private void StandardBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("FontStyle_Choice", "DEFAULT");
        CheckOptions();
    }

    private void RetroBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("FontStyle_Choice", "PIXEL");
        CheckOptions();
    }

    private void FancyBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("FontStyle_Choice", "FANCY");
        CheckOptions();
    }

    private void SmallFontBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("FontSize_Choice", "SMALL");
        CheckOptions();
    }

    private void MediumFontBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("FontSize_Choice", "MEDIUM");
        CheckOptions();
    }

    private void LargeFontBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("FontSize_Choice", "LARGE");
        CheckOptions();
    }
    //tried to make 1 mega function to do everything for every page but found the Resource[] ones only effect the page their written on
    //so I have made seperate functions in the other pages just for the sake of it working however it still works fine for this page
    public void CheckOptions()
    {
        ThemeName = Preferences.Default.Get("Theme_Choice", "LIGHT");
        if(ThemeName == "DARK")
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(new DarkTheme());
        }
        else if(ThemeName == "LIGHT")
        {
            Application.Current.Resources.MergedDictionaries.Clear();
            Application.Current.Resources.MergedDictionaries.Add(new LightTheme());
        }

        FontStyle = Preferences.Default.Get("FontStyle_Choice", "DEFAULT");
        if(FontStyle == "DEFAULT")
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

        FontSizePref = Preferences.Default.Get("FontSize_Choice", "MEDIUM");
        if (FontSizePref == "SMALL")
        {
            Resources["FontSize"] = 10;
        }
        else if (FontSizePref == "MEDIUM")
        {
            Resources["FontSize"] = 20;
        }
        else if(FontSizePref == "LARGE")
        {
            Resources["FontSize"] = 30;
        }

    }

    private void ThreeRoundsBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("RoundsPerGame", "3");
    }

    private void FourRoundsBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("RoundsPerGame", "4");
    }

    private void FiveRoundsBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("RoundsPerGame", "5");
    }
    private void FiveperRoundBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("QuestionsPerRound", "5");
    }

    private void TenperRoundBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("QuestionsPerRound", "10");
    }

    private void FifteenperRoundBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("QuestionsPerRound", "15");
    }
    //I wanted to add this for replayability
    private void ClearSPScoreFile_Clicked(object sender, EventArgs e)
    {
        try
        {
            using (StreamReader reader = new StreamReader(SPScoresFile))
            {
                jsonString = reader.ReadToEnd();
                scores = JsonSerializer.Deserialize<List<Scores>>(jsonString);

            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Single Player Scores deserializing issue", "There has been an issue accessing SPScoresFile", "ok");
        }
        scores[0].HighScore = 30;
        scores[0].SecondScore = 25;
        scores[0].ThirdScore = 20;
        scores[0].FourthScore = 15;
        scores[0].FifthScore = 10;
        scores[0].HighScoreName = " ";
        scores[0].SecondScoreName = " ";
        scores[0].ThirdScoreName = " ";
        scores[0].FourthScoreName = " ";
        scores[0].FifthScoreName = " ";
        jsonString = JsonSerializer.Serialize(scores);
        using (StreamWriter writer = new StreamWriter(SPScoresFile))
        {
            writer.Write(jsonString);
        }
    }

    private void HomeButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }
}