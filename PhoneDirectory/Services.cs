using PhoneDirectoryLibrary.Data.Interfaces;

namespace PhoneDirectory
{
    public static class Services
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddMemoryCache();
            builder.Services.AddTransient<ISQLDataAccess, SQLDataAccess>();
            builder.Services.AddTransient<IDataFactory, DataFactory>();
            builder.Services.AddTransient<IEmployeeData, EmployeeData>();
            builder.Services.AddTransient<ITitleData, TitleData>();
            builder.Services.AddTransient<IDepartmentData, DepartmentData>();
        }
    }
}
