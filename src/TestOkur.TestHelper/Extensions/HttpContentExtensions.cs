namespace TestOkur.TestHelper.Extensions
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

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
