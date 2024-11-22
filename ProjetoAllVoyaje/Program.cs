using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoAllVoyaje.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<AllVoyajeDbContext>
    (options => options.UseSqlServer(connectionString));

/*----------------------------------------------*/
builder.Services.AddSingleton<IConexaoSql>(new ConexaoSql(connectionString));
/*----------------------------------------------*/
var app = builder.Build();

/**************************************************/
/*using (var scope = app.Services.CreateScope())
{
    // Recupera ambos os contextos
    var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var minhaDbContext = scope.ServiceProvider.GetRequiredService<AllVoyajeDbContext>();

    // Aplica as migrações do Identity
    applicationDbContext.Database.Migrate();  // Aplica as migrações do ApplicationDbContext (Identity)

    // Aplica as migrações para as tabelas de negócio (MinhaDbContext)
    minhaDbContext.Database.Migrate();  // Aplica as migrações do MinhaDbContext (tabelas de negócio)

    // Após aplicar as migrações, cria a trigger no banco de dados (no contexto de negócio)
    minhaDbContext.CreateTrigger();  // Criação da trigger para as tabelas de negócio
}*/

/**************************************************/


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
