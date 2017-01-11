using System;

namespace Service
{
    public static class Extensions
    {
        public static bool IsBase64Encoded(this string str)
        {
            try
            {
                var bytes = Convert.FromBase64String(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
