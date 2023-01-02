

using Application.Services.AuthService;
using Application.Services.BrandService;
using Application.Services.CategoryService;
using Application.Services.ColorService;
using Application.Services.CompanyService;
using Application.Services.ProductService;
using Application.Services.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceRegistiration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService,AuthManager>();
        services.AddScoped<IBrandService,BrandManager>();
        services.AddScoped<IColorService,ColorManager>();
        services.AddScoped<IProductService,ProductManager>();
        services.AddScoped<ICategoryService,CategoryManager>();
        services.AddScoped<ICompanyService,CompanyManager>();
        return services;
    }
}
