using OnlineEdu.BusinessLayer.Abstract;
using OnlineEdu.BusniessLayer.Concrete;
using OnlineEdu.DataAccessLayer.Abstract;
using OnlineEdu.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEdu.BusinessLayer.Concrete
{
    public class SubscriberManager : GenericManager<Subscriber>, ISubscriberService
    {
        private readonly ISubscriberDal _subscriberDal;

        public SubscriberManager(ISubscriberDal subscriberDal, IGenericDal<Subscriber> _genericDal) : base(subscriberDal)
        {
            _subscriberDal = subscriberDal;
        }

        public async Task<bool> TCheckNewsletterInbox(string email)
        {
            return await _subscriberDal.CheckNewsletterInbox(email);
        }
    }
}
