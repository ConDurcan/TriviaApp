using System.Text.Json;

namespace TriviaGameProject;

public partial class ScoreScreen : ContentPage
{//The idea for this page was to among showing the winner and runners up the time the game took to complete so you could race yourself in single player
    //But getting the base functionality working took alot longer than I though and so the timer kind of fell by the wayside
    string SPScoresFile = Path.Combine(FileSystem.Current.AppDataDirectory, "SPScores.json");
    string MPScoresFile = Path.Combine(FileSystem.Current.AppDataDirectory, "MPScores.json");
    string GameStateFile = Path.Combine(FileSystem.Current.AppDataDirectory, "GameState.json");
    string PlayerNameFile = Path.Combine(FileSystem.Current.AppDataDirectory, "PlayerNames.json");
    string jsonString;
    string HSText = "1. 30";
    string SecText = "2. 25";
    string ThirText = "3. 20";
    string FourText = "4. 15";
    string FiveText = "5. 10";
    List<Game> games = new List<Game>();
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
            DisplayAlert("Single Player Scores deserializing issue", "There has been an issue accessing SPScoresFile", "ok");
        }//Checks the scores in the file and if the criteria is met replaces and saves them
            if (games[0].Player1Score > scores[0].HighScore)
            {
                scores[0].HighScore = games[0].Player1Score;
                scores[0].HighScoreName = playerNames[0].Player1Name;
                HSText = "1. " + playerNames[0].Player1Name + " " + scores[0].HighScore;
            }
            else if(games[0].Player1Score < scores[0].HighScore && games[0].Player1Score > scores[0].SecondScore || games[0].Player1Score == scores[0].HighScore)
            {
                scores[0].SecondScore = games[0].Player1Score;
                scores[0].SecondScoreName = playerNames[0].Player1Name;
                SecText = "2. " + playerNames[0].Player1Name + " " + scores[0].SecondScore;
            }
            else if(games[0].Player1Score < scores[0].HighScore && games[0].Player1Score < scores[0].SecondScore && games[0].Player1Score > scores[0].ThirdScore || games[0].Player1Score == scores[0].SecondScore)
            {
                scores[0].ThirdScore = games[0].Player1Score;
                scores[0].ThirdScoreName = playerNames[0].Player1Name;
                ThirText = "3. " + playerNames[0].Player1Name + " " + scores[0].ThirdScore;
            }
            else if(games[0].Player1Score < scores[0].HighScore && games[0].Player1Score < scores[0].SecondScore && games[0].Player1Score < scores[0].ThirdScore && games[0].Player1Score > scores[0].FourthScore || games[0].Player1Score == scores[0].ThirdScore)
            {
                scores[0].FourthScore = games[0].Player1Score;
                scores[0].FourthScoreName = playerNames[0].Player1Name;
                FourText = "4. " + playerNames[0].Player1Name + " " + scores[0].FourthScore;
            }
            else if(games[0].Player1Score < scores[0].HighScore && games[0].Player1Score < scores[0].SecondScore && games[0].Player1Score < scores[0].ThirdScore && games[0].Player1Score < scores[0].FourthScore && games[0].Player1Score > scores[0].FifthScore || games[0].Player1Score == scores[0].FourthScore)
            {
                scores[0].FifthScore = games[0].Player1Score;
                scores[0].FifthScoreName = playerNames[0].Player1Name;
                FiveText = "5. " + playerNames[0].Player1Name + " " + scores[0].FifthScore;
            }
            else
            {
                DisplayAlert("Unlucky", "Better Luck Next Time!", "ok");
            }
            //Making sure things show up like their supposed to by checking for nulls so we dont get empty strings
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
        if(File.Exists(MPScoresFile))
        {
            try
            {   //For whatever reason this overwites the file with the base constructor
                using (StreamReader reader = new StreamReader(MPScoresFile))
                {
                    jsonString = reader.ReadToEnd();
                    scores = JsonSerializer.Deserialize<List<Scores>>(jsonString);

                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Single Player Scores deserializing issue", "There has been an issue accessing SPScoresFile", "ok");
            }//As you've probably noticed by now I like a big if statement
            //This checks for the highest score to display a winner 
            //Tiebreakers are based on total score
            //If total score is same i suppose youre going to have to play again to see who wins
            //My brain is melting trying to keep track of this so I hope it works
            if (games[0].PlayerNum == 2)
            {
                if (games[0].Player1Rounds > games[0].Player2Rounds || games[0].Player1Rounds == games[0].Player2Rounds && games[0].Player1TotalScore > games[0].Player2TotalScore)
                {
                    HSText = "1. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                    SecText = "2. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                    FirstScoreLbl.Text = HSText;
                    SecondScoreLbl.Text = SecText;
                }
                else if (games[0].Player2Rounds > games[0].Player1Rounds || games[0].Player2Rounds == games[0].Player1Rounds && games[0].Player2TotalScore > games[0].Player1TotalScore)
                {
                    HSText = "1. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                    SecText = "2. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                    FirstScoreLbl.Text = HSText;
                    SecondScoreLbl.Text = SecText;
                }
                else
                {
                    HSText = "This is as much of a tie as I've seen I guess you'll have to play again";
                    FirstScoreLbl.Text = HSText;
                }
            }
            else if (games[0].PlayerNum == 3)
            {
                if (games[0].Player1Rounds > games[0].Player2Rounds && games[0].Player1Rounds > games[0].Player3Rounds && games[0].Player2Rounds > games[0].Player3Rounds)
                {
                    HSText = "1. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                    SecText = "2. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                    ThirText = "3. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                    FirstScoreLbl.Text = HSText;
                    SecondScoreLbl.Text = SecText;
                    ThirdScoreLbl.Text = ThirText;
                }
                else if (games[0].Player2Rounds > games[0].Player1Rounds && games[0].Player2Rounds > games[0].Player3Rounds && games[0].Player1Rounds > games[0].Player3Rounds)
                {
                    HSText = "1. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                    SecText = "2. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                    ThirText = "3. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                    FirstScoreLbl.Text = HSText;
                    SecondScoreLbl.Text = SecText;
                    ThirdScoreLbl.Text = ThirText;
                }
                else if (games[0].Player3Rounds > games[0].Player1Rounds && games[0].Player3Rounds > games[0].Player2Rounds && games[0].Player1Rounds > games[0].Player2Rounds)
                {
                    HSText = "1. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                    SecText = "2. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                    ThirText = "3. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                    FirstScoreLbl.Text = HSText;
                    SecondScoreLbl.Text = SecText;
                    ThirdScoreLbl.Text = ThirText;
                }
                else if (games[0].Player1Rounds == games[0].Player2Rounds && games[0].Player1Rounds > games[0].Player3Rounds)
                {
                    if (games[0].Player1TotalScore > games[0].Player2TotalScore)
                    {
                        HSText = "1. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                        SecText = "2. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                        ThirText = "3. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                        FirstScoreLbl.Text = HSText;
                        SecondScoreLbl.Text = SecText;
                        ThirdScoreLbl.Text = ThirText;
                    }
                    else
                    {
                        HSText = "1. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                        SecText = "2. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                        ThirText = "3. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                        FirstScoreLbl.Text = HSText;
                        SecondScoreLbl.Text = SecText;
                        ThirdScoreLbl.Text = ThirText;
                    }
                }
                else if (games[0].Player1Rounds == games[0].Player3Rounds && games[0].Player1Rounds > games[0].Player2Rounds)
                {
                    if (games[0].Player1TotalScore > games[0].Player3TotalScore)
                    {
                        HSText = "1. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                        SecText = "2. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                        ThirText = "3. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                        FirstScoreLbl.Text = HSText;
                        SecondScoreLbl.Text = SecText;
                        ThirdScoreLbl.Text = ThirText;
                    }
                    else
                    {
                        HSText = "1. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                        SecText = "2. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                        ThirText = "3. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                        FirstScoreLbl.Text = HSText;
                        SecondScoreLbl.Text = SecText;
                        ThirdScoreLbl.Text = ThirText;
                    }
                }
                else if (games[0].Player1Rounds == games[0].Player2Rounds && games[0].Player1Rounds == games[0].Player3Rounds)
                {
                    if (games[0].Player1TotalScore > games[0].Player2TotalScore && games[0].Player1TotalScore > games[0].Player3TotalScore && games[0].Player2TotalScore > games[0].Player3TotalScore)
                    {
                        HSText = "1. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                        SecText = "2. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                        ThirText = "3. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                        FirstScoreLbl.Text = HSText;
                        SecondScoreLbl.Text = SecText;
                        ThirdScoreLbl.Text = ThirText;
                    }
                    else if (games[0].Player2TotalScore > games[0].Player1TotalScore && games[0].Player2TotalScore > games[0].Player3TotalScore && games[0].Player1TotalScore > games[0].Player3TotalScore)
                    {
                        HSText = "1. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                        SecText = "2. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                        ThirText = "3. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                        FirstScoreLbl.Text = HSText;
                        SecondScoreLbl.Text = SecText;
                        ThirdScoreLbl.Text = ThirText;
                    }
                    else if (games[0].Player3TotalScore > games[0].Player1TotalScore && games[0].Player3TotalScore > games[0].Player2TotalScore && games[0].Player1TotalScore > games[0].Player2TotalScore)
                    {
                        HSText = "1. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                        SecText = "2. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                        ThirText = "3. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                        FirstScoreLbl.Text = HSText;
                        SecondScoreLbl.Text = SecText;
                        ThirdScoreLbl.Text = ThirText;
                    }
                    else
                    {
                        HSText = "The Three of you are as Good as each other it seems, or as bad I cant see the scores from here Play Again";
                        SecText = "It could also be something I havent planned for but hey thems the breaks";
                        FirstScoreLbl.Text = HSText;
                        SecondScoreLbl.Text = SecText;
                    }
                }//I'm throwing a hail mary here nesting a bunch of if statements to try and get the scores to display correctly
                 //I Imagine this is giving you a headache to read as much as it is for me to write, what even is a proper coding practice anyway huh?
                 //This nests the if statements to check for the highest score and then the second highest and then the third highest
                else if (games[0].PlayerNum == 4)
                {
                    if (games[0].Player1Rounds > games[0].Player2Rounds && games[0].Player1Rounds > games[0].Player3Rounds && games[0].Player1Rounds > games[0].Player4Rounds && games[0].Player2Rounds > games[0].Player3Rounds && games[0].Player2Rounds > games[0].Player4Rounds && games[0].Player3Rounds > games[0].Player4Rounds || games[0].Player1Rounds == games[0].Player2Rounds && games[0].Player1Rounds == games[0].Player3Rounds && games[0].Player1Rounds == games[0].Player4Rounds && games[0].Player2Rounds == games[0].Player3Rounds && games[0].Player2Rounds == games[0].Player4Rounds && games[0].Player3Rounds == games[0].Player4Rounds)
                    {
                        if (games[0].Player2Rounds > games[0].Player3Rounds && games[0].Player2Rounds > games[0].Player4Rounds || games[0].Player2Rounds == games[0].Player3Rounds && games[0].Player2Rounds == games[0].Player4Rounds)
                        {
                            if (games[0].Player3Rounds > games[0].Player4Rounds || games[0].Player3Rounds == games[0].Player4Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                SecText = "2. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                ThirText = "3. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                FourText = "4. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                            else if (games[0].Player4Rounds > games[0].Player3Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                SecText = "2. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                ThirText = "3. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                FourText = "4. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                        }
                        else if (games[0].Player3Rounds > games[0].Player2Rounds && games[0].Player3Rounds > games[0].Player4Rounds)
                        {
                            if (games[0].Player2Rounds > games[0].Player4Rounds || games[0].Player2Rounds == games[0].Player4Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                SecText = "2. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                ThirText = "3. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                FourText = "4. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                            else if (games[0].Player4Rounds > games[0].Player2Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                SecText = "2. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                ThirText = "3. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                FourText = "4. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                        }
                        else if (games[0].Player4Rounds > games[0].Player2Rounds && games[0].Player4Rounds > games[0].Player3Rounds)
                        {
                            if (games[0].Player2Rounds > games[0].Player3Rounds || games[0].Player2Rounds == games[0].Player3Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                SecText = "2. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                ThirText = "3. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                FourText = "4. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                            else if (games[0].Player3Rounds > games[0].Player2Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                SecText = "2. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                ThirText = "3. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                FourText = "4. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                        }
                    }
                    else if (games[0].Player2Rounds > games[0].Player1Rounds && games[0].Player2Rounds > games[0].Player3Rounds && games[0].Player2Rounds > games[0].Player4Rounds)
                    {
                        if (games[0].Player1Rounds > games[0].Player3Rounds && games[0].Player1Rounds > games[0].Player4Rounds || games[0].Player1Rounds == games[0].Player3Rounds && games[0].Player1Rounds == games[0].Player4Rounds)
                        {
                            if (games[0].Player3Rounds > games[0].Player4Rounds || games[0].Player3Rounds == games[0].Player4Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                SecText = "2. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                ThirText = "3. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                FourText = "4. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                            else if (games[0].Player4Rounds > games[0].Player3Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                SecText = "2. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                ThirText = "3. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                FourText = "4. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                        }
                        else if (games[0].Player3Rounds > games[0].Player4Rounds && games[0].Player3Rounds > games[0].Player1Rounds)
                        {
                            if (games[0].Player1Rounds > games[0].Player4Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                SecText = "2. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                ThirText = "3. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                FourText = "4. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                            else if (games[0].Player4Rounds > games[0].Player1Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                SecText = "2. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                ThirText = "3. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                FourText = "4. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                        }
                        else if (games[0].Player4Rounds > games[0].Player1Rounds && games[0].Player4Rounds > games[0].Player3Rounds)
                        {
                            if (games[0].Player1Rounds > games[0].Player4Rounds || games[0].Player1Rounds == games[0].Player4Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                SecText = "2. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                ThirText = "3. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                FourText = "4. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                            else if (games[0].Player4Rounds > games[0].Player1Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                SecText = "2. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                ThirText = "3. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                FourText = "4. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                            }
                        }
                    }
                    else if (games[0].Player3Rounds > games[0].Player1Rounds && games[0].Player3Rounds > games[0].Player2Rounds && games[0].Player3Rounds > games[0].Player4Rounds)
                    {
                        if (games[0].Player1Rounds > games[0].Player2Rounds && games[0].Player1Rounds > games[0].Player4Rounds || games[0].Player1Rounds > games[0].Player2Rounds && games[0].Player1Rounds > games[0].Player4Rounds)
                        {
                            if (games[0].Player2Rounds > games[0].Player4Rounds || games[0].Player2Rounds == games[0].Player4Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                SecText = "2. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                ThirText = "3. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                FourText = "4. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                            else if (games[0].Player4Rounds > games[0].Player2Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                SecText = "2. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                ThirText = "3. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                FourText = "4. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                        }
                        else if (games[0].Player2Rounds > games[0].Player1Rounds && games[0].Player2Rounds > games[0].Player4Rounds)
                        {
                            if (games[0].Player1Rounds > games[0].Player4Rounds || games[0].Player1Rounds == games[0].Player4Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                SecText = "2. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                ThirText = "3. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                FourText = "4. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                            else if (games[0].Player4Rounds > games[0].Player1Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                SecText = "2. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                ThirText = "3. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                FourText = "4. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                        }
                    }
                    else if (games[0].Player4Rounds > games[0].Player1Rounds && games[0].Player4Rounds > games[0].Player2Rounds && games[0].Player4Rounds > games[0].Player3Rounds)
                    {
                        if (games[0].Player1Rounds > games[0].Player2Rounds && games[0].Player1Rounds > games[0].Player3Rounds || games[0].Player1Rounds == games[0].Player2Rounds && games[0].Player1Rounds == games[0].Player3Rounds)
                        {
                            if (games[0].Player2Rounds > games[0].Player3Rounds || games[0].Player2Rounds == games[0].Player3Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                SecText = "2. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                ThirText = "3. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                FourText = "4. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                            else if (games[0].Player3Rounds > games[0].Player2Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                SecText = "2. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                ThirText = "3. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                FourText = "4. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                        }
                        else if (games[0].Player2Rounds > games[0].Player1Rounds && games[0].Player2Rounds > games[0].Player3Rounds)
                        {
                            if (games[0].Player1Rounds > games[0].Player3Rounds || games[0].Player1Rounds == games[0].Player3Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                SecText = "2. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                ThirText = "3. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                FourText = "4. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                            else if (games[0].Player3Rounds > games[0].Player1Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                SecText = "2. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                ThirText = "3. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                FourText = "4. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                        }
                        else if (games[0].Player3Rounds > games[0].Player1Rounds && games[0].Player3Rounds > games[0].Player2Rounds)
                        {
                            if (games[0].Player1Rounds > games[0].Player2Rounds || games[0].Player1Rounds == games[0].Player2Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                SecText = "2. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                ThirText = "3. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                FourText = "4. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                            else if (games[0].Player2Rounds > games[0].Player1Rounds)
                            {
                                HSText = "1. " + playerNames[0].Player4Name + " " + games[0].Player4TotalScore;
                                SecText = "2. " + playerNames[0].Player3Name + " " + games[0].Player3TotalScore;
                                ThirText = "3. " + playerNames[0].Player2Name + " " + games[0].Player2TotalScore;
                                FourText = "4. " + playerNames[0].Player1Name + " " + games[0].Player1TotalScore;
                                FirstScoreLbl.Text = HSText;
                                SecondScoreLbl.Text = SecText;
                                ThirdScoreLbl.Text = ThirText;
                                FourthScoreLbl.Text = FourText;
                            }
                        }
                    }
                    else
                    {
                        FirstScoreLbl.Text = "I'm sorry I can't seem to figure out who won, I guess you'll have to play again";
                        SecondScoreLbl.Text = "I'm not sure what happened but I'm sure you all did your best";
                    }
                }
            }
        }
    }
}