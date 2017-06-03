using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TioNerdAppXF.Services;

namespace TioNerdAppXF.Models
{
    public class Repositorio
    {
        /// <summary>
        /// Classe que contêm a lógica de acesso aos dados da aplicação
        /// </summary>
        public async Task<List<Friend>> GetFriends()
        {
            var service = new AzureEasyTableService<Friend>();
            var items = await service.GetTable();

            return items.ToList();
        }

    }
}
