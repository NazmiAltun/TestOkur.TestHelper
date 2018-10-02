namespace FluentAssertions.Primitives
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Execution;
    using FluentAssertions;

    public class HttpResponseMessageAssertions
        : ReferenceTypeAssertions<HttpResponseMessage, HttpResponseMessageAssertions>
    {
        public HttpResponseMessageAssertions(HttpResponseMessage instance)
        {
            Subject = instance;
        }

        protected override string Identifier => "httpresponsemessage";

        public async Task<AndConstraint<HttpResponseMessageAssertions>> BeBadRequestAsync(string errorMessage, string because = "", params object[] becauseArgs)
        {
            var strContent = await Subject.Content.ReadAsStringAsync();

            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(!string.IsNullOrEmpty(errorMessage))
                .FailWith("Message should not be null or empty")
                .Then
                .ForCondition(Subject.StatusCode == HttpStatusCode.BadRequest)
                .FailWith("Expected 400 (BadRequest) but found {0}", Subject.StatusCode)
                .Then
                .ForCondition(strContent.Contains(errorMessage))
                .FailWith(
                    "Expected error message '{0}' not found in {1}",
                    errorMessage,
                    strContent);

            return new AndConstraint<HttpResponseMessageAssertions>(this);
        }
    }
}
