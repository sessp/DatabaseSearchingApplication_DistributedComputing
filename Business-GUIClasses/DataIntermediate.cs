using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_GUIClasses
{
    public class DataIntermediate
    {
        public int bal;
        public uint acct;
        public uint pin;
        public string fname;
        public string lname;
        //TODO: Add profile pictures here 

        public override string ToString() 
        {
            return fname + "," + lname + " " + bal.ToString() + " " + acct.ToString() + " " + pin.ToString() + " ";
        }
    }
}
