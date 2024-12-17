using TriviaGameProject.Resources.Themes;

namespace TriviaGameProject;

public partial class Options : ContentPage
{
    public string ThemeName;
    public string FontStyle;
    string FontSizePref;

	public Options()
	{
		InitializeComponent();
        CheckOptions();
	}

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
    

   
}