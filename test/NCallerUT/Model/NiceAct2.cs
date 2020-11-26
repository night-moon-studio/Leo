using System;
using System.Collections.Generic;

namespace NCallerUT.Model
{
    public class NiceAct2
    {
        public NiceAct2() { }

        public NiceAct2(bool init)
        {
            NiceAct act = new NiceAct();
            
            Str = "StrStr";
            Int16 = 16;
            Int32 = 32;
            Int64 = 64;
            Char = 'c';
            Bytes = new byte[] {1,0,1,0,1,0,1,0,1,0};
            DateTime = DateTime.Today;
            DateTimeOffset = DateTime;
            MustByNullObj = null;
            SomeObj = act;
            SomeNiceActArray = new []{act, act, act, act, act};
            SomeNiceActList = new List<NiceAct> {act, act, act, act, act};
        }

        public string Str { get; set; }

        public Int16 Int16 { get; set; }

        public Int32 Int32 { get; set; }

        public Int64 Int64 { get; set; }

        public Char Char { get; set; }

        public byte[] Bytes { get; set; }

        public DateTime DateTime { get; set; }

        public DateTimeOffset DateTimeOffset { get; set; }
        
        public object MustByNullObj { get; set; }
        
        public object SomeObj { get; set; }
        
        public NiceAct[] SomeNiceActArray { get; set; }
        
        public List<NiceAct> SomeNiceActList{ get; set; }
        
        public decimal Discount { get; set; }
        
        public Country Country { get; set; }

        public override int GetHashCode()
        {
            return Str.GetHashCode();
        }
    }
}