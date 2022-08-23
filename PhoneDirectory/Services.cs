

namespace PhoneDirectory
{
    public static class Services
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddMemoryCache();
            builder.Services.AddSingleton<ISQLDataAccess, SQLDataAccess>();
            builder.Services.AddSingleton<IDataFactory, DataFactory>();
            builder.Services.AddSingleton<IEmployeeData, EmployeeData>();
            builder.Services.AddSingleton<ITitleData, TitleData>();
            builder.Services.AddSingleton<IDepartmentData, DepartmentData>();
        }
    }
}
