using Microsoft.EntityFrameworkCore;

namespace VideoGameApi.Data
{
    public class VideoGameDbContext(DbContextOptions<VideoGameDbContext> options) : DbContext(options)
    {
        public DbSet<VideoGame> VideoGames => Set<VideoGame>(); //creates a table called VideoGames and maps it to the VideoGame class

        protected override void OnModelCreating(ModelBuilder modelBuilder) //this method is called when the model is being created
        {
           base.OnModelCreating(modelBuilder); //calls the base class method
            modelBuilder.Entity<VideoGame>().HasData( //seeding the database with initial data
                new VideoGame
                {
                    Id = 1,
                    Title = "The Legend of Zelda: Breath of the Wild",
                    Platform = "Nintendo Switch",
                    Developer = "Nintendo EPD",
                    Publisher = "Nintendo"
                },
                new VideoGame
                {
                    Id = 2,
                    Title = "The Witcher 3: Wild Hunt",
                    Platform = "PC, PS4, Xbox One, Nintendo Switch",
                    Developer = "CD Projekt Red",
                    Publisher = "CD Projekt"
                },
                new VideoGame
                {
                    Id = 3,
                    Title = "Cyberpunk 2077: Updated",
                    Platform = "PC",
                    Developer = "Insomnic Games",
                    Publisher = "Sony Ineractive Entertainment"
                }
            );
        }
    }
    
   
}
