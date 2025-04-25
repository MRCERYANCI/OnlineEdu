using OnlineEdu.BusinessLayer.Abstract;
using OnlineEdu.BusinessLayer.Concrete;
using OnlineEdu.BusinessLayer.Configurations;
using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.BusniessLayer.Concrete;
using OnlineEdu.DataAccessLayer.Abstract;
using OnlineEdu.DataAccessLayer.EntityFramework;
using OnlineEdu.DataAccessLayer.Repositories;

namespace OnlineEdu.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServiceExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));

            services.AddScoped<IBlogDal, EfBlogDal>();
            services.AddScoped<IBlogService, BlogManager>();

            services.AddScoped<ICourseCategoryDal, EfCourseCategoryDal>();
            services.AddScoped<ICourseCategoryService, CourseCategoryManager>();

            services.AddScoped<ICourseDal, EfCourseDal>();
            services.AddScoped<ICourseService, CourseManager>();

            services.AddScoped<IBlogCategoryDal, EfBlogCategoryDal>();
            services.AddScoped<IBlogCategoryService, BlogCategoryManager>();

            services.AddScoped<ISubscriberDal, EfSubscriberDal>();
            services.AddScoped<ISubscriberService, SubscriberManager>();

            services.AddScoped<IJwtService, JwtManager>();


            services.Configure<JwtTokenOptions>(configuration.GetSection("JwtTokenOptions"));
        }
    }
}
