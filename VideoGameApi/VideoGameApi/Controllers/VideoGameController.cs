using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController : ControllerBase
    {
        static private List<VideoGame> videoGames = new List<VideoGame>
        {
            new VideoGame
            {
                Id = 1,
                Title = "Spider-Man 2",
                Platform = "PS5",
                Developer = "Insomnic Games",
                Publisher = "Sony Ineractive Entertainment"
            },
            new VideoGame
            {
                Id = 2,
                Title = "The Legend of Zelda: Breath of the Wild",
                Platform = "Nintendo Switch",
                Developer = "Nintendo EPD",
                Publisher = "Nintendo"
            },
            new VideoGame
            {
                Id = 3,
                Title = "Cyberpunk 2077",
                Platform = "PC",
                Developer = "CD Projekt Red",
                Publisher = "CD Projekt"
            }
        };
        [HttpGet]
        public ActionResult<List<VideoGame>> GetVideoGames() //gets list of video games
        {
            return Ok(videoGames);
        }

        [HttpGet]
        [Route("{id}")]
        //or [Http("{id}")]
        public ActionResult<VideoGame> GetVideoGameById(int id) //this time we are getting single video game
        {
            var game = videoGames.FirstOrDefault(g => g.Id == id); //*lambda expression* gets an object as g where Id is equal to the provided id 
            if (game == null) //if there's no matching Id
                return NotFound(); // returns status code 404 not found

            return Ok(game); // return the game with 200 OK
        }

        [HttpPost]

        public ActionResult<VideoGame> AddVideoGame(VideoGame newGame)
        {
            if (newGame is null)
                return BadRequest(); //returns status code 400 bad request

            newGame.Id = videoGames.Max(g => g.Id) + 1; //finds the max Id and adds 1 to it
            videoGames.Add(newGame); //adds the new game to the list
            return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame); //returns status code 201 created
        }
    }
}
