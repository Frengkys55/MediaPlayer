using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MediaPlayer

    /*
     * This class is used for validation purposes.
     */

    public static class Validator
    {
        public static bool IsUrl(string input)
        {
            if (input.ToLower().Contains("http://") || input.ToLower().Contains("ftp://"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsHttp(string input)
        {
            if (input.ToLower().Contains("http://"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsFtp(string input)
        {
            if (input.ToLower().Contains("ftp://"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}