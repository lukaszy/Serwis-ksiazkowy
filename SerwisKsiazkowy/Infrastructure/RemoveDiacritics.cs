using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace SerwisKsiazkowy.Infrastructure
{
    public static class RemoveDiacritics
    {
        public static string Diacritics(string text)
        {
            
            string normal = text.Normalize(NormalizationForm.FormD);

            var withoutDiacritics = normal.Where(
                c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark);

            string final = new string(withoutDiacritics.ToArray());

            return final;
        }
    }
}