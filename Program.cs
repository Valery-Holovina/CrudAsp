// dotnet new webapp -n MyWebApp && cd MyWebApp && dotnet run


using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UsersContext>(); // add DbContext
builder.Services.AddScoped<UserService>();

builder.Services.AddRazorPages();




// =========================
            // CORS. ( allow with frontend)
            // =========================
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseStaticFiles();
app.UseDefaultFiles();
app.UseCors("AllowAll");
 

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UsersContext>();
    
    if (db.Database.CanConnect())
    {
        Console.WriteLine("✅ Database connected!");
    }
    else
    {
        Console.WriteLine("❌ Database NOT connected!");
    }
}


app.MapGet("/api/users", async (UserService service) =>
{
    return await service.GetAll();
});

app.MapPost("/api/create", async (Users user, UserService service) =>
{
    return await service.Create(user);
});

app.MapPut("/api/users/{id}", async (int id, Users user, UserService service) =>
{
    var result = await service.Update(id, user);
    return result is null ? Results.NotFound() : Results.Ok(result);
});

app.MapDelete("/api/delete/{id}", async (int id, UserService service) =>
{
    var success = await service.Delete(id);
    return success ? Results.Ok() : Results.NotFound();
});

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
