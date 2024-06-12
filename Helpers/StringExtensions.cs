using System.Globalization;
using System.Text;

namespace LHShop.Helpers
{
    public static class StringExtensions
    {
        private static readonly Dictionary<char, string> VietnameseSigns = new Dictionary<char, string>
        {
            {'à', "a"}, {'á', "a"}, {'ạ', "a"}, {'ả', "a"}, {'ã', "a"},
            {'â', "a"}, {'ầ', "a"}, {'ấ', "a"}, {'ậ', "a"}, {'ẩ', "a"}, {'ẫ', "a"},
            {'ă', "a"}, {'ằ', "a"}, {'ắ', "a"}, {'ặ', "a"}, {'ẳ', "a"}, {'ẵ', "a"},
            {'è', "e"}, {'é', "e"}, {'ẹ', "e"}, {'ẻ', "e"}, {'ẽ', "e"},
            {'ê', "e"}, {'ề', "e"}, {'ế', "e"}, {'ệ', "e"}, {'ể', "e"}, {'ễ', "e"},
            {'ì', "i"}, {'í', "i"}, {'ị', "i"}, {'ỉ', "i"}, {'ĩ', "i"},
            {'ò', "o"}, {'ó', "o"}, {'ọ', "o"}, {'ỏ', "o"}, {'õ', "o"},
            {'ô', "o"}, {'ồ', "o"}, {'ố', "o"}, {'ộ', "o"}, {'ổ', "o"}, {'ỗ', "o"},
            {'ơ', "o"}, {'ờ', "o"}, {'ớ', "o"}, {'ợ', "o"}, {'ở', "o"}, {'ỡ', "o"},
            {'ù', "u"}, {'ú', "u"}, {'ụ', "u"}, {'ủ', "u"}, {'ũ', "u"},
            {'ư', "u"}, {'ừ', "u"}, {'ứ', "u"}, {'ự', "u"}, {'ử', "u"}, {'ữ', "u"},
            {'ỳ', "y"}, {'ý', "y"}, {'ỵ', "y"}, {'ỷ', "y"}, {'ỹ', "y"},
            {'đ', "d"},
            {'À', "a"}, {'Á', "a"}, {'Ạ', "a"}, {'Ả', "a"}, {'Ã', "a"},
            {'Â', "a"}, {'Ầ', "a"}, {'Ấ', "a"}, {'Ậ', "a"}, {'Ẩ', "a"}, {'Ẫ', "a"},
            {'Ă', "a"}, {'Ằ', "a"}, {'Ắ', "a"}, {'Ặ', "a"}, {'Ẳ', "a"}, {'Ẵ', "a"},
            {'È', "e"}, {'É', "e"}, {'Ẹ', "e"}, {'Ẻ', "e"}, {'Ẽ', "e"},
            {'Ê', "e"}, {'Ề', "e"}, {'Ế', "e"}, {'Ệ', "e"}, {'Ể', "e"}, {'Ễ', "e"},
            {'Ì', "i"}, {'Í', "i"}, {'Ị', "i"}, {'Ỉ', "i"}, {'Ĩ', "i"},
            {'Ò', "o"}, {'Ó', "o"}, {'Ọ', "o"}, {'Ỏ', "o"}, {'Õ', "o"},
            {'Ô', "o"}, {'Ồ', "o"}, {'Ố', "o"}, {'Ộ', "o"}, {'Ổ', "o"}, {'Ỗ', "o"},
            {'Ơ', "o"}, {'Ờ', "o"}, {'Ớ', "o"}, {'Ợ', "o"}, {'Ở', "o"}, {'Ỡ', "o"},
            {'Ù', "u"}, {'Ú', "u"}, {'Ụ', "u"}, {'Ủ', "u"}, {'Ũ', "u"},
            {'Ư', "u"}, {'Ừ', "u"}, {'Ứ', "u"}, {'Ự', "u"}, {'Ử', "u"}, {'Ữ', "u"},
            {'Ỳ', "y"}, {'Ý', "y"}, {'Ỵ', "y"}, {'Ỷ', "y"}, {'Ỹ', "y"},
            {'Đ', "d"}
        };

        public static string GenerateAlias(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty; // Trả về chuỗi rỗng nếu text là null hoặc rỗng
            }

            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            var withoutDiacritics = stringBuilder.ToString().Normalize(NormalizationForm.FormC);

            var finalString = new StringBuilder();
            foreach (var c in withoutDiacritics)
            {
                finalString.Append(VietnameseSigns.ContainsKey(c) ? VietnameseSigns[c] : c.ToString());
            }

            return finalString.ToString().ToLower().Replace(" ", "-");
        }
    }
}
