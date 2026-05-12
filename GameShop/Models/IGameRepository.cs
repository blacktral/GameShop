namespace GameShop.Models
{
    public interface IGameRepository
    {
        IEnumerable<Game> Game { get; }
    }
}
