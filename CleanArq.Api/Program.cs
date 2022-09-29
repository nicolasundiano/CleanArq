using CleanArq.Api;
using CleanArq.Application;
using CleanArq.Infrastructure;
using CleanArq.Infrastructure.IdentityAuth;
using CleanArq.Infrastructure.IdentityAuth.Seeds;
using CleanArq.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler("/error");
    app.UseStatusCodePagesWithReExecute("/error/{0}");

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    await RunMigrations();
    app.Run();
}

async Task RunMigrations()
{
    using var scope = app!.Services.CreateScope();

    var service = scope.ServiceProvider;

    var loggerFactory = service.GetRequiredService<ILoggerFactory>();

    try
    {
        var context = service.GetRequiredService<AppDbContext>();
        await context.Database.MigrateAsync();
        //await AppDbContextSeed.SeedAsync(context, loggerFactory);

        var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
        var identityContext = service.GetRequiredService<IdentityAuthDbContext>();
        await identityContext.Database.MigrateAsync();
        await SeedRoles.SeedRolesAsync(identityContext, roleManager, loggerFactory);
    }
    catch (Exception e)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e, "An error occured during migration");
    }
}