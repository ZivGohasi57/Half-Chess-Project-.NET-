using Microsoft.EntityFrameworkCore;
using FinalProject.Data;
using FinalProject;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Middleware to handle redirection based on login status
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/" || context.Request.Path == "/Index")
    {
        if (LogInHelper.isUserLogIn)
        {
            // Redirect to HomePage2 with userID when logged in
            context.Response.Redirect($"/HomePage2?userId={LogInHelper.userID}");
        }
        else
        {
            // Continue to Index if not logged in
            await next.Invoke();
        }
    }
    else
    {
        await next.Invoke();
    }
});

app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();


app.Run();