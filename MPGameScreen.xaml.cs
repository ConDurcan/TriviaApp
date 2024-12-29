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
    int QuestionsPerRound;
    int PlayerNum;
    int RandomNum;
    int QuestionNum = 0;
    int Player1Score = 0;
    int Player2Score = 0;
    int Player3Score = 0;
    int Player4Score = 0;
    int HighestScorer;
    int Player1Rounds = 0;
    int Player2Rounds = 0;
    int Player3Rounds = 0;
    int Player4Rounds = 0;
    int PlayerTurn = 1;
    int RoundNum;
    bool GameOver = false;
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
                List<PlayerNames> playerNames = JsonSerializer.Deserialize<List<PlayerNames>>(jsonString);
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
                Game game = JsonSerializer.Deserialize<Game>(jsonString);
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Game state deserializing issue", "There has been an issue accessing the GameState file", "ok");
        }
    }

    //Http client grabs questions and loads first question
    private async Task GetQuestions()
    {
        var response = await client.GetAsync("https://opentdb.com/api.php?amount=50&type=multiple");
        if (response.IsSuccessStatusCode)
        {
            string contents = await response.Content.ReadAsStringAsync();
            questionInput = JsonSerializer.Deserialize<QuestionResponse>(contents);
            TheQuestion.Text = questionInput.results[0].question;
        }

    }
    //Function to randomize the correct answer
    private async Task AnswerAssign()
    {
        RandomNum = random.Next(1, 4 + 1);
        if (RandomNum == 1)
        {
            ButtonA.Text = questionInput.results[QuestionNum].correct_answer;
            ButtonB.Text = questionInput.results[QuestionNum].incorrect_answers[0];
            ButtonC.Text = questionInput.results[QuestionNum].incorrect_answers[1];
            ButtonD.Text = questionInput.results[QuestionNum].incorrect_answers[2];
        }
        else if (RandomNum == 2)
        {
            ButtonA.Text = questionInput.results[QuestionNum].incorrect_answers[0];
            ButtonB.Text = questionInput.results[QuestionNum].correct_answer;
            ButtonC.Text = questionInput.results[QuestionNum].incorrect_answers[1];
            ButtonD.Text = questionInput.results[QuestionNum].incorrect_answers[2];
        }
        else if (RandomNum == 3)
        {
            ButtonA.Text = questionInput.results[QuestionNum].incorrect_answers[0];
            ButtonB.Text = questionInput.results[QuestionNum].incorrect_answers[1];
            ButtonC.Text = questionInput.results[QuestionNum].correct_answer;
            ButtonD.Text = questionInput.results[QuestionNum].incorrect_answers[2];
        }
        else if (RandomNum == 4)
        {
            ButtonA.Text = questionInput.results[QuestionNum].incorrect_answers[0];
            ButtonB.Text = questionInput.results[QuestionNum].incorrect_answers[1];
            ButtonC.Text = questionInput.results[QuestionNum].incorrect_answers[2];
            ButtonD.Text = questionInput.results[QuestionNum].correct_answer;
        }
        else
        {
            DisplayAlert("RandomNum Error", "Your RandomNum dont work right on line 85", "Shit");
        }
    }

    private async void SetupGame()
    {
        GetQuestions();
        await Task.Delay(1000);
        AnswerAssign();
    }

    private void CheckRounds()
    {
        if (QuestionNum % QuestionsPerRound == 0)
        {
            DisplayAlert("Next Player", "Player " + PlayerTurn + "these ones are for you", "Alrighty Then");
        }
        if(PlayerTurn == 5)
        {
            DisplayAlert("Round Finish", "The Player with the most points is" + HighestScorer, "Next Round");
            if(HighestScorer == 1)
            {
                Player1Rounds++;
            }
            else if(HighestScorer == 2)
            {
                Player2Rounds++;
            }
            else if(HighestScorer == 3)
            {
                Player3Rounds++;
            }
            else if(HighestScorer == 4)
            {
                Player4Rounds++;
            }
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
            Player1Score++;
            QuestionNum++;
            CheckGameOver();
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
            ScoreDisplay.Text = playerNames[0].Player1Name + "'s score: " + Player1Score.ToString();

        }
        else
        {
            QuestionNum++;
            CheckGameOver();
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();


        }
    }

    private void ButtonB_Clicked(object sender, EventArgs e)
    {
        if (ButtonB.Text == questionInput.results[QuestionNum].correct_answer)
        {
            Player1Score++;
            QuestionNum++;
            CheckGameOver();
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
            ScoreDisplay.Text = playerNames[0].Player1Name + "'s score: " + Player1Score.ToString();

        }
        else
        {
            QuestionNum++;
            CheckGameOver();
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
        }
    }

    private void ButtonC_Clicked(object sender, EventArgs e)
    {
        if (ButtonC.Text == questionInput.results[QuestionNum].correct_answer)
        {
            Player1Score++;
            QuestionNum++;
            CheckGameOver();
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
            ScoreDisplay.Text = playerNames[0].Player1Name + "'s score: " + Player1Score.ToString(); ;
        }
        else
        {
            QuestionNum++;
            CheckGameOver();
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();

        }
    }

    private void ButtonD_Clicked(object sender, EventArgs e)
    {
        if (ButtonD.Text == questionInput.results[QuestionNum].correct_answer)
        {
            Player1Score++;
            QuestionNum++;
            CheckGameOver();
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
            ScoreDisplay.Text = playerNames[0].Player1Name + "'s score: " + Player1Score.ToString();

        }
        else
        {
            QuestionNum++;
            CheckGameOver();
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
        }
    }

    //Gives values to the game class so they can be saved and sends you to the score screen
    private void CheckGameOver()
    {
        if (QuestionNum == questionInput.results.Count - 1)
        {
            GameOver = true;
            //Player1Score = game.Player1Score;
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            jsonString = JsonSerializer.Serialize(games, options);

            using (StreamWriter writer = new StreamWriter(MPScoresFile))
            {
                writer.Write(jsonString);
            }
            Navigation.PushAsync(new ScoreScreen());
        }
    }
}