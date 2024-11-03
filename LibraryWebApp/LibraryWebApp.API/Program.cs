using LibraryWebApp.Application.Abstractions.Mappers;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Infrastructure;
using LibraryWebApp.Infrastructure.Mappers;
using LibraryWebApp.Infrastructure.Mappers.Custom;
using LibraryWebApp.Infrastructure.Repositories;
using LibraryWebApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using LibraryWebApp.Application.Abstractions.UseCases.AuthorUseCases;
using LibraryWebApp.Application.UseCases.AuthorUseCases;
using LibraryWebApp.Application.Abstractions.UseCases.BookUseCases;
using LibraryWebApp.Application.UseCases.BookUseCases;
using LibraryWebApp.Application.Abstractions.UseCases.UserUseCases;
using LibraryWebApp.Application.UseCases.UserUseCases;

namespace LibraryWebApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
            builder.Services.Configure<JwtSettings>(jwtSettingsSection);

            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                };
            });
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
            });
            
            
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LibraryAPI", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "{your token}",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {           
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
            });
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(AuthorProfile),
                typeof(BookProfile), typeof(UserBookProfile), typeof(UserProfile));

            builder.Services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IBookRepository, BookRepository>();
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserBookRepository, UserBookRepository>();
            builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            builder.Services.AddScoped<IBookMapper, BookMapper>();
            builder.Services.AddScoped<IAuthorMapper, AuthorMapper>();
            builder.Services.AddScoped<IUserMapper, UserMapper>();
            builder.Services.AddScoped<IUserBookMapper, UserBookMapper>();
            builder.Services.AddScoped<ITokenMapper, TokenMapper>();

            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserBookService, UserBookService>();

            builder.Services.AddScoped<IAddAuthorUseCase, AddAuthorUseCase>();
            builder.Services.AddScoped<IDeleteAuthorUseCase, DeleteAuthorUseCase>();
            builder.Services.AddScoped<IGetAllAuthorsUseCase, GetAllAuthorsUseCase>();
            builder.Services.AddScoped<IGetAuthorBooksUseCase, GetAuthorBooksUseCase>();
            builder.Services.AddScoped<IGetAuthorByIdUseCase, GetAuthorByIdUseCase>();
            builder.Services.AddScoped<IUpdateAuthorUseCase, UpdateAuthorUseCase>();
            builder.Services.AddScoped<IGetPagedAuthorsUseCase, GetPagedAuthorsUseCase>();

            builder.Services.AddScoped<IAddBookUseCase, AddBookUseCase>();
            builder.Services.AddScoped<IDeleteBookUseCase, DeleteBookUseCase>();
            builder.Services.AddScoped<IGetAllBooksUseCase, GetAllBooksUseCase>();
            builder.Services.AddScoped<IGetBookByISBNUseCase, GetBookByISBNUseCase>();
            builder.Services.AddScoped<IUpdateBookUseCase, UpdateBookUseCase>();
            builder.Services.AddScoped<IGetPagedBooksUseCase, GetPagedBooksUseCase>();

            builder.Services.AddScoped<IAddUserUseCase, AddUserUseCase>();
            builder.Services.AddScoped<IDeleteUserUseCase, DeleteUserUseCase>();
            builder.Services.AddScoped<IGetAllUsersUseCase, GetAllUsersUseCase>();
            builder.Services.AddScoped<IGetUserByLoginUseCase, GetUserByLoginUseCase>();
            builder.Services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            builder.Services.AddScoped<IAuthorizeUseCase, AuthorizeUseCase>();
            builder.Services.AddScoped<IRegisterBookForUserUseCase, RegisterBookForUserUseCase>();
            builder.Services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            builder.Services.AddScoped<IGetPagedUsersUseCase, GetPagedUsersUseCase>();

            builder.Logging.AddConsole();
            var app = builder.Build();
            app.UseMiddleware<ExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
