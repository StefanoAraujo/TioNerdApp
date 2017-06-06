using Microsoft.WindowsAzure.MobileServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TioNerdAppXF.Services
{
    public class AzureEasyTableService<T>
    {
        // var que armazena a instância de IMobileServiceClient
        private IMobileServiceClient Client;

        // var que armazena uma instância de IMobileServiceTable genérica que representa uma mesa backend.
        private IMobileServiceTable<T> TmobileServiceTable;

        public AzureEasyTableService()
        {
            string myServiceURL = Helpers.Constants.mobileServiceURL;
            Client = new MobileServiceClient(myServiceURL);
            TmobileServiceTable = Client.GetTable<T>();
        }

        public Task<IEnumerable<T>> GetTable()
        {
            return TmobileServiceTable.ToEnumerableAsync();
        }

    }
}
