using System.Linq;
using NUnit.Framework;

namespace Infrastructure.Utils
{
    public static class TestNameUtil
    {
        public static string TestName => TestContext.CurrentContext.Test.Name;

        public static string TestClassName => TestContext.CurrentContext.Test.ClassName;

        public static string TestId =>
            int.TryParse(TestClassName?.Split("_").First().Replace("TC", string.Empty).Split(".").Last(),
                out var testNumber)
                ? testNumber.ToString()
                : string.Empty;
    }
}