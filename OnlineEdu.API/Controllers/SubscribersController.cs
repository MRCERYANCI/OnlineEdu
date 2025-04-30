using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusinessLayer.Abstract;
using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.SubscriberDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController(IGenericService<Subscriber> _genericService, IMapper _mapper, ISubscriberService _subscriberService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> SubscriberGettAll()
        {
            return Ok(_mapper.Map<List<ResultSubscriberDto>>(await _genericService.TGetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdSubscriber(int id)
        {
            return Ok(_mapper.Map<ResultSubscriberDto>(await _genericService.TGetByIdAsync(id)));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateSubscriber(CreateSubscriberDto createSubscriberDto)
        {
            if (await _subscriberService.TCheckNewsletterInbox(createSubscriberDto.Email))
                return BadRequest();

            await _genericService.TCreateAsync(_mapper.Map<Subscriber>(createSubscriberDto));
            return Ok("Subscriber Alanı Başarıyla Eklenmiştir");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubscriber(int id)
        {
            await _genericService.TDeleteAsync(id);
            return Ok("Subscriber Alanı Başarıyla Silinmiştir");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubscriber(UpdateSubscriberDto updateSubscriberDto)
        {
            await _genericService.TUpdateAsync(_mapper.Map<Subscriber>(updateSubscriberDto));
            return Ok("Subscriber Alanı Başarıyla Güncellenmiştir");
        }
    }
}
