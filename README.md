# ASP .Net Core Web API MySQL CRUD

## Prerequisites
* add **C#** and **Visual Studio** .gitignore files for the repository **at the beginning.** (important)
* Download and Install Dotnet 9 SDK [here](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
* Download and Install Visual Studio 2022 [here](https://visualstudio.microsoft.com/vs/)

  
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
* PUT method is used to update whole object.
* PATCH method is used to update only some properties of a object.
* update VideoGameController.cs
```C#
[HttpPut("{id}")]

public ActionResult<VideoGame> UpdateVideoGame(int id, VideoGame updatedGame)
{
    var game = videoGames.FirstOrDefault(g => g.Id == id); //finds the game with the provided Id
    if (game == null)
        return NotFound(); //returns status code 404 not found

    game.Title = updatedGame.Title; //updates the title
    game.Platform = updatedGame.Platform; //updates the platform
    game.Developer = updatedGame.Developer; //updates the developer
    game.Publisher = updatedGame.Publisher; //updates the publisher
    return Ok(game); //returns status code 200 OK
}
```

## DELETE a Video Game
* update VideoGameController.cs
  ```C#
  [HttpDelete("{id}")]

        public ActionResult DeleteVideoGame(int id) {
            var game = videoGames.FirstOrDefault(g => g.Id == id); //finds the game with the provided Id
            if (game == null)
                return NotFound(); //returns status code 404 not found
            videoGames.Remove(game); //removes the game from the list
            return NoContent(); //returns status code 204 no content
        }
  ```
  
## Implementing the VideoGameDbContext
* Create Folder named "Data" inside the VideoGameApi Project.
* Create VideoGameDbContext.cs inside it.
* add:
  ```C#
  public class VideoGameDbContext(DbContextOptions<VideoGameDbContext> options) : DbContext(options)
  ```
* Time stamp: 39:27 Install package 'MicrosoftEntityFrameworkCore'
  ```C#
  public DbSet<VideoGame> VideoGames => Set<VideoGame>(); //creates a table called VideoGames and maps it to the VideoGame class
  ```

## Adding the ConnectionString in the appsettings.json
  * code block for the appsettings.json
  ```Json
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\SQLExpress;Database=VideoGameDb;Trusted_Connection=True;TrustServerCertificate=true;"
  },
  ```

## Register the DbContext in the Program.cs
  * Update program.cs
  ```C#
  builder.Services.AddDbContext<VideoGameDbContext>(options =>
  {
    options.UseSqlServer(builder.Configuration.GetConnectionString("VideoGameDb")); //connects to the database and uses the connection string from appsettings.json using the key "VideoGameDb"
  });
  ```

## Installing the SQLServer Provider
  * Go to nugget package manager by righthand clicking the project
  * Find "Microsoft.EntityFrameworkCore.SqlServer" - Microsoft SQL Server database provider for Entity Framework Core.
  * Then Download and install it. (Check below Error 3 in [Errors](#errors) Section, if needed)

## Installing SQL Server Express
* Download And Install [SQLServer Management Studio 20.2.](https://learn.microsoft.com/en-us/ssms/download-sql-server-management-studio-ssms)
* Download and install [SQL Server 2022 Express.](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) 

## Code-First Migrations
* Install NuGet Package "MicrosoftEntityFrameworkCore.Tools"
* Go to Tools ->  NuGet Package Manager -> Package Manager Console
* Select this project as default project
* In CLI type ```Add-Migration <MigrationName>``` hit enter.
  * This will build our application
  * And create a migration file for us and open it.
  ### Migration file
  ```C#
  using Microsoft.EntityFrameworkCore.Migrations;

  #nullable disable

  namespace VideoGameApi.Migrations
  {
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideoGames",
                columns: table => new //creates a table called VideoGames with the following columns
                {
                    Id = table.Column<int>(type: "int", nullable: false) //creates a column called Id with the type int
                        .Annotation("SqlServer:Identity", "1, 1"), //sets the column to auto increment
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true), //creates a column called Title with the type string
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: true), //creates a column called Platform with the type string
                    Developer = table.Column<string>(type: "nvarchar(max)", nullable: true), //creates a column called Developer with the type string
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true) //creates a column called Publisher with the type string
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGames", x => x.Id); //sets the Id column as the primary key
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) //deletes the table VideoGames if it exists 
        {
            migrationBuilder.DropTable( 
                name: "VideoGames"); 
        }
    }
  }

  ```
* In CLI type ```Update-Database``` hit enter.
  * This will run our migration file and creates a DB for us.
  * If errors occur, refer error 4 and error 5 [Errors](#errors)
  * Now under the Databases you will see our "VideoGameDb" database
    * 







]
------------------------------------------------------------------------------------------------
# Errors
## 1. ```Unable to find package Microsoft.AspNetCore.OpenApi. No packages exist with this id in source(s): Microsoft Visual Studio Offline Packages``` <br/>
(.NET 9)
   <br/>
  * Make sure you have the official NuGet package reference in the NuGet.config. To do so follow these steps
  
      1. Open your Visual Studio
      2. Go to Tools -> Nuget Package Manager -> Package Manager Settings
      3. A setting menu will open, select Package Sources in the left-side navigation pane under Nuget Package Manager
      4. Look for a package with the name Nuget having this source address https://api.nuget.org/v3/index.json if not add it and click ok
      5. Clean your solution and build it again.
  * This should solve your problem !!
      
## 2. Error with cache files. (Github Desktop)
  * ```open("VideoGameApi/.vs/VideoGameApi/FileContentIndex/e93899ed-d975-4484-a243-cf47bf0b989d.vsidx"): Permission denied fatal: Unable to process path VideoGameApi/.vs/VideoGameApi/FileContentIndex/e93899ed-d975-4484-a243-cf47bf0b989d.vsidx.```
  * this error you are sending cache files to the repository. To stop that create a proper C# gitignore.

## 3. Error while installing "Microsoft.EntityFrameworkCore.SqlServer" (Error with package source)
   ![image](https://github.com/user-attachments/assets/8e91697b-7a87-483a-aef3-78b5ef12fc6e)
   ![image](https://github.com/user-attachments/assets/03dbf3bf-48fa-481f-a16f-b6c5b44a8100)

  * First Check the "Package source settings" inside the "NuGet Package Manager" and it must only have this configuraton. ![image](https://github.com/user-attachments/assets/69b8c771-5989-4986-887a-4c6ca2a4897e)
  * Secondly, Check the Dependencies form your Solution Explorer whether you have installed this Dependencies properly. if not install them using NuGet Package Manager.
  * ![image](https://github.com/user-attachments/assets/05aae4dc-1ad4-4013-a88b-2e19c6409304)
  * Now you are good to go !!!

## 4. Error while adding "Update-Databse" in Package Manager Console. 
  ![image](https://github.com/user-attachments/assets/582cd1d6-1d6f-4cec-ae44-6cf9ab9837df)
  ```ClientConnectionId:00000000-0000-0000-0000-000000000000 Error Number:-1983577849,State:0,Class:20 A network-related or instance-specific error occurred while establishing a connection to SQL Server. The server was not found or was not accessible. Verify that the instance name is correct and that SQL Server is configured to allow remote connections. (provider: SQL Network Interfaces, error: 50 - Local Database Runtime error occurred. The specified LocalDB instance does not exist.)```
  * Make sure the connection string in your appsettings.json matches with your database's connection string.
    * you can find your database connection string when you first create it in the SQL Server Express 2022.


## Error 5
  ![Screenshot 2025-03-25 201912](https://github.com/user-attachments/assets/b781eead-4fbb-4dce-8afe-a98f6cd32145)
  ```ClientConnectionId:afb2e33e-4e08-47a9-9f4f-b257c40d27a5 Error Number:-2146893019,State:0,Class:20 A connection was successfully established with the server, but then an error occurred during the login process. (provider: SSL Provider, error: 0 - The certificate chain was issued by an authority that is not trusted.)```

  * Makesure your connection string has proper server, Database added and most importantly "Trusted_Connection" and "TrustServerCertificate" are set to "true".
  * See below example Connection string.
  ```
  DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=VideoGameDb;Trusted_Connection=True;TrustServerCertificate=true;
  ```
  






## Reference
* Sample Games
  ```JSON
  [
  {
    "id": 1,
    "title": "Spider-Man 2",
    "platform": "PS5",
    "developer": "Insomnic Games",
    "publisher": "Sony Ineractive Entertainment"
  },
  {
    "id": 2,
    "title": "The Legend of Zelda: Breath of the Wild",
    "platform": "Nintendo Switch",
    "developer": "Nintendo EPD",
    "publisher": "Nintendo"
  },
  {
    "id": 3,
    "title": "Cyberpunk 2077: Updated",
    "platform": "PC",
    "developer": "CD Projekt Red",
    "publisher": "CD Projekt"
  }
  ]
  ```
* [Youtube By Patrick God](https://www.youtube.com/watch?v=AKjG2tjI07U) (dotnet 9)
* [ASRNE'II' Core web API documentation with Swagger/OpenAPl](https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-8.0)
* [Article on .gitignore on W3Schools](https://www.w3schools.com/git/git_ignore.asp?remote=github)
* [Artical on adding images to readme.md](https://cloudinary.com/guides/web-performance/4-ways-to-add-images-to-github-readme-1-bonus-method#:~:text=Open%20the%20folder%20containing%20the,you%20want%20it%20to%20appear.&text=Click%20Preview%20to%20see%20how,README%20with%20the%20new%20image.)
* [Artical on Adding Hyperlinks in Readme.md file.](https://learn.microsoft.com/en-us/contribute/content/how-to-write-links?utm_source=chatgpt.com#bookmark-links)
* [SQLServer Management Studio 20.2 Download](https://learn.microsoft.com/en-us/ssms/download-sql-server-management-studio-ssms)
