using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.ContactDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController(IGenericService<Contact> _genericService,IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> ContactGettAll()
        {
            return Ok(_mapper.Map<List<ResultContactDto>>(await _genericService.TGetAllAsync()));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdContact(int id)
        {
            return Ok(_mapper.Map<ResultContactDto>(await _genericService.TGetByIdAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDto createContactDto)
        {
            await _genericService.TCreateAsync(_mapper.Map<Contact>(createContactDto));
            return Ok("Contact Alanı Başarıyla Eklenmiştir");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            await _genericService.TDeleteAsync(id);
            return Ok("Contact Alanı Başarıyla Silinmiştir");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContact(UpdateContactDto updateContactDto)
        {
            await _genericService.TUpdateAsync(_mapper.Map<Contact>(updateContactDto));
            return Ok("Contact Alanı Başarıyla Güncellenmiştir");
        }
    }
}
