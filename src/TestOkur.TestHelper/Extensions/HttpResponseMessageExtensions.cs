namespace FluentAssertions
{
    using System.Net.Http;
    using Primitives;

    public static class HttpResponseMessageExtensions
    {
        public static HttpResponseMessageAssertions Should(this HttpResponseMessage instance)
        {
            return new HttpResponseMessageAssertions(instance);
        }
    }
}
