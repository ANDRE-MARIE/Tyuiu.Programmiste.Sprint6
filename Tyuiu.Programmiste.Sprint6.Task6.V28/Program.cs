
using Tyuiu.Programmiste.Sprint6.Task6.V28.Lib;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== Avant-Dernier Mot Extracteur ===");
        Console.WriteLine("Extrait l'avant-dernier mot de chaque ligne d'un fichier");
        Console.WriteLine("======================================\n");

        try
        {
            string inputPath = "InPutFileTask6V28.txt";

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Fichier non trouvé: {inputPath}");
                Console.WriteLine("Création d'un fichier exemple...");
                CreateSampleFile(inputPath);
            }

            Console.WriteLine("Contenu du fichier:");
            Console.WriteLine(new string('-', 60));
            string[] fileLines = File.ReadAllLines(inputPath);
            for (int i = 0; i < fileLines.Length; i++)
            {
                Console.WriteLine($"[{i + 1,3}] {fileLines[i]}");
            }
            Console.WriteLine(new string('-', 60));

            DataService dataService = new DataService();

            Console.WriteLine("\nTraitement du fichier...");
            string result = dataService.CollectTextFromFile(inputPath);

            DisplayResults(result);

            SaveResults(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErreur: {ex.Message}");
        }

        Console.WriteLine("\nAppuyez sur une touche pour quitter...");
        Console.ReadKey();
    }

    static void CreateSampleFile(string filePath)
    {
        string[] sampleData = {
                "Le renard brun rapide saute",
                "Bonjour le monde de C#",
                "Ceci est une ligne de test",
                "Un deux trois quatre cinq",
                "",
                "   ",
                "Dernière ligne avec mot"
            };

        File.WriteAllLines(filePath, sampleData);
        Console.WriteLine($"Fichier exemple créé: {Path.GetFullPath(filePath)}");
    }

    static void DisplayResults(string result)
    {
        Console.WriteLine("\nMots Avant-Derniers Extraits:");
        Console.WriteLine(new string('-', 40));

        if (string.IsNullOrEmpty(result))
        {
            Console.WriteLine("Aucun mot avant-dernier trouvé.");
        }
        else
        {
            string[] words = result.Split(
                new[] { "\r\n", "\r", "\n" },
                StringSplitOptions.RemoveEmptyEntries
            );

            Console.WriteLine($"Nombre de mots extraits: {words.Length}");
            Console.WriteLine();

            for (int i = 0; i < words.Length; i++)
            {
                Console.WriteLine($"[{i + 1,3}] {words[i]}");
            }
        }

        Console.WriteLine(new string('-', 40));
    }

    static void SaveResults(string result)
    {
        try
        {
            string outputPath = "OutPutFileTask6V28.txt";
            File.WriteAllText(outputPath, result);
            Console.WriteLine($"\nRésultats sauvegardés dans: {Path.GetFullPath(outputPath)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nErreur de sauvegarde: {ex.Message}");
        }
    }
}

