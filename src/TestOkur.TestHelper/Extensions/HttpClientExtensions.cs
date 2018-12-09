namespace TestOkur.TestHelper.Extensions
{
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    public static class HttpClientExtensions
    {
        public static async Task<string> DownloadAsync(this HttpClient client, string url)
        {
            var newPath = Path.GetRandomFileName();

            using (var response = await client.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();

                using (var stream = CreateStream(newPath))
                {
                    await response.Content.CopyToAsync(stream);
                }
            }

            return newPath;
        }

        private static FileStream CreateStream(string path)
        {
            return new FileStream(
                path,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None);
        }
    }
}
