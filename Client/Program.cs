using System.Net.Http.Json;
using Client;

Console.Write("Enter a player name: ");
string playerName = (Console.ReadLine())!;

using (var client = new HttpClient())
{
    while (true)
    {
        // Call the /match/{player} endpoint with the specified player name
        HttpResponseMessage response = await client.GetAsync($"http://localhost:5231/match/{playerName}");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Failed to match player {playerName}. Status code: {response.StatusCode}");
            break;
        }

        // Read the response content and deserialize it to a Match object
        GameMatch match = (await response.Content.ReadFromJsonAsync<GameMatch>())!;

        Console.WriteLine($"Match state for {playerName}: {match.State}");

        // If the match state is GameReady, we're done
        if (match.State == MatchState.GameReady)
        {
            Console.WriteLine($"Match found! {match.Player1} vs {match.Player2}");
            break;
        }

        // Wait for 1 second before polling again
        await Task.Delay(TimeSpan.FromSeconds(1));
    }
}