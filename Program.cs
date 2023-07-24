using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;

using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddLocalization(option => option.ResourcesPath = "Resources");

builder.Services
    //.AddControllersWithViews()
    .AddMvc()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix
    //, options =>
    //{
    //    options.ResourcesPath = "Resources";
    //}
    );


builder.Services
    .Configure<RequestLocalizationOptions>(options =>
    {

        var supportedCultures = new[]
        {
            "en",
            "en-GB",
            "en-US",
            "fr",
            "ja",
            "pl",
            "de"
        };

        // We can use methods to configure cultures
        //options
        //    .SetDefaultCulture(supportedCultures[0])
        //    .AddSupportedCultures(supportedCultures)
        //    .AddSupportedUICultures(supportedCultures);

        var supportedCulturesList = new List<CultureInfo>();

        System.Array.ForEach(supportedCultures, (cultureTag) => {
            supportedCulturesList.Add(new CultureInfo(cultureTag));
        });

        // We can also use property counterparts
        options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pl");
        options.SupportedCultures = supportedCulturesList;
        options.SupportedUICultures = supportedCulturesList;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
