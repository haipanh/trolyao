using Solar.Infrastructure.Repository;

namespace Application.Domain
{
    /// <summary>
    /// RuleConfigurer.Configure() is responsible for adding setting up business rules to Rule Manager.
    /// </summary>
    public static class RuleConfigurer
    {
        public static void Configure()
        {
            //RuleManager<Account>.Default
                //.RemoveAllRules()
                //.AddRule(new CustomerEmailMustBeUnique());
                //.AddRule(new CustomerAddressNotIncludePOBoxAddress())
                //.AddRule(new AddressZipCodeMustComplyToStateRule());
        }
    }
}
