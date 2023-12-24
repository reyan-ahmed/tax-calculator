namespace congestion_tax_calculator.Services;

public static class ServicesExtensions
{
    public static void AddCongestionTaxServices(this IServiceCollection services)
    {
        services.AddSingleton<ICongestionTaxService, CongestionTaxService>();
    }
}
