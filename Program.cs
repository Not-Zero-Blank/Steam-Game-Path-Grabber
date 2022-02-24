using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
public class SteamPathGrabber
{
    public static string Grab(string SteamGameToGrab)
    {
        try
        {
            string Raw = File.ReadAllText(GetSteamPath() + @"\steamapps\libraryfolders.vdf");
            int count = -1;
            foreach (string a in Raw.Split(new string[] { "\"path\"" }, StringSplitOptions.None))
            {
                if (count == -1)
                {

                }
                else
                {
                    Console.WriteLine($"Got Library {count} Path: {a.Split('"')[1]}\nChecking for {SteamGameToGrab}...");
                    foreach (string b in GetAppsPath(a.Split('"')[1]))
                    {
                        if (b.Contains(SteamGameToGrab))
                        {
                            return b;
                        }
                    }
                }
                count++;
            }
            throw new Exception("Game coudnt be Found or isnt Installed!");
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    static string GetSteamPath()
    {
        object a = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam").GetValue("SteamPath"); //Just Reading doesnt require Admin
        if (a == null) throw new Exception("Cant find Steam!");
        return a.ToString();
    }
    static IEnumerable<string> GetAppsPath(string Path) => Directory.EnumerateDirectories(Path + "\\steamapps\\common");
}