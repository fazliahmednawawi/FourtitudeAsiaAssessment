using FourtitudeAsiaAssessment.Application.IService;
using FourtitudeAsiaAssessment.Infrastructure.Service;
using FourtitudeAsiaAssessment.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace FourtitudeAsiaAssessment
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
            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddAutoMapper(typeof(TransactionMapping));
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            builder.Logging.ClearProviders();
            builder.Logging.AddLog4Net("log4net.config");

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
