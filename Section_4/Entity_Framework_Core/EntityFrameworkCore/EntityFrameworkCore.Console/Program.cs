
using EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;

using var context = new FootballLeagueDbContext();

// Select all teams
// GetAllTeams();

// Select one team
// GetOneTeam();

// Seelct all records with a condition
// await GetFilteredTeams();

async Task GetFilteredTeams()
{
    // SQL
    // SELECT * FROM Teams WHERE Name = 'Tivoli Gardens FC'

    // LINQ
    var teamsFiltered = await context.Teams
    .Where(team => team.Name == "Tivoli Gardens FC")
    .ToListAsync();

    foreach (var team in teamsFiltered)
    {
        Console.Write(team.Name);
    }

    // Partial match
    // SQL: SELECT * FROM Teams WHERE Name LIKE '%FC%'

    //var partialMatches = await context.Teams.Where(team => EF.Functions.Like(team.Name, $"%{FC}%")).ToListAsync();
    var partialMatches = await context.Teams.Where(team => team.Name.Contains("FC")).ToListAsync();
}

async void GetOneTeam()
{
    // SQL
    // SELECT * FROM Teams WHERE Id = 1

    // LINQ
    // Select a single record - First in the list that meets a condition
    var team1 = await context.Teams.FirstOrDefaultAsync(team => team.Id == 1);

    // Select a single record - Single record
    var team2 = await context.Teams.SingleOrDefaultAsync(team => team.Id == 1);

    // Select based on id
    var team3 = await context.Teams.FindAsync(2);
}

void GetAllTeams()
{
    // SQL
    // SELECT * FROM Teams

    // LINQ
    var teams = context.Teams.ToList();

    foreach (var team in teams)
    {
        Console.WriteLine($"Team Id: {team.Id}, Name: {team.Name}, CreatedAt: {team.CreatedAt}");
    }
}   


