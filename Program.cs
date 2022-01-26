using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ObseravabilityEasyDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var overrideDlls = true;
                var pdbs = System.IO.Directory.GetFiles(args[0], "*.pdb");
                var destinationPaths = args.Skip(1);
                var dlls = new List<string>();
                foreach (var path in destinationPaths)
                {
                    dlls.AddRange(Directory.GetFiles(args[1], "*.dll", SearchOption.AllDirectories).ToList());
                }
                foreach (var filePath in pdbs)
                {
                    var test = Regex.Match(filePath, @".*\\([^\\]+$)").Groups;
                    string pdbName = Regex.Match(filePath, @".*\\([^\\]+$)").Groups[1].Value;
                    string filename = pdbName.Replace(".pdb", ".dll");
                    var destinationDir = dlls.Where(x => x.Contains(filename))
                        .Select(p => Regex.Match(p, @".*\\([^\\]+$)").Groups[0])
                        .ToList();
                    foreach (var dir in destinationDir)
                    {
                        var destincationDir = Path.GetDirectoryName(dir.ToString());
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
                    

                }

                Console.WriteLine("FINISHED");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
