using System.Text.Json;

namespace TriviaGameProject;

public partial class ScoreScreen : ContentPage
{
    string SPScoresFile = @"C:\Users\conor\Documents\Json Files\SPScores.json";
    string jsonString;
    int basescore = 0;
    List<Game> scorefile = new List<Game>();
    List<int> scores = new List<int>();
    public ScoreScreen()
	{
		InitializeComponent();
        //GetScores();
	}

    private void HomeButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }
    //Commented out as at the moment as it just crashes the app due to a deserialization issue
    /*private void GetScores()
	{
        try
        {
            using (StreamReader reader = new StreamReader(SPScoresFile))
            {
                jsonString = reader.ReadToEnd();
                scorefile = JsonSerializer.Deserialize<List<Game>>(jsonString);

            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Player name deserializing issue", "There has been an issue accessing the PlayerNames file", "ok");
        }
        
        scores.OrderByDescending(x => x).Take(5).ToList();
        FirstScore.Text = scores[0].ToString();
        SecondScore.Text = scores[1].ToString();
        ThirdScore.Text = scores[2].ToString();
        FourthScore.Text = scores[3].ToString();
        FifthScore.Text = scores[4].ToString();
        
    }*/

}