using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DerestageClasses
{
    public class Certification
    {
        public static string encodedUdid;

        public static string Udid
        {
            get
            {
                return Cryptographer.decode(encodedUdid);
            }
        }
    }
}
