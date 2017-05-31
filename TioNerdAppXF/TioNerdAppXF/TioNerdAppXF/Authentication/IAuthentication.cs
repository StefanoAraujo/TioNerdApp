using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace TioNerdAppXF.Authentication
{
    public interface IAuthentication
    {
        /// <summary>
        /// Método que vai retornar um MobileServiceUser, que é o usuário do Mobile Apps
        /// </summary>
        Task<MobileServiceUser> LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider,
            IDictionary<string, string> paremeters = null);
    }
}
