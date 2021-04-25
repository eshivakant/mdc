namespace MDC.AuthService.Domain
{
    public interface IBusinessUsersRepo
    {
        void Add(BusinessUser agent);
        BusinessUser FindByLogin(string login);
    }
}
