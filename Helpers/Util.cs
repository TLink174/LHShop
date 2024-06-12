﻿using System.Text;

namespace LHShop.Helpers
{
    public class Util
    {
        public static string GenerateRandomKey(int length = 5)
        {
            var pattern = @"qwertyuiopassdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM!";
            var sb = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }

            return sb.ToString();
        }

        public static string UploadHinh(IFormFile Hinh, string folder)
        {
            try
            {
                var fullPath = Path.Combine
                                     (Directory.GetCurrentDirectory(), "wwwroot", folder, Hinh.FileName);
                using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
                {
                    Hinh.CopyTo(myfile);
                }
            return Hinh.FileName;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }    
        }
    }
}
