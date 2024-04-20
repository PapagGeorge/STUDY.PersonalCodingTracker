namespace Application.Interfaces
{
    public interface IApplication
    {
        void CreateBet(int userId, int matchId, string bettingMarket, decimal stake);
    }
}
