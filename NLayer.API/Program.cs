using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWork;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using System.Reflection;

namespace NLayer.API
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

            //Burada ilk yazd���m�z interface ile kar��la��rsak ikinci yazd���m�z class � nesne �rne�i almas� i�in scoped belirlitoruz.
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); 
            builder.Services.AddScoped(typeof(IGenericReposit�ry<>), typeof(GenericRepository<>));
            builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

            builder.Services.AddAutoMapper(typeof(MapProfile));


            builder.Services.AddDbContext<AppDbContext>(x =>
            {
                x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>   //DbContext in hangi Assembly i�erisinde oldu�unu belirtmek i�in yaz�yoruz.
                {
                    //option.MigrationsAssembly("NLayer.Repository"); bu �ekilde yazabilirdik ancak Tip g�venli�i i�in a�a��daki gibi yapt�k.
                    option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name); 
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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}