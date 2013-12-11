using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;

namespace SougoInput2Google
{
    public class GoogleEntity
    {
        public string key;
        public string place;
        public string value;
    }
    public class Google
    {
        ArrayList google = new ArrayList();
        public void Parse(Sougo sougo)
        {
            foreach (SougoEntity se in sougo.sougo)
            {
                GoogleEntity ge = new GoogleEntity();
                ge.key = se.key;
                ge.place = se.place;
                ge.value = se.value;
                google.Add(ge);
            }
        }
        public void ToFile(string Path)
        {
            StreamWriter sw = new StreamWriter(Path, false, Encoding.Default);
            foreach (GoogleEntity ge in google)
            {
                //sw.WriteLine("{0}\t{1}\t{2}", ge.key, ge.place, ge.value);
                sw.WriteLine("{0}\t{1}", ge.key, ge.value);
            }
            sw.Close();
        }
    }
}
