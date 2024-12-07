namespace TriviaGameProject
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
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
