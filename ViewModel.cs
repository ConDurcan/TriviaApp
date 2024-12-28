using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace TriviaGameProject;

public class ViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;
    //This is for the appearing and disappearing PlayerNameEntry input bars on the PlayerInfoScreen.
    private bool _isVisible = false;
    public bool IsVisible
    {
        get => _isVisible;
        set
        {
            if (_isVisible != value)
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }
    }
    // DataBinding for changing Question Text on the GameScreens.
    private string currentQuestion;
    public string CurrentQuestion
    {
        get
        {
            return currentQuestion;
        }
        set
        {
            currentQuestion = value;
            OnPropertyChanged(nameof(CurrentQuestion));
        }
    }
    public ViewModel()
	{
		
	}
    public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}