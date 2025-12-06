

using Tyuiu.Programmiste.Sprint6.Task3.V18.Lib;
internal class Program
{
    private static void Main(string[] args)
    {
        DataService ds = new DataService();

        // Utiliser un chemin dans le répertoire temporaire de l'utilisateur (toujours existant)
        string tempDir = Path.GetTempPath();
        string path = Path.Combine(tempDir, "matrix_task6.txt");

        Console.WriteLine("=== Programme de transformation de matrice ===");
        Console.WriteLine($"Fichier utilisé : {path}");

        try
        {
            // Vérifier si le fichier existe
            if (!File.Exists(path))
            {
                Console.WriteLine("\nCréation du fichier avec les données d'exemple...");

                // S'assurer que le répertoire existe (toujours le cas pour Temp)
                string directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    Console.WriteLine($"Répertoire créé : {directory}");
                }

                // Données de la matrice
                string[] matrixData = {
                        "-19 -19 1 18 7",
                        "5 3 -4 -6 -12",
                        "-15 6 2 2 -14",
                        "-9 -10 15 -5 -6",
                        "-13 -15 -9 7 1"
                    };

                File.WriteAllLines(path, matrixData);
                Console.WriteLine("Fichier créé avec succès !");
            }
            else
            {
                Console.WriteLine("\nFichier existant trouvé, utilisation des données...");
            }

            Console.WriteLine("\nLecture et transformation en cours...");

            // Appeler la méthode CollectTextFromFile
            string result = ds.CollectTextFromFile(path);

            Console.WriteLine("\nRésultat de la transformation :");
            Console.WriteLine("=================================");
            Console.WriteLine(result);
            Console.WriteLine("=================================");

            // Afficher les changements spécifiques
            Console.WriteLine("\nExplication des changements :");
            Console.WriteLine("- Ligne 4, colonne 2 : -10 → 0 (nombre pair)");
            Console.WriteLine("- Ligne 4, colonne 5 : -6 → 0 (nombre pair)");

            // Optionnel : Afficher le contenu original
            Console.WriteLine("\n--- Contenu original du fichier ---");
            string[] originalLines = File.ReadAllLines(path);
            foreach (string line in originalLines)
            {
                Console.WriteLine(line);
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine($"Erreur : Accès refusé au répertoire {Path.GetDirectoryName(path)}");
            Console.WriteLine("Veuillez exécuter le programme en tant qu'administrateur ou choisir un autre répertoire.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur : {ex.Message}");
            Console.WriteLine($"Type d'erreur : {ex.GetType().Name}");
        }

        Console.WriteLine("\nAppuyez sur une touche pour quitter...");
        Console.ReadKey();
    }
}
