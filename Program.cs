using Microsoft.EntityFrameworkCore;
using SSResurrezioneBR.Models.Services.Application.Almanacco;
using SSResurrezioneBR.Models.Services.Application.CoroPolifonicoMaterMisericodie;
using SSResurrezioneBR.Models.Services.Application.ImgForCarousel;
using SSResurrezioneBR.Models.Services.Application.Incarichi;
using SSResurrezioneBR.Models.Services.Infrastructure;
using SSResurrezioneBR.Models.Options;
using Microsoft.Extensions.Caching.Memory;
using SSResurrezioneBR.Models.Services.Application.CallApiSantiDelGiorno;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using SSResurrezioneBR.Models.Services.Application.AppuntamentiDellaSettimana;
using SSResurrezioneBR.Models.Services.Application.ImgAppuntamentiDellaSettimana;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.Configure<KestrelServerOptions>(configuration.GetSection("Kestrel"));

builder.Services.AddTransient<IIncarichiService, IncarichiService>();
builder.Services.AddTransient<IImgForCarouselService, ImgForCarouselService>();
builder.Services.AddTransient<IImgAppuntamentiDellaSettimanaService, ImgAppuntamentiDellaSettimanaService>();
builder.Services.AddTransient<ICallApiSantiDelGiornoService, CallApiSantiDelGiornoService>();
builder.Services.AddTransient<ISqliteDatabaseAccessor, SqliteDatabaseAccessor>();

builder.Services.AddTransient<IAlmanaccoService, EfCoreAlmanaccoService>();
builder.Services.AddTransient<ICachedAlmanaccoService, MemoryCacheAlmanaccoService>();
//builder.Services.AddTransient<ICachedAlmanaccoService, DistribuitedCacheAlmanaccoService>();
builder.Services.AddTransient<ICoroPolifonicoMaterMisericordieService, EfCoreCoroPolifonicoMaterMisericordieService>();
builder.Services.AddTransient<ICachedCoroPolifonicoMaterMisericordieService, MemoryCacheCoroPolifonicoMaterMisericordieService>();
builder.Services.AddSingleton<IImgPersister, MagickNetImgForCarouselImagePersister>();
builder.Services.AddSingleton<IImgPersisterAS, MagickNetImgAppuntamentiDellaSettimanaImagePersister>();

builder.Services.AddDbContextPool<SSResurrezioneDbContext>(optionsBuilder => {
    string connectionString = configuration.GetSection("ConnectionStrings").GetValue<string>("Default");
    optionsBuilder.UseSqlite(connectionString);
});

#region Configurazione Cache distribuita
var services = new ServiceCollection();
services.Configure<RedisCacheOptions>(configuration.GetSection("DistributedCache:Redis"));
builder.Services.AddStackExchangeRedisCache(
    options =>
    {
        //Configuration.Bind("distribuitedCache:Redis", options);
        var redisCacheOptions = configuration.GetSection("DistributedCache:Redis").Get<RedisCacheOptions>();
        options.Configuration = redisCacheOptions.Configuration;
        options.InstanceName = redisCacheOptions.InstanceName;
    });
#endregion

// Options
builder.Services.Configure<ConnectionStringsOptions>(configuration.GetSection("ConnectionStrings"));
builder.Services.Configure<SSResurrezioneOptions>(configuration.GetSection("SSResurrezione"));
builder.Services.Configure<MemoryCacheOptions>(configuration.GetSection("MemoryCache"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}else {
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
