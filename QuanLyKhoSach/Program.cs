using Microsoft.EntityFrameworkCore;
using QuanLyKhoSach.Interface.Repository;
using QuanLyKhoSach.Interface.Service;
using QuanLyKhoSach.Mapper;
using QuanLyKhoSach.Models;
using QuanLyKhoSach.Repository;
using QuanLyKhoSach.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<BookDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBookService,BookService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddLogging(
    logging => logging.AddConsole()
    );
builder.Services.AddHttpClient();
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAllOrigins", policy =>
//    {
//        policy.AllowAnyOrigin()  
//              .AllowAnyMethod() 
//              .AllowAnyHeader(); 
//    });
//});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.MapGet("/", async () =>
{
    var httpClient = app.Services.GetRequiredService<HttpClient>();
    var response = await httpClient.GetStringAsync("https://pokeapi.co/api/v2/pokemon/ditto");
    return $"Welcome to the Book Management API! Data: {response}";
});
app.Run();