using var input = new StreamReader(Console.OpenStandardInput());
using var output = new StreamWriter(Console.OpenStandardOutput());

var storage = new Dictionary<int, Dictionary<int, HashSet<string>>>();

var n = Convert.ToInt32(input.ReadLine());
for (int i = 0; i < n; i++)
{
    var login = input.ReadLine();
    var length = login.Length;
    var volume = GetVolume(login);

    if (!storage.ContainsKey(length))
        storage.Add(length, new Dictionary<int, HashSet<string>>());

    if (!storage[length].ContainsKey(volume))
        storage[length].Add(volume, new HashSet<string>());

    storage[length][volume].Add(login);
}

var m = Convert.ToInt32(input.ReadLine());
for (int i = 0;i < m; i++)
{
    var login = input.ReadLine();
    var length = login.Length;
    var volume = GetVolume(login);

    storage.TryGetValue(length, out var possible);
    if (possible == null)
    {
        output.WriteLine(0);
        continue;
    }

    possible.TryGetValue(volume, out var similar);
    if (similar == null)
    {
        output.WriteLine(0);
        continue;
    }

    if (similar.Contains(login) || similar.Any(x => CheckSwapTwoCharacters(x, login)))
    {
        output.WriteLine(1);
        continue;
    }

    output.WriteLine(0);
}


static int GetVolume(string source) => source.Sum(Convert.ToInt32);

static bool CheckSwapTwoCharacters(string firstString, string secondString)
{
    var differentChars = new List<(char, char, int)>();

    for (int i = 0; i < firstString.Length; i++)
    {
        if (differentChars.Count > 2)
            break;

        if (firstString[i] != secondString[i])
            differentChars.Add((firstString[i], secondString[i], i));
    }

    return differentChars.Count == 2
      && differentChars.First().Item1 == differentChars.Last().Item2
      && differentChars.First().Item2 == differentChars.Last().Item1
      && differentChars.Last().Item3 - differentChars.First().Item3 == 1;
}