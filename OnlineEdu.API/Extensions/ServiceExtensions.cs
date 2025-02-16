using OnlineEdu.BusinessLayer.Abstract;
using OnlineEdu.BusinessLayer.Concrete;
using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.BusniessLayer.Concrete;
using OnlineEdu.DataAccessLayer.Abstract;
using OnlineEdu.DataAccessLayer.EntityFramework;
using OnlineEdu.DataAccessLayer.Repositories;

namespace OnlineEdu.API.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServiceExtensions(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericDal<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericManager<>));

            services.AddScoped<IBlogDal, EfBlogDal>();
            services.AddScoped<IBlogService, BlogManager>();

            services.AddScoped<ICourseCategoryDal, EfCourseCategoryDal>();
            services.AddScoped<ICourseCategoryService, CourseCategoryManager>();

        }
    }
}
