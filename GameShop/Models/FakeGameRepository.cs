namespace GameShop.Models
{
    public class FakeGameRepository : IGameRepository
    {

   
            private IEnumerable<Game>? _game;

            public IEnumerable<Game> Game => _game ??= new List<Game>()

{

new Game { Title = "Hollow Knight : Silksong", ReleaseDate = new DateOnly(2025, 01, 01)},

new Game { Title = "Cyberpunk 2077", ReleaseDate = new DateOnly(2020, 01, 01) },

new Game { Title = "Heroes of Might and Magic 3", ReleaseDate = new DateOnly(1999, 01, 01) }

};

        }
    }

