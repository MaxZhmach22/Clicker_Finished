namespace Clicker
{
    internal class UiFactories
    {
        public MainMenuView.Factory MainMenuFactory { get; private set; }
        public UiFactories(MainMenuView.Factory mainMenuFactory)
        {
            MainMenuFactory = mainMenuFactory;
        }
    }
}