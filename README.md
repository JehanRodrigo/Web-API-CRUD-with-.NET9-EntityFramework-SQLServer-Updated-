# ASP .Net Core Web API MySQL CRUD

## Prerequisites
* Download and Install Dotnet 9 SDK [here](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
* Download and Install Visual Studio 2022 [here](https://visualstudio.microsoft.com/vs/)
* add C# .gitignore for the repository.
  
## Creating the Project
* Create a new project using "ASP.NET Core Web API" template on Visual Studio
  * Tick: Conﬁgure for HTTPS
  * Tick: Enable OpenAPl support
  * Tick: Use controllers

## Project Overview
  ### VideoGameApi.http
  * We can test our endpoints using this .http file
    
  ### WeatherForecast.cs
  * Default example class

## About OpenAPI
* https://localhost:<Port>/openapi/v1.json
* above path is to see our openAPI Configurations.

## Add Scalar for Manual API Tests
* Right Click the Solution from Solution Explorer.
* Install Scalar using 'manage nugget package manager'
* add this to program.cs to map Scalar Endpoints.
  ```C#
  app.MapScalarApiReference();
  ```
* Now this will our documentation.: ```https://localhost:<Port>/scalar/v1```

## Add the VideoGame Model
* created VideoGame.cs

## Add the VideoGame Controller
* Controllers -> add -> Controller -> Common -> API -> API Controller-Empty -> Add
* Create VideoGameController.cs
* Added 3 Video games inside the controller

## Implement your first GET Call
* Get call created inside the "VideoGameController.cs"
```C#
[HttpGet]
    public ActionResult<List<VideoGame>> GetVideoGames()
    {
        return Ok(videoGames);
    }
}
```
* return type set to OK to get 200 OK response

## Get a Single Video Game
* update VideoGameController.cs
```C#
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
```

## Create a Video Game with POST
* Post is the method that we use when we need to add sth.
* update VideoGameController.cs
```C#
[HttpPost]

public ActionResult<VideoGame> AddVideoGame(VideoGame newGame)
{
    if (newGame is null)
        return BadRequest(); //returns status code 400 bad request

    newGame.Id = videoGames.Max(g => g.Id) + 1; //finds the max Id and adds 1 to it
    videoGames.Add(newGame); //adds the new game to the list
    return CreatedAtAction(nameof(GetVideoGameById), new { id = newGame.Id }, newGame); //returns status code 201 created
}
```

## UPDATE a Video Game with PUT
* PUT method is used to update complete object
* 

  ----------------------------------------------------------------
Old Section:
[
* Download MySQL Community Server 8 from [here](https://dev.mysql.com/downloads/mysql/)
* Download MySQL Workbench 8 CE from [here](https://dev.mysql.com/downloads/workbench/)
* create database mytestdb

]
------------------------------------------------------------------------------------------------
## Errors
1. Unable to find package Microsoft.AspNetCore.OpenApi. No packages exist with this id in source(s): Microsoft Visual Studio Offline Packages<br/>
(.NET 9)
   <br/>
    * Make sure you have the official NuGet package reference in the NuGet.config. To do so follow these steps
  
      1. Open your Visual Studio
      2. Go to Tools -> Nuget Package Manager -> Package Manager Settings
      3. A setting menu will open, select Package Sources in the left-side navigation pane under Nuget Package Manager
      4. Look for a package with the name Nuget having this source address https://api.nuget.org/v3/index.json if not add it and click ok
      5. Clean your solution and build it again.
    * This should solve your problem !!
      
2. open("VideoGameApi/.vs/VideoGameApi/FileContentIndex/e93899ed-d975-4484-a243-cf47bf0b989d.vsidx"): Permission denied fatal: Unable to process path VideoGameApi/.vs/VideoGameApi/FileContentIndex/e93899ed-d975-4484-a243-cf47bf0b989d.vsidx.
    * this error you are sending cache files to the repository. To stop that create a proper C# gitignore.

## Reference
* [Youtube By Patrick God](https://www.youtube.com/watch?v=AKjG2tjI07U) (dotnet 9)
* [ASRNE'II' Core web API documentation with Swagger/OpenAPl](https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-8.0)
* Old
* https://www.youtube.com/watch?v=6YIRKBsRWVI&t (dotnet 8))
