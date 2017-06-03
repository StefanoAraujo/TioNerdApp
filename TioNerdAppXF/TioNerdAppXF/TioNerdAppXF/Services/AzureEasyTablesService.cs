using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;

namespace TioNerdAppXF.Services
{
    public class AzureEasyTablesService<T>
    {
        // var que armazena a instância de IMobileServiceClient
        private IMobileServiceClient Client;

        // var que armazena uma instância de IMobileServiceTable genérica que representa uma mesa backend.
        private IMobileServiceTable<T> TmobileServiceTable;

        public AzureEasyTablesService()
        {
            string myServiceURL = "http://tionerdapp.azurewebsites.net";
            Client = new MobileServiceClient(myServiceURL);
            TmobileServiceTable = Client.GetTable<T>();
        }

        public Task<IEnumerable<T>> GetTable()
        {
            return TmobileServiceTable.ToEnumerableAsync();
        }
    }
}
