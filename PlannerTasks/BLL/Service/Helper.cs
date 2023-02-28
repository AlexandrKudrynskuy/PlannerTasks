using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bll
{
    public static class Helper
    {
        public static string PhotoPathUser { get; set; }
        static Helper()
        {
            PhotoPathUser = Directory.GetCurrentDirectory + "\\PhotoUser";
            if (!Directory.Exists(PhotoPathUser))
            {
                Directory.CreateDirectory(PhotoPathUser);
            }
        }

        public static bool IsCorectLogin(this string str)
        {
            //string patern = @"^[A-z0-9_-]{3,40}$";
            //var regex = new Regex(patern);
            //var match = regex.Matches(str);
            //if (match.Count == 1)
            //{
            //    return true;
            //}
            //return false;
            return true;

        }
        public static bool IsCorectPassword(this string str)
        {
            //string patern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^a-zA-Z0-9])\S{8,16}$";
            //var regex = new Regex(patern);
            //var match = regex.Matches(str);
            //if (match.Count == 1)
            //{
            //    return true;
            //}
            //return false;
            return true;

        }
        public static bool IsCorectName(this string str)
        {
            //string patern = @"^([А-ЯІ]{1}[а-яі]{1,23}|[A-Z]{1}[a-z]{1,23})$";
            //var regex = new Regex(patern);
            //var match = regex.Matches(str);
            //if (match.Count == 1)
            //{
            //    return true;

            //}
            //return false;

            return true;

        }
    }
}