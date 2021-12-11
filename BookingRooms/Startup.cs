using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingRooms.Interfaces;
using BookingRooms.Services;
using BookingRooms.DataAccessLayer.Repository;
using BookingRooms.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text.Json.Serialization;
using System.Text.Json;
using BookingRooms.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Protocols;
using BookingRooms.ActionFilters;
using BookingRooms.Models;

namespace BookingRooms
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<BRoomsContext>(
                    options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<AsyncActionFilterBookingValidation>();
            services.AddScoped<AsyncActionFilterBookingIdValidation>();
            services.AddScoped<AsyncActionFilterBookingRoomIdValidation>();
            services.AddScoped<AsyncActionFilterRoomValidation>();
            services.AddScoped<AsyncActionFilterRoomIdValidation>();
            services.AddScoped<AsyncActionFilterUserValidation>();
            services.AddScoped<AsyncActionFilterUserIdValidation>();

            services.AddDbContext<AppIdentityContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<AppUser, IdentityRole>()
                    .AddEntityFrameworkStores<AppIdentityContext>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddCors(o => o.AddPolicy("MVCClient", builder =>
            {
                builder.WithOrigins("https://localhost:44329", "http://localhost:20074")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors("MVCClient");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                //endpoints.MapControllerRoute(
                //        name: "default",
                //        pattern: "{controller=Room/Index}/{action=Index}");
            });

            app.UseSwagger();
            app.UseSwaggerUI();      
        }
    }
}
