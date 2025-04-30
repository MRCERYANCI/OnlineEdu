using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.MessageDtos;
using OnlineEdu.EntityLayer.Entities;
using System.Net;

namespace OnlineEdu.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController(IGenericService<Message> _genericService,IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> MessageGettAll()
        {
            return Ok(_mapper.Map<List<ResultMessageDto>>(await _genericService.TGetAllAsync()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdMessage(int id)
        {
            return Ok(_mapper.Map<ResultMessageDto>(await _genericService.TGetByIdAsync(id)));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageDto createMessageDto)
        {
            createMessageDto.IPAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

            await _genericService.TCreateAsync(_mapper.Map<Message>(createMessageDto));
            return Ok(new { Message = "Mesaj gönderildi", ClientIP = createMessageDto.IPAddress });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            await _genericService.TDeleteAsync(id);
            return Ok("Message Alanı Başarıyla Silinmiştir");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            await _genericService.TUpdateAsync(_mapper.Map<Message>(updateMessageDto));
            return Ok("Message Alanı Başarıyla Güncellenmiştir");
        }
    }
}
