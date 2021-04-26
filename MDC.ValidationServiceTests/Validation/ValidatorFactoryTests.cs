using MDC.ValidationService.Validation;
using NUnit.Framework;

namespace MDC.ValidationServiceTests.Validation
{
    [TestFixture]
    public class ValidatorFactoryTests
    {
        [TestCase("FxQuote", true)]
        [TestCase("FXQUOTE", true)]
        [TestCase("fxquote", true)]
        [TestCase("SomethingElse", false)]
        public void CreateValidatorTest(string quoteType, bool expectedFxQuoteValidator)
        {
            var factory = new ValidatorFactory();
            var validator = factory.CreateValidator(quoteType);
            Assert.AreEqual(validator is FxQuoteValidator, expectedFxQuoteValidator);
        }
    }
}