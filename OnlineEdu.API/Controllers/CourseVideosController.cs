using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineEdu.BusniessLayer.Abstract;
using OnlineEdu.DtoLayer.Dtos.CourseVideoDtos;
using OnlineEdu.EntityLayer.Entities;

namespace OnlineEdu.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseVideosController(IGenericService<CourseVideo> _genericService_, IWebHostEnvironment _env, IMapper _mapper) : ControllerBase
    {

        [HttpPost("upload-multiple")]
        public async Task<IActionResult> UploadMultipleVideos(CreateCourseVideoDto createCourseVideoDto)
        {
            if (createCourseVideoDto.Video == null || createCourseVideoDto.Thumbnails == null || createCourseVideoDto.Video.Count != createCourseVideoDto.Thumbnails.Count)
                return BadRequest("Video ve kapak sayıları eşleşmeli.");

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var results = new List<object>();

            for (int i = 0; i < createCourseVideoDto.Video.Count; i++)
            {
                var video = createCourseVideoDto.Video[i];
                var thumbnail = createCourseVideoDto.Thumbnails[i];

                var videoFileName = Guid.NewGuid() + Path.GetExtension(video.FileName);
                var videoPath = Path.Combine(uploadsFolder, videoFileName);
                using (var stream = new FileStream(videoPath, FileMode.Create))
                {
                    await video.CopyToAsync(stream);
                }

                var imageFileName = Guid.NewGuid() + Path.GetExtension(thumbnail.FileName);
                var imagePath = Path.Combine(uploadsFolder, imageFileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await thumbnail.CopyToAsync(stream);
                }

                await _genericService_.TCreateAsync(new CourseVideo
                {
                    CourseId = createCourseVideoDto.CourseId,
                    Video = videoFileName,
                    Thumbnails = imageFileName,
                    VideoNumber = createCourseVideoDto.VideoNumber,
                    Title = createCourseVideoDto.Title
                });
            }

            return Ok(results);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetCourseVideos(int courseId)
        {
            var videos = await _genericService_.TGetFilteredListAsync(x => x.CourseId == courseId);

            var result = videos.Select(v => new ResultCourseVideoDto
            {
                CourseVideoId = v.CourseVideoId,
                CourseId = v.CourseId,
                VideoNumber = v.VideoNumber,
                Title = v.Title,
                Video = $"{Request.Scheme}://{Request.Host}/uploads/{v.Video}",
                Thumbnails = $"{Request.Scheme}://{Request.Host}/uploads/{v.Thumbnails}"
            });

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _genericService_.TGetAllAsync());
        }

    }
}
