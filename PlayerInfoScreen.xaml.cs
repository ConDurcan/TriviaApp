namespace TriviaGameProject;

public partial class PlayerInfoScreen : ContentPage
{
    public string Player1Name;
    public string Player2Name;
    public string Player3Name;
    public string Player4Name;
    string FontStyle;
    string FontSizePref;
    private ViewModel viewmodel;
    
   
	public PlayerInfoScreen()
	{
		InitializeComponent();
        viewmodel = new ViewModel();
        BindingContext = viewmodel;
        CheckFontStyle();
        CheckFontSize();
	}

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
    }

    private void ThreePlayerBtn_Clicked(object sender, EventArgs e)
    {
        PlayerNameEntry1.IsVisible = true;
        PlayerNameEntry2.IsVisible = true;
        PlayerNameEntry3.IsVisible =true;
        PlayerNameEntry4.IsVisible = false;
        PlayerNameEntry4.Text = string.Empty;
        BeginBtn.IsVisible = true;
    }

    private void FourPlayerBtn_Clicked(object sender, EventArgs e)
    {
        PlayerNameEntry1.IsVisible = true;
        PlayerNameEntry2.IsVisible = true;
        PlayerNameEntry3.IsVisible = true;
        PlayerNameEntry4.IsVisible = true;
        BeginBtn.IsVisible = true;
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

    private void BeginBtn_Clicked(object sender, EventArgs e)
    {

    }
}