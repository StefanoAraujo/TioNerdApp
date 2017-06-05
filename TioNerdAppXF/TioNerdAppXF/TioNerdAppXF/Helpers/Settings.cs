using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace TioNerdAppXF.Helpers
{
    /// <summary>
    /// Aqui vamos armazenar o Token do MobileServiceUser depois de autenticado
    /// </summary>
    public static class Settings
    {
        // Vendo qual plataforma estamos rodando
        private static ISettings AppSettings => CrossSettings.Current;

        // Chave que vamos armazenar no local storage
        private const string UserIdKey = "user_id";
        static readonly string UserIdDefault = string.Empty;
        
        private const string AuthTokenKey = "authtoken";
        static readonly string AuthTokenDefault = string.Empty;

        private const string NameId = "name";
        static readonly string NameDefault = string.Empty;

        // Propriedades que irão servir para acessar os valores fora da classe
        public static string AuthToken
        {
            get { return AppSettings.GetValueOrDefault<string>(AuthTokenKey, AuthTokenDefault); }
            set { AppSettings.AddOrUpdateValue<string>(AuthTokenKey, value); }
        }

        public static string UserId
        {
            get { return AppSettings.GetValueOrDefault<string>(UserIdKey, UserIdDefault); }
            set { AppSettings.AddOrUpdateValue<string>(UserIdKey, value); }
        }

        public static string Nome
        {
            get { return AppSettings.GetValueOrDefault<string>(NameId, NameDefault); }
            set { AppSettings.AddOrUpdateValue<string>(NameId, value); }
        }

        // Propriedade para saber se o cara está logado ou não
        public static bool IsLoggedIn => !string.IsNullOrWhiteSpace(UserId);

    }
}