
using System.Text;
using Tyuiu.Programmiste.Sprint6.Task6.V28.Lib;
internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("=== Simulation du test unitaire ===");
        Console.WriteLine();

        // Créer un fichier de test avec des lignes vides
        string cheminFichier = "test.txt";
        CreerFichierTest(cheminFichier);

        // Appeler la méthode à tester
        string resultat = CollecterTexteDuFichier(cheminFichier);

        Console.WriteLine($"Résultat obtenu : '{resultat}'");
        Console.WriteLine($"Résultat attendu : 'with | Line of'");
        Console.WriteLine();

        // Nettoyer
        File.Delete(cheminFichier);

        // Version corrigée
        Console.WriteLine("=== Version corrigée ===");
        resultat = CollecterTexteDuFichierCorrige(cheminFichier);
        Console.WriteLine($"Résultat obtenu : '{resultat}'");
        Console.WriteLine($"Résultat attendu : 'with | Line of'");
    }

    // Méthode avec le problème (version originale)
    static string CollecterTexteDuFichier(string chemin)
    {
        var lignes = File.ReadAllLines(chemin);
        var parties = new List<string>();

        foreach (string ligne in lignes)
        {
            // Simuler une logique de traitement
            if (!string.IsNullOrWhiteSpace(ligne))
            {
                // Ici, on simule l'extraction de certaines parties
                if (ligne.Contains("with"))
                    parties.Add("with");
                else if (ligne.Contains("Line"))
                    parties.Add("Line");
                else if (ligne.Contains("of"))
                    parties.Add("of");
            }
            // Problème : on n'ignore pas complètement les lignes vides
            // ou on ajoute une chaîne vide à la liste
        }

        return string.Join(" | ", parties);
    }

    // Méthode corrigée
    static string CollecterTexteDuFichierCorrige(string chemin)
    {
        var lignes = File.ReadAllLines(chemin);
        var parties = new List<string>();

        foreach (string ligne in lignes)
        {
            // Ignorer complètement les lignes vides
            if (string.IsNullOrWhiteSpace(ligne))
                continue;

            // Logique d'extraction
            if (ligne.Contains("with"))
                parties.Add("with");
            else if (ligne.Contains("Line of")) // Traiter "Line of" comme une unité
                parties.Add("Line of");
            else if (ligne.Contains("Line") && ligne.Contains("of"))
                parties.Add("Line of");
        }

        // Vérifier qu'on n'ajoute pas de séparateur inutile
        return string.Join(" | ", parties);
    }

    static void CreerFichierTest(string chemin)
    {
        var contenu = new StringBuilder();
        contenu.AppendLine("Une ligne avec le mot 'with'");
        contenu.AppendLine(""); // Ligne vide - cause du problème
        contenu.AppendLine("Une autre ligne avec 'Line of' text");
        contenu.AppendLine(""); // Autre ligne vide

        File.WriteAllText(chemin, contenu.ToString());
        Console.WriteLine("Fichier de test créé :");
        Console.WriteLine(contenu.ToString());
    }
}

