using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            string Raw = File.ReadAllText(GetSteamPath() + @"\steamapps\libraryfolders.vdf");
            int count = -1;
            bool result = false;
            string VRCPath = string.Empty;
            foreach (string a in Raw.Split(new string[] { "\"path\"" }, StringSplitOptions.None))
            {
                if (result) continue;
                if (count == -1)
                {

                }
                else
                {
                    Console.WriteLine($"Got Library {count} Path: {a.Split('"')[1]}\nChecking For VRC...");
                    foreach (string b in GetAppsPath(a.Split('"')[1]))
                    {
                        if (result) continue;
                        if (b.Contains("VRChat"))
                        {
                            Console.WriteLine(b);
                            VRCPath = b;
                            result = true;
                        }
                    }
                }
                count++;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Failed: "+e);
        }
        Console.ReadLine();
    }
    static string GetSteamPath()
    {
        object a = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam").GetValue("SteamPath");
        if (a == null) throw new Exception("Cant find Steam!");
        return a.ToString();
    }
    static IEnumerable<string> GetAppsPath(string Path) => Directory.EnumerateDirectories(Path + "\\steamapps\\common");
}