using System;
using System.Text;

namespace Infrastructure.Utils
{
    public static class StringUtil
    {
        public static readonly string SpecialCharacters = "!\"#$%&'()*+,-. /:;<=>?@[\\]^_`{|}~";
        public static readonly string Space = " ";
        public static readonly string AllCharsAndNumbers = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly Random Random = new();

        public static string TimeStamp => $"{DateTime.Now:hhmmssfff}";

        public static string GetRandomString(int length = 10)
        {
            StringBuilder newRandomString = new();
            for (int i = 0; i < length; i++)
            {
                newRandomString.Append(AllCharsAndNumbers[Random.Next(AllCharsAndNumbers.Length)]);
            }
            return newRandomString.ToString();
        }

        public static string ToMarkdownString(this string value)
        {
            return value
                .Replace("&", "&amp;")
                .Replace("\"", "&quot;")
                .Replace("'", "&#39;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;");
        }

        public static string BigText => "12340ф-=][\';/.,tesq\r\n" +
                                        "!@#$%^&*()_+}{:\" |?><\r\n" +
                                        "Lorem ipsum dolor sit amet, Римский император Константин I Великий, 北京位於華北平原的西北边缘\r\n" +
                                        "Null ᴮᴵᴳᴮᴵᴿᴰ\r\n" +
                                        "email@subdomain.domain.com\r\n" +
                                        "email@domain.com\r\n" +
                                        "www.mysite.com\r\n" +
                                        "http://www.mysite.com:80\r\n" +
                                        "foo://www.mysite.com\r\n" +
                                        "1E-16\r\n" +
                                        "$5,000.00\r\n" +
                                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.Lorem\r\n" +
                                        "     leading tabs and spaces\r\n" +
                                        "');--\r\n" +
                                        "'--\r\n" +
                                        "<script>alert(1)</script>\r\n" +
                                        "<img src onerror=(1)>\r\n" +
                                        "<blink>Blinki</blink>\r\n" +
                                        "<script src=\"data:;base64,YWxlcnQoZG9jdW1lbnQuZG9tYWluKQ==\"></script>\r\n" +
                                        "NͫOͬ\r\n" +
                                        "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
    }
}