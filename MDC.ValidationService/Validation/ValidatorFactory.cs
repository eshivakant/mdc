namespace MDC.ValidationService.Validation
{
    public class ValidatorFactory
    {
        public IContributionRequestValidator CreateValidator(string type)
        {
            switch (type.ToUpper())
            {
                case "FXQUOTE":
                    return new FxQuoteValidator();
                default:
                    return new CatchAllValidator();
            }
        }
    }
}