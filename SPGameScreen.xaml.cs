using System.Text.Json;

namespace TriviaGameProject;

public partial class SPGameScreen : ContentPage
{
    string PlayerNameFile = Path.Combine(FileSystem.Current.AppDataDirectory, "PlayerNames.json");
    string GameStateFile = Path.Combine(FileSystem.Current.AppDataDirectory, "GameState.json");
    string SPScoresFile = Path.Combine(FileSystem.Current.AppDataDirectory, "SPScores.json");
    string jsonString;
    string FontStyle;
    string FontSizePref;
    int RandomNum;
    int QuestionNum = 0;
    int Player1Score = 0;
    bool GameOver = false;
    QuestionResponse questionInput;
    Random random = new Random();
    List<Game> games;
    Game game = new Game();
    List<PlayerNames> playerNames;
    List<Question> question = new List<Question>();
    List<Scores> scores;    
    HttpClient client = new HttpClient();
    private ViewModel viewmodel;
    


    public SPGameScreen()
	{
		InitializeComponent();
        CheckFontSize();
        CheckFontStyle();
        games = new List<Game>();
        playerNames = new List<PlayerNames>();
        scores = new List<Scores>();
        getGameState();
        getPlayerNames();
        SetupGame();
        client = new HttpClient();
        viewmodel = new ViewModel();
        BindingContext = viewmodel;
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
        RandomNum = random.Next(1,4 + 1);
        if(RandomNum == 1)
        {
            ButtonA.Text = questionInput.results[QuestionNum].correct_answer;
            ButtonB.Text = questionInput.results[QuestionNum].incorrect_answers[0];
            ButtonC.Text = questionInput.results[QuestionNum].incorrect_answers[1];
            ButtonD.Text = questionInput.results[QuestionNum].incorrect_answers[2];
        }
        else if(RandomNum == 2)
        {
            ButtonA.Text = questionInput.results[QuestionNum].incorrect_answers[0];
            ButtonB.Text = questionInput.results[QuestionNum].correct_answer;
            ButtonC.Text = questionInput.results[QuestionNum].incorrect_answers[1];
            ButtonD.Text = questionInput.results[QuestionNum].incorrect_answers[2];
        }
        else if(RandomNum == 3)
        {
            ButtonA.Text = questionInput.results[QuestionNum].incorrect_answers[0];
            ButtonB.Text = questionInput.results[QuestionNum].incorrect_answers[1];
            ButtonC.Text = questionInput.results[QuestionNum].correct_answer;
            ButtonD.Text = questionInput.results[QuestionNum].incorrect_answers[2];
        }
        else if(RandomNum == 4)
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
    //Just handles the buttons. When a button is clicked it checks to see if its right and adds to your score 
    //if so then changes the question and button text according to how many have been asked.
    //Also checks to see if the quiz is over by checking the questionNum to the count of the List of Questions
    private void ButtonA_Clicked(object sender, EventArgs e)
    {
        if(ButtonA.Text == questionInput.results[QuestionNum].correct_answer)
        {
            Player1Score++;
            QuestionNum++;
            CheckGameOver();
            questionInput.results[QuestionNum].question = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].question);
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
            ScoreDisplay.Text = playerNames[0].Player1Name + "'s score: " + Player1Score.ToString();
            
        }
        else
        {
            QuestionNum++;
            CheckGameOver();
            questionInput.results[QuestionNum].question = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].question);
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
            questionInput.results[QuestionNum].question = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].question);
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
            ScoreDisplay.Text = playerNames[0].Player1Name + "'s score: " + Player1Score.ToString();

        }
        else
        {
            QuestionNum++;
            CheckGameOver();
            questionInput.results[QuestionNum].question = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].question);
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
            questionInput.results[QuestionNum].question = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].question);
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
            ScoreDisplay.Text = playerNames[0].Player1Name + "'s score: " + Player1Score.ToString();
        }
        else
        {
            QuestionNum++;
            CheckGameOver();
            questionInput.results[QuestionNum].question = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].question);
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
            questionInput.results[QuestionNum].question = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].question);
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
            ScoreDisplay.Text = playerNames[0].Player1Name + "'s score: " + Player1Score.ToString();

        }
        else
        {
            QuestionNum++;
            CheckGameOver();
            questionInput.results[QuestionNum].question = System.Web.HttpUtility.HtmlDecode(questionInput.results[QuestionNum].question);
            TheQuestion.Text = questionInput.results[QuestionNum].question;
            AnswerAssign();
        }
    }
    //Gives values to the game class so they can be saved and sends you to the score screen
    private async void CheckGameOver()
    {// -1 to avoid the out of bounds 
        if(QuestionNum == questionInput.results.Count - 1)
        {
            GameOver = true;
            games[0].Player1Score = Player1Score; 
  
            jsonString = JsonSerializer.Serialize(games);

            using (StreamWriter writer = new StreamWriter(GameStateFile))
            {
                writer.Write(jsonString);
            }

            Navigation.PushAsync(new ScoreScreen());
        }
    }
}