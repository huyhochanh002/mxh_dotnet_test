using Microsoft.EntityFrameworkCore;
using WebAPI_MXH.Data;
using WebAPI_MXH.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDBContext>(options =>
   { 
       
       options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnections"));
   
   
   });

// scope nè nhớ nha má  
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<GroupService>();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("app", build =>
    {
        build.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("app");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
