namespace Clicker
{
    internal sealed class PlayerProfile
    {
        public SubscriptionProperty<GameState> CurrentGameState;

        public PlayerProfile()
        {
            CurrentGameState = new SubscriptionProperty<GameState>(GameState.Start);

        }

    }
}
