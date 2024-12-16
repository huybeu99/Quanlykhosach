using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoSach.DTO;
using QuanLyKhoSach.Interface.Service;
using QuanLyKhoSach.Services;

namespace QuanLyKhoSach.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController:BaseController
    {
        private readonly IPublisherService _publisherservice;
        private readonly IMapper _mapper;
        public PublisherController(IPublisherService publisherservice, IMapper mapper)
        {
            _publisherservice = publisherservice;
            _mapper = mapper;
        }
        [HttpGet("allpublisher")]
        public async Task<ActionResult<IEnumerable<PublisherDTO>>> GetAllPublisher()
        {
            try
            {
                var publishers = await _publisherservice.GetPublisherAsync();
                return Ok(publishers);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherDTO>> GetPublisherByID(int id)
        {
            try
            {
                var publisher = await _publisherservice.GetPublisherByIDAsync(id);
                return Ok(publisher);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PublisherDTO>> CreatePublisher([FromBody] PublisherDTO publisherDTO)
        {
            try
            {
                var createpublisher = await _publisherservice.AddPublisherAsync(publisherDTO);
                return CreatedAtAction(
                    nameof(GetPublisherByID),
                    new { id = createpublisher.Publisher_ID }, createpublisher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PublisherDTO>> UpdatePublisher([FromBody] PublisherDTO publisherDTO, int id)
        {
            if (id != publisherDTO.Publisher_ID)
            {
                return BadRequest(new { message = "ID không khớp" });
            }
            try
            {
                var updatepublisher = await _publisherservice.UpdatePublisherAsync(publisherDTO);
                return Ok(updatepublisher);
            }
            catch (NotFoundException)
            {
                return NotFound(new { message = "Không tìm thấy nhà sản xuất" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi cập nhật nhà sản xuất", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PublisherDTO>> DeletePublisher(int id)
        {
            try
            {
                await _publisherservice.DeletePublisherAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi xóa nhà sản xuất ", error = ex.Message });
            }
        }
    }
}
