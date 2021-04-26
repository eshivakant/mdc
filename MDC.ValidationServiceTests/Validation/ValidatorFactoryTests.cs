using System.Collections.Generic;
using MDC.ContributionService.Common;
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

    public class FxQuoteValidator
    {
        [TestCase("GBP/USD", 1.1, 1.2, true)]
        [TestCase("EUR/USD", -1.1, 1.2, false)]
        [TestCase("EUR/BRL", 1.1, -1.2, false)]
        [TestCase("GBP/USD", -1.1, -1.2, false)]
        [TestCase("EUR__USD", 1.1, 1.2, false)]
        [TestCase("EURBRL22", 1.1, 1.2, false)]
        [TestCase("EUR/USD", 1.1, 1.2, true)]
        [TestCase("EUR/BRL", 1.1, 1.2, true)]
        [TestCase("GBP/INR", 1.1, 1.2, true)]
        [TestCase("GBP/AUD", 1.1, 1.2, true)]
        public void Validate(string ticker, double bid, double ask, bool valid)
        {
            var factory = new ValidatorFactory();
            var validator = factory.CreateValidator("FxQuote");
            var computed = validator.Validate(new ContributionRequest()
            {
                ContributionType = "FxQuote",
                RequestFields = new List<RequestField>()
                {
                    new RequestField() {FieldName = "Ticker", Type = FieldType.StringField, Value = ticker},
                    new RequestField() {FieldName = "Bid", Type = FieldType.FloatField, Value = bid},
                    new RequestField() {FieldName = "Ask", Type = FieldType.FloatField, Value = ask}
                }
            });

            Assert.AreEqual(valid, string.IsNullOrEmpty(computed));
        }
    }
}