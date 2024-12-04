using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoSach.DTO;
using QuanLyKhoSach.Interface.Service;
using QuanLyKhoSach.Services;

namespace QuanLyKhoSach.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : BaseController
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }
        [HttpGet("allstore")]
        public async Task<ActionResult<IEnumerable<StoreDTO>>> GetAllStore()
        {
            try
            {
                var store = await _storeService.GetStoreAsync();
                return Ok(store);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreDTO>> GetStoreByID(int id)
        {
            try
            {
                var store = await _storeService.GetStoreByIDAsync(id);
                return Ok(store);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<StoreDTO>> CreateStore([FromBody] StoreDTO storedto)
        {
            try
            {
                var createstore = await _storeService.AddStoreAsync(storedto);
                return CreatedAtAction(
                    nameof(GetAllStore),
                    new { id = createstore.Store_Id },
                    createstore);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StoreDTO>> UpdateStore([FromBody] StoreDTO storedto, int id)
        {
            if (id != storedto.Store_Id)
            {
                return BadRequest(new { message = "ID không khớp" });
            }
            try
            {
                var updatebook = await _storeService.UpdateStoreAsync(storedto);
                return Ok(storedto);
            }
            catch (NotFoundException)
            {
                return NotFound(new { message = "Không tìm thấy cửa hàng" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi cập nhật cửa hàng", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StoreDTO>> DeleteStore(int id)
        {
            try
            {
                await _storeService.DeleteStoreAsync(id);
                return NoContent();
            }
            catch (DirectoryNotFoundException)
            {
                return NotFound(new { message = "Không tìm thấy cửa hàng" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi xóa cửa hàng", error = ex.Message });
            }
        }
    }
}
