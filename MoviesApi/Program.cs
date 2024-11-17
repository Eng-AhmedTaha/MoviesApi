using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MoviesApi.Models;
using MoviesApi.Services;

var builder = WebApplication.CreateBuilder(args);



var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("No connection string was found");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));



builder.Services.AddTransient<IGenresService, GenresService>();
builder.Services.AddTransient<IMoviesService, MoviesService>();

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Movies",
        Description = "My First API",
        TermsOfService = new Uri("https://www.facebook.com/profile.php?id=100074316274412"),
        Contact = new OpenApiContact
        {
            Name = "Taha Mohammmed",
            Email = "Test@gamil.com",
            Url = new Uri("https://www.google.com")

        }
       ,
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://www.google.com"),
        }
       
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type =SecuritySchemeType.ApiKey,
        Scheme= "Bearer",
        BearerFormat ="JWT"
        ,In = ParameterLocation.Header,
        Description ="Enter Your JWT Key"

    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                            Type = ReferenceType.SecurityScheme,

                            Id ="Bearer"
            },
            Name = "Bearer", 
            In = ParameterLocation.Header, 
        },
        new List<string>() 
    }
});

});





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(c=> c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
