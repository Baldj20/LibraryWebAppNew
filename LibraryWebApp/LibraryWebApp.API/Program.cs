using LibraryWebApp.Application.Abstractions.Mappers;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Infrastructure;
using LibraryWebApp.Infrastructure.Mappers;
using LibraryWebApp.Infrastructure.Mappers.Custom;
using LibraryWebApp.Infrastructure.Repositories;
using LibraryWebApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryWebApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(AuthorProfile),
                typeof(BookProfile), typeof(UserBookProfile), typeof(UserProfile));

            builder.Services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserBookRepository, UserBookRepository>();

            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<IAuthorService, AuthorService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserBookService, UserBookService>();

            builder.Services.AddScoped<IBookMapper, BookMapper>();
            builder.Services.AddScoped<IAuthorMapper, AuthorMapper>();
            //builder.Services.AddScoped<IUserMapper, UserService>();
            //builder.Services.AddScoped<IUserMapper, UserBookService>();

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
        }
    }
}
