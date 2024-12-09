namespace TriviaGameProject
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();
            Options options = new Options();
            options.CheckTheme();
        }

        
        private void NewGameBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void ContinueGameBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void OptionsBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Options());
        }
    }

}
