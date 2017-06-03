using Microsoft.WindowsAzure.MobileServices;

namespace TioNerdAppXF.Models
{
    [DataTable("People")]
    public class Friend
    {
        [Version]
        public string AzureVersion { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Age { get; set; }

        public string Description { get; set; }

        public string FacebookURL { get; set; }

        public string FacebookImage { get; set; }

        public string CompleteName => Name + " " + LastName;
    }
}
