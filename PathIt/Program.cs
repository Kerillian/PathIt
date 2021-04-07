using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PathIt
{
    public class Program
    {
        private const string PATH = "PATH";

        private static int Main(string[] args)
        {
            string path = Path.GetFullPath(args.Length > 0 ? args[0] : Directory.GetCurrentDirectory());

            if (Directory.Exists(path))
            {
                List<string> pathsInPATH = Environment.GetEnvironmentVariable(PATH, EnvironmentVariableTarget.User)?.Split(';').ToList();

                if (pathsInPATH == null)
                {
                    return 1;
                }
                
                if (!pathsInPATH.Contains(path))
                {
                    pathsInPATH.Add(path);
                    
                    Environment.SetEnvironmentVariable(PATH, string.Join(";", pathsInPATH), EnvironmentVariableTarget.User);
                    Console.WriteLine($"Added \"{path}\" to PATH.");
                }
                else
                {
                    pathsInPATH.Remove(path);
                    
                    Environment.SetEnvironmentVariable(PATH, string.Join(";", pathsInPATH), EnvironmentVariableTarget.User);
                    Console.WriteLine($"Removed \"{path}\" from PATH.");
                }
            }
            else
            {
                Console.Error.WriteLine("[Error] Input path is not a directory.");
                return 1;
            }

            return 0;
        }
    }
}