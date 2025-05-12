using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameApi.Data;

namespace VideoGameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoGameController(VideoGameDbContext context) : ControllerBase 
    {
        private readonly VideoGameDbContext _context = context; //this is the database context

       

        [HttpGet]
        public async Task<ActionResult<List<VideoGame>>> GetVideoGames() //gets list of video games
        {
            return Ok(await _context.VideoGames.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        //or [Http("{id}")]
        public async Task<ActionResult<VideoGame>> GetVideoGameById(int id) //this time we are getting single video game
        {
            var game = await _context.VideoGames.FindAsync(id); //finds the game with the provided Id
            if (game == null) //if there's no matching Id
                return NotFound(); // returns status code 404 not found

            return Ok(game); // return the game with 200 OK
        }

        [HttpPost]

        public async Task<ActionResult<VideoGame>> AddVideoGame(VideoGame newGame)
        {
            if (newGame is null)
                return BadRequest(); //returns status code 400 bad request

            _context.VideoGames.Add(newGame); //adds the new game to the database
            await _context.SaveChangesAsync(); //saves the changes to the database

            return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame); //returns status code 201 created
        }

        //[HttpPut("{id}")]

        //public ActionResult<VideoGame> UpdateVideoGame(int id, VideoGame updatedGame)
        //{
        //    var game = videoGames.FirstOrDefault(g => g.Id == id); //finds the game with the provided Id
        //    if (game == null)
        //        return NotFound(); //returns status code 404 not found

        //    game.Title = updatedGame.Title; //updates the title
        //    game.Platform = updatedGame.Platform; //updates the platform
        //    game.Developer = updatedGame.Developer; //updates the developer
        //    game.Publisher = updatedGame.Publisher; //updates the publisher
        //    return Ok(game); //returns status code 200 OK
        //}

        //[HttpDelete("{id}")]

        //public ActionResult DeleteVideoGame(int id) {
        //    var game = videoGames.FirstOrDefault(g => g.Id == id); //finds the game with the provided Id
        //    if (game == null)
        //        return NotFound(); //returns status code 404 not found
        //    videoGames.Remove(game); //removes the game from the list
        //    return NoContent(); //returns status code 204 no content
        //}
    }
}
