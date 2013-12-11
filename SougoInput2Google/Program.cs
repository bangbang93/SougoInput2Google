using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

namespace SougoInput2Google
{
    class Program
    {
        static Regex SougoType = new Regex(@"^.*,\d=.*");
        static Regex SougoTypeKey = new Regex(@"^(.*),\d=.*");
        static Regex SougoTypePlace = new Regex(@"^.*,(\d)=.*");
        static Regex SougoTypeValue = new Regex(@"^.*,\d=(.*)");
        static Sougo sougo = new Sougo();
        static void Main(string[] args)
        {
            string SPYUsers = Environment.GetEnvironmentVariable("LOCALAPPDATA") + @"Low\SogouPY.users";
            DirectoryInfo SPYUserDirs = new DirectoryInfo(SPYUsers);

            foreach (DirectoryInfo SPYUserDir in SPYUserDirs.GetDirectories())
            {
                if (Directory.GetFiles(SPYUserDir.FullName).Contains(SPYUserDir.FullName + "\\phrases.ini"))
                {
                    StreamReader sr = new StreamReader(SPYUserDir.FullName + "\\phrases.ini", Encoding.UTF8);
                    while (!sr.EndOfStream)
                    {
                        string str = sr.ReadLine();
                        if (SougoType.IsMatch(str))
                        {
                            SougoEntity sougoEntity = new SougoEntity();
                            sougoEntity.key = SougoTypeKey.Match(str).Groups[1].Value;
                            sougoEntity.place = SougoTypePlace.Match(str).Groups[1].Value;
                            sougoEntity.value = SougoTypeValue.Match(str).Groups[1].Value;
                            sougo.sougo.Add(sougoEntity);
                        }
                    }
                }
            }
            Console.WriteLine("总共找到{0}条搜狗自定义短语",sougo.sougo.Count);
            Google google = new Google();
            google.Parse(sougo);
            google.ToFile("google.dis");
            Baidu baidu = new Baidu();
            baidu.Parse(sougo);
            baidu.ToFile("baidu.txt");
            Console.WriteLine("Mission Complete,按任意键继续");
            Console.ReadKey();
        }
    }
}
