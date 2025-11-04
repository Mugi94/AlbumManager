using BusinessObjects.Entity;
using DataAccessLayer.Contexts;
using DataAccessLayer.DataTransferObject;
using DataAccessLayer.Repository;
using Microsoft.EntityFrameworkCore;
using Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add custom services
builder.Services.AddScoped<IGenericRepository<Artist>, ArtistRepository>();
builder.Services.AddScoped<IGenericRepository<Record>, RecordRepository>();
builder.Services.AddScoped<IGenericRepository<Track>, TrackRepository>();

builder.Services.AddScoped<IArtistManager, ArtistManager>();
builder.Services.AddScoped<IRecordManager, RecordManager>();
builder.Services.AddScoped<ITrackManager, TrackManager>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddDbContext<MusicContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
