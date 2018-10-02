namespace System.Net.Http
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Threading.Tasks;

    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsync<T>(this HttpResponseMessage message)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            };
            var json = await message.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json, settings);
        }
    }
}
