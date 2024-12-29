using System.Text.Json;

namespace TriviaGameProject;

public partial class ScoreScreen : ContentPage
{
    string SPScoresFile = Path.Combine(FileSystem.Current.AppDataDirectory, "SPScores.json");
    string GameStateFile = Path.Combine(FileSystem.Current.AppDataDirectory, "GameState.json");
    string PlayerNameFile = Path.Combine(FileSystem.Current.AppDataDirectory, "PlayerNames.json");
    string jsonString;
    string HSText = "30";
    string SecText = "25";
    string ThirText = "20";
    string FourText = "15";
    string FiveText = "10";
    List<Game> games = new List<Game>();
    int basescore = 0;
    List<Scores> scores = new List<Scores>();
    List<PlayerNames> playerNames = new List<PlayerNames>();
    public ScoreScreen()
	{
		InitializeComponent();
        getGameState();
        getPlayerNames();
        if (games[0].PlayerNum == 1)
        {
            GetSpScores();
        }
        else
        {
            GetMpScores();
        }
	}

    private void HomeButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

    public void getGameState()
    {

        try
        {
            using (StreamReader reader = new StreamReader(GameStateFile))
            {
                jsonString = reader.ReadToEnd();
                games = JsonSerializer.Deserialize<List<Game>>(jsonString);
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Game state deserializing issue", "There has been an issue accessing the GameState file", "ok");
        }
    }

    public void getPlayerNames()
    {
        try
        {
            using (StreamReader reader = new StreamReader(PlayerNameFile))
            {
                jsonString = reader.ReadToEnd();
                playerNames = JsonSerializer.Deserialize<List<PlayerNames>>(jsonString);
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Player name deserializing issue", "There has been an issue accessing the PlayerNames file", "ok");
        }
    }

    //Just doing it for single player to start so I can make sure it works before I tweak it for Multiplayer
    private void GetSpScores()
	{
       if(File.Exists(SPScoresFile))
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
            DisplayAlert("Player name deserializing issue", "There has been an issue accessing the PlayerNames file", "ok");
        }//Checks the amounts in the file and if the criteria is met replaces and saves them
            if (games[0].Player1Score > scores[0].HighScore)
            {
                scores[0].HighScore = games[0].Player1Score;
                scores[0].HighScoreName = playerNames[0].Player1Name;
                HSText = "1. " + playerNames[0].Player1Name + " " + scores[0].HighScore;
            }
            else if(games[0].Player1Score < scores[0].HighScore && games[0].Player1Score > scores[0].SecondScore)
            {
                scores[0].SecondScore = games[0].Player1Score;
                scores[0].SecondScoreName = playerNames[0].Player1Name;
                SecText = "2. " + playerNames[0].Player1Name + " " + scores[0].SecondScore;
            }
            else if(games[0].Player1Score < scores[0].HighScore && games[0].Player1Score < scores[0].SecondScore && games[0].Player1Score > scores[0].ThirdScore)
            {
                scores[0].ThirdScore = games[0].Player1Score;
                scores[0].ThirdScoreName = playerNames[0].Player1Name;
                ThirText = "3. " + playerNames[0].Player1Name + " " + scores[0].ThirdScore;
            }
            else if(games[0].Player1Score < scores[0].HighScore && games[0].Player1Score < scores[0].SecondScore && games[0].Player1Score < scores[0].ThirdScore && games[0].Player1Score > scores[0].FourthScore)
            {
                scores[0].FourthScore = games[0].Player1Score;
                scores[0].FourthScoreName = playerNames[0].Player1Name;
                FourText = "4. " + playerNames[0].Player1Name + " " + scores[0].FourthScore;
            }
            else if(games[0].Player1Score < scores[0].HighScore && games[0].Player1Score < scores[0].SecondScore && games[0].Player1Score < scores[0].ThirdScore && games[0].Player1Score < scores[0].FourthScore && games[0].Player1Score > scores[0].FifthScore)
            {
                scores[0].FifthScore = games[0].Player1Score;
                scores[0].FifthScoreName = playerNames[0].Player1Name;
                FiveText = "5. " + playerNames[0].Player1Name + " " + scores[0].FifthScore;
            }
            else
            {
                DisplayAlert("Unlucky", "Better Luck Next Time!", "ok");
            }
            //Making sure things show up like their supposed to by checking for nulls
            if (scores[0].HighScore == null)
            {
                HSText = "1. 30";
            }
            if (scores[0].SecondScore == null)
            {
                SecText = "2. 25";
            }
            if (scores[0].ThirdScore == null)
            {
                ThirText = "3. 20";
            }
            if (scores[0].FourthScore == null)
            {
                FourText = "4. 15";
            }
            if (scores[0].FifthScore == null)
            {
                FiveText = "5. 10";
            }

            if (scores[0].HighScore != null)
            {
                HSText = "1. " + scores[0].HighScoreName + " " + scores[0].HighScore;
            }
            if (scores[0].SecondScore != null)
            {
                SecText = "2. " + scores[0].SecondScoreName + " " + scores[0].SecondScore;
            }
            if (scores[0].ThirdScore != null)
            {
                ThirText = "3. " + scores[0].ThirdScoreName + " " + scores[0].ThirdScore;
            }
            if (scores[0].FourthScore == null)
            {
                FourText = "4. " + scores[0].FourthScoreName + " " + scores[0].FourthScore;
            }
            if (scores[0].FifthScore == null)
            {
                FiveText = "5. " + scores[0].FifthScoreName + " " + scores[0].FifthScore;
            }
        }
       else
        {

            scores[0].HighScore = 30;
            scores[0].SecondScore = 25;
            scores[0].ThirdScore = 20;
            scores[0].FourthScore = 15;
            scores[0].FifthScore = 10;

            HSText = "1. " + scores[0].HighScore.ToString();
            SecText = "2. " + scores[0].SecondScore.ToString();
            ThirText = "3. " + scores[0].ThirdScore.ToString();
            FourText = "4. " + scores[0].FourthScore.ToString();
            FiveText = "5. " + scores[0].FifthScore.ToString();

        }

        FirstScoreLbl.Text = HSText;
        SecondScoreLbl.Text = SecText;
        ThirdScoreLbl.Text = ThirText;
        FourthScoreLbl.Text = FourText;
        FifthScoreLbl.Text = FiveText;

        jsonString = JsonSerializer.Serialize(scores);

        using (StreamWriter writer = new StreamWriter(SPScoresFile))
        {
            writer.Write(jsonString);
        }
    }
    private void GetMpScores()
    {

    }
}