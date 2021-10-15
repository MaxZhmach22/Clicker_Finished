namespace Clicker
{
    internal sealed class PlayerProfile
    {
        public SubscriptionProperty<GameStates> CurrentGameState;

        public PlayerProfile()
        {
            CurrentGameState = new SubscriptionProperty<GameStates>(GameStates.Start);

        }

    }
}
