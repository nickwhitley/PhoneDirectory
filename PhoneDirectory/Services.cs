namespace PhoneDirectory
{
    public static class Services
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddMemoryCache();
        }
    }
}
