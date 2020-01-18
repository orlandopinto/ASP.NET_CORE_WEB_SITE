namespace ASP.NET_CORE_WEB_SITE.Settings
{
	public class StoreAppSettings
	{
		public string WebApiBaseUrl { get; set; }
		public string User { get; set; }
		public string Password { get; set; }
	}

	public class SettingsStoreApp
	{
		public StoreAppSettings StoreAppSettings { get; set; }

	}
}
