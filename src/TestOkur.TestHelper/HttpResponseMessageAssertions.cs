namespace TestOkur.TestHelper
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using FluentAssertions;
    using FluentAssertions.Execution;
    using FluentAssertions.Primitives;

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
                .FailWith($"Expected 400 (BadRequest) but found {Subject.StatusCode}")
                .Then
                .ForCondition(strContent.Contains(errorMessage))
                .FailWith($"Expected error message '{errorMessage}' not found in {strContent}");

            return new AndConstraint<HttpResponseMessageAssertions>(this);
        }
    }
}
