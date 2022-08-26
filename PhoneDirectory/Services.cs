



namespace PhoneDirectory
{
    public static class Services
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();
            builder.Services.AddMemoryCache();
            builder.Services.AddControllersWithViews().AddMicrosoftIdentityUI();

            builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
                .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAdB2C"));
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireClaim("jobTitle", "Admin");
                });
            });

            builder.Services.AddSingleton<ISQLDataAccess, SQLDataAccess>();
            builder.Services.AddSingleton<IDataFactory, DataFactory>();
            builder.Services.AddSingleton<IEmployeeData, EmployeeData>();
            builder.Services.AddSingleton<ITitleData, TitleData>();
            builder.Services.AddSingleton<IDepartmentData, DepartmentData>();
        }
    }
}
