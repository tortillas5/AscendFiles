using System;
using System.IO;

namespace RemonteFichiers
{
    internal class Program
    {
        public static string CheckExist(string newPath)
        {
            string returnPath = newPath;
            FileInfo file = new FileInfo(returnPath);
            int i = 1;

            while (file.Exists)
            {
                returnPath = Path.Combine(file.DirectoryName, Path.GetFileNameWithoutExtension(file.FullName) + " (" + i + ")" + Path.GetExtension(file.FullName));
                file = new FileInfo(returnPath);
                i++;
            }

            return returnPath;
        }

        private static void Main(string[] args)
        {
            try
            {
                string path = string.Empty;

                do
                {
                    Console.WriteLine("Veuillez rentrer le chemin d'où il faut remonter des fichiers.");
                    path = Console.ReadLine();
                } while (string.IsNullOrWhiteSpace(path));

                DirectoryInfo directoryInfo = new DirectoryInfo(path);

                foreach (var directory in directoryInfo.GetDirectories())
                {
                    foreach (var file in directory.GetFiles())
                    {
                        string newPath = Path.Combine(path, file.Name);

                        newPath = CheckExist(newPath);

                        file.MoveTo(newPath);
                    }
                }

                Console.WriteLine("Remontage terminé.");
                Console.WriteLine("Appuyez sur une touche pour fermer cette console.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}