namespace TestOkur.TestHelper.Extensions
{
    using System.Net.Http;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using FluentAssertions.Primitives;

    public static class HttpResponseMessageExtensions
    {
        public static HttpResponseMessageAssertions Should(this HttpResponseMessage instance)
        {
            return new HttpResponseMessageAssertions(instance);
        }

        public static async Task<string> ExtractAntiForgeryToken(this HttpResponseMessage response)
        {
            var responseAsString = await response.Content.ReadAsStringAsync();
            return await Task.FromResult(ExtractAntiForgeryToken(responseAsString));
        }

        private static string ExtractAntiForgeryToken(string htmlResponseText)
        {
            var match = Regex.Match(htmlResponseText, @"\<input name=""__RequestVerificationToken"" type=""hidden"" value=""([^""]+)"" \/\>");
            return match.Success ? match.Groups[1].Captures[0].Value : null;
        }
    }
}
