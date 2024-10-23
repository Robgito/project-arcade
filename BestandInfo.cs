using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_3___Arcade
{
    public class BestandInfo
    {
        private string _naam;
        private DateTime _opmaakDatum;

        public BestandInfo()
        {
        }
        public BestandInfo(string naam, DateTime opmaakDatum)
        {
            Naam = naam;
            OpmaakDatum = opmaakDatum;
        }

        public string Naam { get { return _naam; } set { _naam = value; } }
        public DateTime OpmaakDatum { get { return _opmaakDatum; } set { _opmaakDatum = value; } }

        public override string ToString()
        {
            return $"{Naam}  -  {OpmaakDatum}";
        }
    }
}
