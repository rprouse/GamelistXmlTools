using System.Diagnostics;
using System.Xml.Serialization;
using GameListUtils;

// Edit these
const string ROMS   = @"E:\Roms";
string[] SEARCH = new [] {
    "Japan", "Asia", "bootleg", "French", "German", "Korea", "Mahjong", 
    "China", "Hong Kong", "JA-", "Prototype", "ver JAA", "ver JAB", "ver JAC", 
    "Spanish", "Old ver", "Brazil", "Australia",
    "notgame"
};
string[] systems = Directory.GetDirectories(ROMS);

foreach (string system in systems)
{
    // Read the gamelist.xml
    var filename = Path.Combine(system, "gamelist.xml");
    if (!File.Exists(filename)) continue;

    var serializer = new XmlSerializer(typeof(GameList));
    GameList? gamelist = null;
    using (var reader = new StreamReader(filename))
    {
        gamelist = serializer.Deserialize(reader) as GameList;
    }

    if (gamelist is null) throw new ArgumentNullException(nameof(GameList));

    // Find the games to delete
    var toDelete = gamelist
        .Games
        .Where(g =>
            SEARCH.Any(s => g.Name.Contains(s, StringComparison.InvariantCultureIgnoreCase) ||
                            g.Path.Contains(s, StringComparison.InvariantCultureIgnoreCase)) ||
            (g.RatingSpecified && g.Rating < 0.5m))
        .ToList();

    // Backup gamelist.xml
    var backup = Path.Combine(system, "gamelist.bak.xml");
    File.Move(filename, backup, true);

    // Delete the games from gamelist.xml
    var deleteIds = toDelete.Select(g => g.Id).ToList();
    gamelist.Games = gamelist.Games.Where(g => !deleteIds.Contains(g.Id)).ToArray();

    // Delete the games from disk
    foreach (var game in toDelete)
    {
        Console.WriteLine($"Deleting {game.Name}");
        string rom = Path.GetFullPath(Path.Combine(system, game.Path));
        if (File.Exists(rom))
            File.Delete(rom);

        string img = Path.GetFullPath(Path.Combine(system, game.Image ?? ""));
        if (File.Exists(img))
            File.Delete(img);
    }

    // Serialize out the gamelist.xml
    using (var writer = new StreamWriter(filename))
    {
        serializer.Serialize(writer, gamelist);
    }

    //var process = new Process
    //{
    //    StartInfo = new ProcessStartInfo
    //    {
    //        FileName = Path.Combine(ROMS, "xml.exe"),
    //        WorkingDirectory = system,
    //        Arguments = "edit -d \"//desc\" -d \"//rating\" -d \"//genre\" -d \"//players\" -d \"//releasedate\" -d \"//developer\" -d \"//publisher\" -d \"//hash\" -d \"//thumbnail\" -d \"//genreid\" --subnode \"gameList/game[not(image)]\" -t elem -n image -v \"no-img.png\" gamelist.xml > miyoogamelist.xml",
    //        UseShellExecute = true,
    //        RedirectStandardOutput = true,
    //        CreateNoWindow = true,
    //    }
    //}; 
    //process.OutputDataReceived += new DataReceivedEventHandler
    //(
    //    delegate (object sender, DataReceivedEventArgs e)
    //    {
    //        using (StreamReader output = process.StandardOutput)
    //        {
    //            Console.WriteLine(output.ReadToEnd());
    //        }
    //    }
    //);
    //process.Start();
    //process.WaitForExit();
}