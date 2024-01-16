using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using _19T1021111.DomainModels;

namespace _19T1021111.Web
{
    /// <summary>
    /// Convert dữ liệu
    /// </summary>
    public static class Converter
    {
        /// <summary>
        /// Convert dữ liệu kiểu ngày tháng
        /// </summary>
        /// <param name="s"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static DateTime? DMYStringToDateTime(string s, string format = "d/M/yyyy")
        {
            try
            {
                return DateTime.ParseExact(s, format, CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }
        public static UserAccount CookieToUserAccount(string cookie)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<UserAccount>(cookie);
        }
    }
}