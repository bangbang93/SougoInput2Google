using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace SougoInput2Google
{
    public class BaiduEntity
    {
        public string key;
        public string place;
        public string value;
    }
    public class Baidu
    {
        ArrayList baidu = new ArrayList();
        public void Parse(Sougo sougo)
        {
            foreach (SougoEntity se in sougo.sougo)
            {
                BaiduEntity ge = new BaiduEntity();
                ge.key = se.key;
                ge.place = se.place;
                ge.value = se.value;
                baidu.Add(ge);
            }
        }
        public void ToFile(string Path)
        {
            StreamWriter sw = new StreamWriter(Path, false, Encoding.Default);
            foreach (BaiduEntity ge in baidu)
            {
                //sw.WriteLine("{0}\t{1}\t{2}", ge.key, ge.place, ge.value);
                sw.WriteLine("{0},{1}={2}", ge.place, ge.key, ge.value);
            }
            sw.Close();
        }
    }
}
