namespace System.Net.Http
{
    using IO;
    using Threading.Tasks;

    public static class HttpClientExtensions
    {
        public static async Task<string> DownloadAsync(this HttpClient client, string url)
        {
            var newPath = Path.GetRandomFileName();

            using (var response = await client.GetAsync(url))
            {
                response.EnsureSuccessStatusCode();

                using (var stream = new FileStream(
                    newPath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None))
                {
                    await response.Content.CopyToAsync(stream);
                }
            }

            return newPath;
        }
    }
}
