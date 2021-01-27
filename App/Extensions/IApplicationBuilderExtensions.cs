using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace App.Extensions
{
    public static class IApplicationBuilderExtensions
    {
          public static IApplicationBuilder UsarCulturaBrasileira(this IApplicationBuilder app){
            
            var ptbr = "pt-BR";
            var supportedCultures = new[] { new CultureInfo(ptbr) };
            
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: ptbr, uiCulture: ptbr),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            return app;
        }
    }
}