namespace GameShop.Models
{
    public class EFGameRepository : IGameRepository

    {

        public IEnumerable<Game> Game => context.Game;


        private ApplicationDbContext context;


        public EFGameRepository(ApplicationDbContext ctx)

        {

            context = ctx;

        }
    }
}
