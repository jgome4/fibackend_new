using FI_Infra_Tools_Core;

namespace FI_Infra_Tools_Aggregate
{
    public interface IToken
    {
        public bool IsValidUser(string user, string password);
    }
}
