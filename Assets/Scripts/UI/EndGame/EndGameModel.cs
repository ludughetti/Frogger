namespace UI.EndGame
{
    public class EndGameModel
    {
        public string ResultMessage { get; set; }
        public bool PlayerWon { get; set; }

        public EndGameModel(string resultMessage, bool playerWon)
        {
            ResultMessage = resultMessage;
            PlayerWon = playerWon;
        }
    }
}
