
using Tyuiu.Programmiste.Sprint6.Task7.V23.Lib;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("***********************************************************");
        Console.WriteLine("* Application Console pour le Sprint 6 - Tâche 7 V.23     *");
        Console.WriteLine("***********************************************************");
        Console.WriteLine();

        DataService ds = new DataService();

        // Demander le chemin du fichier d'entrée
        Console.WriteLine("Veuillez entrer le chemin complet du fichier CSV d'entrée :");
        Console.WriteLine("(Exemple: C:\\Data\\InPutFileTask7V23.csv)");
        Console.Write("> ");
        string inputFilePath = Console.ReadLine();

        // Vérifier si le fichier existe
        if (!File.Exists(inputFilePath))
        {
            Console.WriteLine($"ERREUR : Le fichier '{inputFilePath}' n'existe pas.");
            Console.WriteLine("Appuyez sur une touche pour quitter.");
            Console.ReadKey();
            return;
        }

        try
        {
            Console.WriteLine("\nTraitement du fichier...");

            // Charger et modifier la matrice
            int[,] matrix = ds.GetMatrix(inputFilePath);

            // Afficher les dimensions
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);

            Console.WriteLine($"\nMatrice chargée : {rows} lignes x {columns} colonnes");
            Console.WriteLine("\nContenu de la matrice modifiée :");
            Console.WriteLine("(Les valeurs <2 dans la dernière colonne ont été remplacées par 2)");
            Console.WriteLine();

            // Afficher la matrice
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write($"{matrix[i, j],4}");
                    if (j < columns - 1) Console.Write(" | ");
                }
                Console.WriteLine();
            }

            // Demander où sauvegarder
            Console.WriteLine("\nVeuillez entrer le chemin pour sauvegarder le résultat :");
            Console.WriteLine("(Exemple: C:\\Data\\OutPutFileTask7.csv)");
            Console.Write("> ");
            string outputFilePath = Console.ReadLine();

            // Sauvegarder le résultat
            ds.SaveMatrixToFile(matrix, outputFilePath);

            Console.WriteLine($"\n✅ Résultat sauvegardé avec succès dans : {outputFilePath}");
            Console.WriteLine($"Taille du fichier : {new FileInfo(outputFilePath).Length} octets");

            // Proposer d'afficher le contenu du fichier
            Console.Write("\nVoulez-vous afficher le contenu du fichier sauvegardé ? (O/N) : ");
            string response = Console.ReadLine();

            if (response.ToUpper() == "O")
            {
                Console.WriteLine("\nContenu du fichier sauvegardé :");
                Console.WriteLine("--------------------------------");
                string[] outputLines = File.ReadAllLines(outputFilePath);
                foreach (string line in outputLines)
                {
                    Console.WriteLine(line);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n❌ ERREUR : {ex.Message}");
            Console.WriteLine($"Détails : {ex.InnerException?.Message}");
        }

        Console.WriteLine("\nAppuyez sur une touche pour quitter...");
        Console.ReadKey();
    }

    // Méthode utilitaire pour créer un fichier de test
    static void CreateTestFile(string path)
    {
        string testContent = "5;3;1\n2;4;0\n6;7;1\n8;9;3\n1;2;-1\n4;5;6";
        File.WriteAllText(path, testContent);
        Console.WriteLine($"Fichier de test créé : {path}");
    }
}
