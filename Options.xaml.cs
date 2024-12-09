using TriviaGameProject.Resources.Themes;

namespace TriviaGameProject;

public partial class Options : ContentPage
{
    public string ThemeName;
    public string FontStyle;

	public Options()
	{
		InitializeComponent();
        CheckTheme();
	}

    private void LightThemeBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("Theme_Choice", "LIGHT");
        Application.Current.Resources.MergedDictionaries.Clear();
        Application.Current.Resources.MergedDictionaries.Add(new LightTheme());

    }

    private void DarkThemeBtn_Clicked(object sender, EventArgs e)
    {
        Preferences.Default.Set("Theme_Choice", "DARK");
        Application.Current.Resources.MergedDictionaries.Clear();
        Application.Current.Resources.MergedDictionaries.Add(new DarkTheme());
    }

    private void StandardBtn_Clicked(object sender, EventArgs e)
    {
        
    }

    private void RetroBtn_Clicked(object sender, EventArgs e)
    {

    }

    private void FancyBtn_Clicked(object sender, EventArgs e)
    {

    }

   public void CheckTheme()
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
    }
}