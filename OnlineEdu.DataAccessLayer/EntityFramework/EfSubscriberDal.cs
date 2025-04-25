using Microsoft.EntityFrameworkCore;
using OnlineEdu.DataAccessLayer.Abstract;
using OnlineEdu.DataAccessLayer.Concrete;
using OnlineEdu.DataAccessLayer.Repositories;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.DataAccessLayer.EntityFramework
{
    public class EfSubscriberDal : GenericRepository<Subscriber>, ISubscriberDal
    {
        public EfSubscriberDal(OnlineEduContext onlineEduContext) : base(onlineEduContext)
        {
            
        }

        public async Task<bool> CheckNewsletterInbox(string email)
        {
            return await _onlineEduContext.Subscribers.AnyAsync(x=>x.Email == email);
        }
    }
}
