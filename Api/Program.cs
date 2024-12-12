using Api.Middlewares;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
{
    var configuration = builder.Configuration;

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddApplication(configuration).AddInfrastructure(configuration);


    // Configurar políticas de autorización
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("Admin", policy =>
            policy.Requirements.Add(new RolesRequirement(
                Guid.Parse(RolConst.Admin)
            )));
        options.AddPolicy("AdminOrReception", policy =>
            policy.Requirements.Add(new RolesRequirement(
                [Guid.Parse(RolConst.Admin), Guid.Parse(RolConst.Reception)]
            )));
    });

    // Registrar el handler de autorización
    builder.Services.AddScoped<IAuthorizationHandler, RolesRequirementHandler>();


    builder.Services.AddCors(options =>
    {
        options.AddPolicy("new", app =>
        {
            app.AllowAnyOrigin();
            app.AllowAnyHeader();
            app.AllowAnyMethod();
        });
    });
}


var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseCors("new");
    app.Run();
}