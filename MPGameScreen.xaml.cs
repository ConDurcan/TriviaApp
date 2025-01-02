using System.Text.Json;
namespace TriviaGameProject;

public partial class MPGameScreen : ContentPage
{
    string PlayerNameFile = Path.Combine(FileSystem.Current.AppDataDirectory, "PlayerNames.json");
    string GameStateFile = Path.Combine(FileSystem.Current.AppDataDirectory, "GameState.json");
    string MPScoresFile = Path.Combine(FileSystem.Current.AppDataDirectory, "MPScores.json");
    string jsonString;
    string FontStyle;
    string FontSizePref;
    string QuestionsPerRoundPref;
    string RoundNumPref;
    string ScoreDisplayText;
    string Player1Name;
    string Player2Name;
    string Player3Name;
    string Player4Name;
    int QuestionsPerRound;
    int PlayerNum;
    int RandomNum;
    int QuestionNum = 0;
    int Player1Score = 0;
    int Player2Score = 0;
    int Player3Score = 0;
    int Player4Score = 0;
    int Player1TotalScore = 0;
    int Player2TotalScore = 0;
    int Player3TotalScore = 0;
    int Player4TotalScore = 0;
    int HighestScorer;
    int Player1Rounds = 0;
    int Player2Rounds = 0;
    int Player3Rounds = 0;
    int Player4Rounds = 0;
    int PlayerTurn;
    int RoundNum;
    int RoundCount = 0;
    bool GameOver = false;
    bool Tiebreak = false;
    bool ContinueGame;
    QuestionResponse questionInput;
    Random random = new Random();
    List<Game> games = new List<Game>();
    List<PlayerNames> playerNames = new List<PlayerNames>();
    List<Question> question = new List<Question>();
    HttpClient client = new HttpClient();
    private ViewModel viewmodel;
    public MPGameScreen()
	{
        InitializeComponent();
        CheckFontSize();
        CheckFontStyle();
        getGameState();
        getPlayerNames();
        CheckRoundLength();
        CheckRoundPerGame();
        SetupGame();
        client = new HttpClient();
        viewmodel = new ViewModel();
        BindingContext = viewmodel;
    }
    //If game is not over and you leave it saves so when you return you can continue
    protected override void OnDisappearing()
    {
        if (GameOver = false)
        {

            games[0].PlayerNum = PlayerNum;
            games[0].TurnNum = PlayerTurn;
            games[0].RoundNum = RoundNum;
            games[0].QuestionNum = QuestionNum;
            games[0].Player1Score = Player1Score;
            games[0].Player2Score = Player2Score;
            games[0].Player3Score = Player3Score;
            games[0].Player4Score = Player4Score;
            games[0].Player1Rounds = Player1Rounds;
            games[0].Player2Rounds = Player2Rounds;
            games[0].Player3Rounds = Player3Rounds;
            games[0].Player4Rounds = Player4Rounds;
            games[0].Player1TotalScore = Player1TotalScore;
            games[0].Player2TotalScore = Player2TotalScore;
            games[0].Player3TotalScore = Player3TotalScore;
            games[0].Player4TotalScore = Player4TotalScore;
            

            jsonString = JsonSerializer.Serialize(games);

            using (StreamWriter writer = new StreamWriter(GameStateFile))
            {
                writer.Write(jsonString);
            }
        }
        base.OnDisappearing();
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

    //Http client grabs questions and loads first question
    //Resets QuestionNum so in the case of needing more questions it will start at 0
    //Trying to prevent it from asking an extra question when it shouldnt with the if statement
    //Can cause an issue with the round length I may not be able to fix in time 
    private async Task GetQuestions()
    {
        
            var response = await client.GetAsync("https://opentdb.com/api.php?amount=50&type=multiple");
            if (response.IsSuccessStatusCode)
            {
                string contents = await response.Content.ReadAsStringAsync();
                questionInput = JsonSerializer.Deserialize<QuestionResponse>(contents);
                QuestionNum = 0;
            questionInput.results[QuestionNum].question = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].question);
            TheQuestion.Text = questionInput.results[0].question;
            }
        
       

    }
    //Function to randomize the correct answer
    private async Task AnswerAssign()
    {
        RandomNum = random.Next(1, 4 + 1);
        if (RandomNum == 1)
        {
            questionInput.results[QuestionNum].correct_answer = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].correct_answer);
            questionInput.results[QuestionNum].incorrect_answers[0] = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].incorrect_answers[0]);
            questionInput.results[QuestionNum].incorrect_answers[1] = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].incorrect_answers[1]);
            questionInput.results[QuestionNum].incorrect_answers[2] = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].incorrect_answers[2]);
            ButtonA.Text = questionInput.results[QuestionNum].correct_answer;
            ButtonB.Text = questionInput.results[QuestionNum].incorrect_answers[0];
            ButtonC.Text = questionInput.results[QuestionNum].incorrect_answers[1];
            ButtonD.Text = questionInput.results[QuestionNum].incorrect_answers[2];
        }
        else if (RandomNum == 2)
        {
            questionInput.results[QuestionNum].correct_answer = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].correct_answer);
            questionInput.results[QuestionNum].incorrect_answers[0] = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].incorrect_answers[0]);
            questionInput.results[QuestionNum].incorrect_answers[1] = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].incorrect_answers[1]);
            questionInput.results[QuestionNum].incorrect_answers[2] = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].incorrect_answers[2]);
            ButtonA.Text = questionInput.results[QuestionNum].incorrect_answers[0];
            ButtonB.Text = questionInput.results[QuestionNum].correct_answer;
            ButtonC.Text = questionInput.results[QuestionNum].incorrect_answers[1];
            ButtonD.Text = questionInput.results[QuestionNum].incorrect_answers[2];
        }
        else if (RandomNum == 3)
        {
            questionInput.results[QuestionNum].correct_answer = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].correct_answer);
            questionInput.results[QuestionNum].incorrect_answers[0] = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].incorrect_answers[0]);
            questionInput.results[QuestionNum].incorrect_answers[1] = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].incorrect_answers[1]);
            questionInput.results[QuestionNum].incorrect_answers[2] = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].incorrect_answers[2]);
            ButtonA.Text = questionInput.results[QuestionNum].incorrect_answers[0];
            ButtonB.Text = questionInput.results[QuestionNum].incorrect_answers[1];
            ButtonC.Text = questionInput.results[QuestionNum].correct_answer;
            ButtonD.Text = questionInput.results[QuestionNum].incorrect_answers[2];
        }
        else if (RandomNum == 4)
        {
            questionInput.results[QuestionNum].correct_answer = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].correct_answer);
            questionInput.results[QuestionNum].incorrect_answers[0] = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].incorrect_answers[0]);
            questionInput.results[QuestionNum].incorrect_answers[1] = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].incorrect_answers[1]);
            questionInput.results[QuestionNum].incorrect_answers[2] = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].incorrect_answers[2]);
            ButtonA.Text = questionInput.results[QuestionNum].incorrect_answers[0];
            ButtonB.Text = questionInput.results[QuestionNum].incorrect_answers[1];
            ButtonC.Text = questionInput.results[QuestionNum].incorrect_answers[2];
            ButtonD.Text = questionInput.results[QuestionNum].correct_answer;
        }
        else
        {
            DisplayAlert("RandomNum Error", "Your RandomNum dont work right on line 85", "Damn");
        }
    }
    //Assigns Game variables to the class variables for if you wish to continue a previous game
    //Also assigns first question and button responses on starting the game
    private async void SetupGame()
    {
        ContinueGame = games[0].ContinueGame;
        PlayerTurn = games[0].TurnNum;
        QuestionNum = games[0].QuestionNum;
        Player1Score = games[0].Player1Score;
        Player1TotalScore = games[0].Player1TotalScore;
        Player2Score = games[0].Player2Score;
        Player2TotalScore = games[0].Player2TotalScore;
        Player3Score = games[0].Player3Score;
        Player3TotalScore = games[0].Player3TotalScore;
        Player4Score= games[0].Player4Score;
        Player4TotalScore = games[0].Player4TotalScore;
        Player1Rounds = games[0].Player1Rounds;
        Player2Rounds = games[0].Player2Rounds;
        Player3Rounds = games[0].Player3Rounds;
        Player4Rounds = games[0].Player4Rounds;
        GetQuestions();
        await Task.Delay(1000);
        AnswerAssign();
    }

    private void CheckRounds()
    {
        if (QuestionNum % QuestionsPerRound == 0)
        {//Resets Score so we can do each round seperate and changes the score label so the correct players score shows
            //Also in the event of a tie the winner will be chosen by whoever got the most questions right overall
            //Also checks the PlayerNum so the rounds happen right
            //If the last player has had their turn it ends the round and tells you who won
            
            if (games[0].PlayerNum == 2)
            {
                if (PlayerTurn == 1)
                {
                    PlayerTurn = 2;
                    DisplayAlert("Next Player", "Player " + PlayerTurn + " these ones are for you", "Alrighty Then");
                    Player1TotalScore = Player1Score + Player1TotalScore;
                    ScoreDisplayText = " ";
                    ScoreDisplay.Text = ScoreDisplayText;
                }
                else if (PlayerTurn == 2)
                {
                    Player2TotalScore = Player2Score + Player2TotalScore;
                    ScoreDisplayText = " ";
                    ScoreDisplay.Text = ScoreDisplayText;
                    RoundFinish();
                }
            }
            if (games[0].PlayerNum == 3)
            {
                if (PlayerTurn == 1)
                {
                    PlayerTurn = 2;
                    DisplayAlert("Next Player", "Player " + PlayerTurn + " these ones are for you", "Alrighty Then");
                    Player1TotalScore = Player1Score + Player1TotalScore;
                    ScoreDisplayText = " ";
                    ScoreDisplay.Text = ScoreDisplayText;
                }
                else if (PlayerTurn == 2)
                {
                    PlayerTurn = 3;
                    DisplayAlert("Next Player", "Player " + PlayerTurn + " these ones are for you", "Alrighty Then");
                    Player2TotalScore = Player2Score + Player2TotalScore;
                    ScoreDisplayText = " ";
                    ScoreDisplay.Text = ScoreDisplayText;
                }
                else if (PlayerTurn == 3)
                {
                    Player3TotalScore = Player3Score + Player3TotalScore;
                    ScoreDisplayText = " ";
                    ScoreDisplay.Text = ScoreDisplayText;
                    RoundFinish();
                }
            }
            if (games[0].PlayerNum == 4)
            {
                if (PlayerTurn == 1)
                {
                    PlayerTurn = 2;
                    Player1TotalScore = Player1Score + Player1TotalScore;
                    DisplayAlert("Next Player", "Player " + PlayerTurn + " these ones are for you", "Alrighty Then");
                    ScoreDisplayText = " ";
                    ScoreDisplay.Text = ScoreDisplayText;
                }
                else if (PlayerTurn == 2)
                {
                    PlayerTurn = 3;
                    Player2TotalScore = Player2Score + Player2TotalScore;
                    DisplayAlert("Next Player", "Player " + PlayerTurn + " these ones are for you", "Alrighty Then");
                    ScoreDisplayText = " ";
                    ScoreDisplay.Text = ScoreDisplayText;
                }
                else if (PlayerTurn == 3)
                {
                    PlayerTurn = 4;
                    Player3TotalScore = Player3Score + Player3TotalScore;
                    Player3Score = 0;
                    DisplayAlert("Next Player", "Player " + PlayerTurn + " these ones are for you", "Alrighty Then");
                    ScoreDisplayText = " ";
                    ScoreDisplay.Text = ScoreDisplayText;
                }
                else if (PlayerTurn == 4)
                {
                    Player4TotalScore = Player4Score + Player4TotalScore;
                    Player4Score = 0;
                    ScoreDisplayText = " ";
                    ScoreDisplay.Text = ScoreDisplayText;
                    RoundFinish();
                }
                
        }
        
            
        }
    }
    //This checks to see who the highest scorer is every round and if there is no higher scorer it will go to a tiebreak
    //Tiebreaks are just extra rounds until there is a winner it may be unfair but its how this works keeps it interesting anyway
    //Sets Boolean Tiebreak to true so the game knows to keep going until there is a winner
    //The Players who are "Back in it" are the players that lost that round but are still in the game because i'm nice like that
    private void RoundFinish()
    {
        if (games[0].PlayerNum == 2)
        {
            if(Player1Score > Player2Score)
            {
                HighestScorer = 1;
                Tiebreak = false;
            }
            else if (Player2Score > Player1Score)
            {
                HighestScorer = 2;
                Tiebreak = false;
            }
            else if(Player1Score == Player2Score)
            {
                RoundCount--;
                DisplayAlert("Tiebreak", "Extra Round", "ok");
                Tiebreak = true;
            }
            else
            {
                HighestScorer = 1;
                DisplayAlert("Round Win Error", "Round win may not be applied correctly", "ok");
                Tiebreak = false;
            }
        }
        else if (games[0].PlayerNum == 3)
        {
            if (Player1Score > Player2Score && Player1Score > Player3Score)
            {              
                HighestScorer = 1;
                Tiebreak = false;                
            }
            else if (Player2Score > Player1Score && Player2Score > Player3Score)
            {            
                HighestScorer = 2;
                Tiebreak = false;
            }
            else if(Player3Score > Player1Score && Player3Score > Player2Score)
            {   
                HighestScorer = 3;
                Tiebreak = false;
            }
            else if(Player1Score == Player2Score && Player1Score > Player3Score)
            {
                RoundCount--;
                DisplayAlert("Tiebreak", "Extra Round " + playerNames[0].Player3Name + " is back in it", "ok");
                Tiebreak = true;
            }
            else if(Player1Score == Player3Score && Player1Score > Player2Score)
            {
                RoundCount--;
                DisplayAlert("Tiebreak", "Extra Round " + playerNames[0].Player2Name + " is back in it", "ok");
                Tiebreak = true;
            }
            else if (Player2Score == Player3Score && Player2Score > Player1Score)
            {
                RoundCount--;
                DisplayAlert("Tiebreak", "Extra Round " + playerNames[0].Player1Name + " is back in it", "ok");
                Tiebreak = true;
            }
            else
            {
                HighestScorer = 1;
                DisplayAlert("Round Win Error", "Round win may not be applied correctly", "ok");
                Tiebreak = false;
            }
           

        }
        else if (games[0].PlayerNum == 4)
        {
            if (Player1Score > Player2Score && Player1Score > Player3Score && Player1Score > Player4Score)
            {
                HighestScorer = 1;
                Tiebreak = false;
            }
            else if (Player2Score > Player1Score && Player2Score > Player3Score && Player2Score > Player4Score)
            {
                HighestScorer = 2;
                Tiebreak = false;
            }
            else if (Player3Score > Player1Score && Player3Score > Player2Score && Player3Score > Player4Score)
            {
                HighestScorer = 3;
                Tiebreak = false;
            }
            else if (Player4Score > Player1Score && Player4Score > Player2Score && Player4Score > Player3Score)
            {
                HighestScorer = 4;
                Tiebreak = false;
            }
            else if(Player1Score == Player2Score && Player1Score > Player3Score && Player1Score > Player4Score)
            {
                RoundCount--;
                DisplayAlert("Tiebreak", "Extra Round " + playerNames[0].Player3Name + " and " + playerNames[0].Player4Name + " are back in it", "ok");
                Tiebreak = true;
            }
            else if(Player1Score == Player3Score && Player1Score > Player2Score && Player1Score > Player4Score)
            {
                RoundCount--;
                DisplayAlert("Tiebreak", "Extra Round " + playerNames[0].Player2Name + " and " + playerNames[0].Player4Name + " are back in it", "ok");
                Tiebreak = true;
            }
            else if(Player1Score == Player4Score && Player1Score > Player2Score && Player1Score > Player3Score)
            {
                RoundCount--;
                DisplayAlert("Tiebreak", "Extra Round " + playerNames[0].Player2Name + " and " + playerNames[0].Player3Name + " are back in it", "ok");
                Tiebreak = true;
            }
            else if(Player2Score == Player3Score && Player2Score > Player1Score && Player2Score > Player4Score)
            {
                RoundCount--;
                DisplayAlert("Tiebreak", "Extra Round " + playerNames[0].Player1Name + " and " + playerNames[0].Player4Name + " are back in it", "ok");
                Tiebreak = true;
            }
            else if(Player2Score == Player4Score && Player2Score > Player1Score && Player2Score > Player3Score)
            {
                RoundCount--;
                DisplayAlert("Tiebreak", "Extra Round " + playerNames[0].Player1Name + " and " + playerNames[0].Player3Name + " are back in it", "ok");
                Tiebreak = true;
            }
            else if(Player3Score == Player4Score && Player3Score > Player1Score && Player3Score > Player2Score)
            {
                RoundCount--;
                DisplayAlert("Tiebreak", "Extra Round " + playerNames[0].Player1Name + " and " + playerNames[0].Player2Name + " are back in it", "ok");
                Tiebreak = true;
            }
            else
            {
                HighestScorer = 1;
                DisplayAlert("Round Win Error", "Round win may not be applied correctly", "ok");
                Tiebreak = false;
            }
        }
        if (Tiebreak == false)
        {

            DisplayAlert("Round Finish", "The Player with the most points is " + HighestScorer, "Next Round");
            if (HighestScorer == 1)
            {
                Player1Rounds++;
            }
            else if (HighestScorer == 2)
            {
                Player2Rounds++;
            }
            else if (HighestScorer == 3)
            {
                Player3Rounds++;
            }
            else if (HighestScorer == 4)
            {
                Player4Rounds++;
            }
        }
        PlayerTurn = 1;
        Player1Score = 0;
        Player2Score = 0;
        Player3Score = 0;
        Player4Score = 0;
        RoundCount++;
        CheckGameOver();
    }

    private void CheckRoundPerGame()
    {
        RoundNumPref = Preferences.Default.Get("RoundsPerGame", "3");
        if (RoundNumPref == "3")
        {
            RoundNum = 3;
        }
        else if (RoundNumPref == "4")
        {
            RoundNum = 4;
        }
        else if (RoundNumPref == "5")
        {
            RoundNum = 5;
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

    private void CheckRoundLength()
    {
        QuestionsPerRoundPref = Preferences.Default.Get("QuestionsPerRound", "5");
            if(QuestionsPerRoundPref == "5")
        {
            QuestionsPerRound = 5;
        }
            else if(QuestionsPerRoundPref == "10")
        {
            QuestionsPerRound = 10;
        }
            else if(QuestionsPerRoundPref == "15")
        {
            QuestionsPerRound = 15;
        }
    }
    //Just handles the buttons. When a button is clicked it checks to see if its right and adds to your score 
    //if so then changes the question and button text according to how many have been asked.
    //Also checks to see if the quiz is over by checking the questionNum to the count of the List of Questions
    
    private void ButtonA_Clicked(object sender, EventArgs e)
    {
        if (ButtonA.Text == questionInput.results[QuestionNum].correct_answer)
        {
            if(PlayerTurn == 1)
            {
                Player1Score++;
                ScoreDisplayText = playerNames[0].Player1Name + "'s " + Player1Score;
                ScoreDisplay.Text = ScoreDisplayText;
            }
            else if(PlayerTurn == 2)
            {
                Player2Score++;
                ScoreDisplayText = playerNames[0].Player2Name + "'s " + Player2Score;
                ScoreDisplay.Text = ScoreDisplayText;
            }
            else if(PlayerTurn == 3)
            {
                Player3Score++;
                ScoreDisplayText = playerNames[0].Player3Name + "'s " + Player3Score;
                ScoreDisplay.Text = ScoreDisplayText;
            }
            else if(PlayerTurn == 4)
            {
                Player4Score++;
                ScoreDisplayText = playerNames[0].Player4Name + "'s " + Player4Score;
                ScoreDisplay.Text = ScoreDisplayText;
            }
            
            QuestionNum++;
            CheckGameOver();
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
            CheckRounds();
            if (QuestionNum == questionInput.results.Count - 1)
            {
                GetQuestions();
                AnswerAssign();
            }

        }
        else
        {
            QuestionNum++;
            CheckGameOver();
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
            CheckRounds();
            if (QuestionNum == questionInput.results.Count - 1)
            {
                GetQuestions();
                AnswerAssign();
            }

        }
    }

    private void ButtonB_Clicked(object sender, EventArgs e)
    {
        if (ButtonB.Text == questionInput.results[QuestionNum].correct_answer)
        {
            if (PlayerTurn == 1)
            {
                Player1Score++;
                ScoreDisplayText = playerNames[0].Player1Name + "'s " + Player1Score;
                ScoreDisplay.Text = ScoreDisplayText;
            }
            else if (PlayerTurn == 2)
            {
                Player2Score++;
                ScoreDisplayText = playerNames[0].Player2Name + "'s " + Player2Score;
                ScoreDisplay.Text = ScoreDisplayText;
            }
            else if (PlayerTurn == 3)
            {
                Player3Score++;
                ScoreDisplayText = playerNames[0].Player3Name + "'s " + Player3Score;
                ScoreDisplay.Text = ScoreDisplayText;
            }
            else if (PlayerTurn == 4)
            {
                Player4Score++;
                ScoreDisplayText = playerNames[0].Player4Name + "'s " + Player4Score;
                ScoreDisplay.Text = ScoreDisplayText;
            }
            QuestionNum++;
            CheckGameOver();
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
            CheckRounds();
            if (QuestionNum == questionInput.results.Count - 1)
            {
                GetQuestions();
                AnswerAssign();
            }
        }
        else
        {
            QuestionNum++;
            CheckGameOver();
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
            CheckRounds();
            if (QuestionNum == questionInput.results.Count - 1)
            {
                GetQuestions();
                AnswerAssign();
            }
        }
    }

    private void ButtonC_Clicked(object sender, EventArgs e)
    {
        if (ButtonC.Text == questionInput.results[QuestionNum].correct_answer)
        {
            if (PlayerTurn == 1)
            {
                Player1Score++;
                ScoreDisplayText = playerNames[0].Player1Name + "'s " + Player1Score;
                ScoreDisplay.Text = ScoreDisplayText;
            }
            else if (PlayerTurn == 2)
            {
                Player2Score++;
                ScoreDisplayText = playerNames[0].Player2Name + "'s " + Player2Score;
                ScoreDisplay.Text = ScoreDisplayText;
            }
            else if (PlayerTurn == 3)
            {
                Player3Score++;
                ScoreDisplayText = playerNames[0].Player3Name + "'s " + Player3Score;
                ScoreDisplay.Text = ScoreDisplayText;
            }
            else if (PlayerTurn == 4)
            {
                Player4Score++;
                ScoreDisplayText = playerNames[0].Player4Name + "'s " + Player4Score;
                ScoreDisplay.Text = ScoreDisplayText;
            }
            QuestionNum++;
            CheckGameOver();
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
            CheckRounds();
            if (QuestionNum == questionInput.results.Count - 1)
            {
                GetQuestions();
                AnswerAssign();
            }
        }
        else
        {
            QuestionNum++;
            CheckGameOver();
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
            CheckRounds();
            if (QuestionNum == questionInput.results.Count - 1)
            {
                GetQuestions();
                AnswerAssign();
            }
        }
    }

    private void ButtonD_Clicked(object sender, EventArgs e)
    {
        if (ButtonD.Text == questionInput.results[QuestionNum].correct_answer)
        {
            if (PlayerTurn == 1)
            {
                Player1Score++;
                ScoreDisplayText = playerNames[0].Player1Name + "'s " + Player1Score;
                ScoreDisplay.Text = ScoreDisplayText;
            }
            else if (PlayerTurn == 2)
            {
                Player2Score++;
                ScoreDisplayText = playerNames[0].Player2Name + "'s " + Player2Score;
                ScoreDisplay.Text = ScoreDisplayText;
            }
            else if (PlayerTurn == 3)
            {
                Player3Score++;
                ScoreDisplayText = playerNames[0].Player3Name + "'s " + Player3Score;
                ScoreDisplay.Text = ScoreDisplayText;
            }
            else if (PlayerTurn == 4)
            {
                Player4Score++;
                ScoreDisplayText = playerNames[0].Player4Name + "'s " + Player4Score;
                ScoreDisplay.Text = ScoreDisplayText;
            }
            QuestionNum++;
            CheckGameOver();
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
            CheckRounds();
            if (QuestionNum == questionInput.results.Count - 1)
            {
                GetQuestions();
                AnswerAssign();
            }

        }
        else
        {
            QuestionNum++;
            CheckGameOver();
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
            CheckRounds();
            if (QuestionNum == questionInput.results.Count - 1)
            {
                GetQuestions();
                AnswerAssign();
            }
        }
    }

    //Gives values to the game class so they can be saved and sends you to the score screen
    //Gives Rounds and Scores to the game class so they can be used in the score screen
    private void CheckGameOver()
    {//If the round count is equal to the round number then the game is over and the scores are saved
        if (RoundCount > RoundNum)
        {
            GameOver = true;
            Player1Rounds = games[0].Player1Rounds;
            Player2Rounds = games[0].Player2Rounds;
            Player3Rounds = games[0].Player3Rounds;
            Player4Rounds = games[0].Player4Rounds;
            Player1TotalScore = games[0].Player1TotalScore;
            Player2TotalScore = games[0].Player2TotalScore;
            Player3TotalScore = games[0].Player3TotalScore;
            Player4TotalScore = games[0].Player4TotalScore;
            jsonString = JsonSerializer.Serialize(games);

            using (StreamWriter writer = new StreamWriter(MPScoresFile))
            {
                writer.Write(jsonString);
            }
            Navigation.PushAsync(new ScoreScreen());
        }
    }
}