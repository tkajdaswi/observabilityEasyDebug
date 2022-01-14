using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ObseravabilityEasyDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            var overrideDlls = true;
            var pdbs = System.IO.Directory.GetFiles(args[0], "*.pdb");
            var dlls = System.IO.Directory.GetFiles(args[1], "*.dll", SearchOption.AllDirectories);
            foreach (var filePath in pdbs)
            {
                var test = Regex.Match(filePath, @".*\\([^\\]+$)").Groups;
                string pdbName = Regex.Match(filePath, @".*\\([^\\]+$)").Groups[1].Value;
                string filename = pdbName.Replace(".pdb",".dll");
                var destinationDir = dlls.Where(x => x.Contains(filename)).Select(p => Regex.Match(p, @".*\\([^\\]+$)").Groups[0])
                    .FirstOrDefault();
                var destincationDir = Path.GetDirectoryName(destinationDir.ToString());
                var destPath = $"{destincationDir}\\{pdbName}";
                var destDllPath = $"{destincationDir}\\{filename}";
                File.Copy(filePath, destPath, true);
                if (overrideDlls)
                {
                    try
                    {
                        File.Copy(filePath.Replace(".pdb", ".dll"), destDllPath, true);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[ERROR] : {ex.Message}");
                    }
                }
                Console.WriteLine($"[OK] {filePath}>{destincationDir}");
            }

            Console.ReadLine();
        }
    }
}
