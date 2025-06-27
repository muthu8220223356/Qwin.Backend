
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Qwin.WebAPI.Data;
using Qwin.WebAPI.Fiters;
using Qwin.WebAPI.Repository;
using System.Text;

namespace Qwin.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);
            string cs = builder.Configuration.GetConnectionString("QwinCS");
            // Add services to the container.
            builder.Services.AddDbContext<QwinDbContext>(p => p.UseSqlServer(cs));
            builder.Services.AddControllers();
            builder.Services.AddScoped<IUserRepository,UserRepository>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //Exception Filter
            builder.Services.AddControllers(options => {
                options.Filters.Add<CustomExceptionFilter>();
            });

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            // Configure JWT authentication
            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
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
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
                };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            //app.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseCors(p => p.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
