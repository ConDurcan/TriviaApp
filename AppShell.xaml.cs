namespace TriviaGameProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("scorescreen", typeof(ScoreScreen));
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("playerinfoscreen", typeof(PlayerInfoScreen));
            Routing.RegisterRoute("options", typeof(Options));
            Routing.RegisterRoute("spgamescreen", typeof(SPGameScreen));
            Routing.RegisterRoute("mpgamescreen", typeof(MPGameScreen));
        }
    }
}
