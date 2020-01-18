namespace ASP.NET_CORE_WEB_SITE.Settings
{
    public interface IOptionsSettings<out TOptions> where TOptions : class, new()
    {
        TOptions Value { get; }
    }
}
