using FI_Infra_Tools_Aggregate;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Reflection;

namespace FI_Infra_Tools_Implementation
{
    public class TokenService:IToken
    {
        public TokenService(IConfiguration configuration)
        {

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }



        public bool IsValidUser(string user, string password)
        {
            return Configuration["tokenManagement:User"].ToString().Equals(user) && Configuration["tokenManagement:Password"].ToString().Equals(password);
        }

        
    }
}
