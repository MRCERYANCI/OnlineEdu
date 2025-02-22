using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.Abstract
{
    public interface ICourseService : IGenericService<Course>
    {
        Task<List<Course>> TListCourseWithCategories();
        Task TShowOnHome(int courseId);
        Task TDontShowOnHome(int courseId);
    }
}
