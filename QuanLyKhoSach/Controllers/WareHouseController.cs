using Microsoft.AspNetCore.Mvc;
using QuanLyKhoSach.DTO;
using QuanLyKhoSach.Interface.Service;

namespace QuanLyKhoSach.Controllers
{
    [ApiController]
    [Route("api / [controller]")]
    public class WareHouseController:BaseController
    {
        private readonly IWareHouseService _warehouseservice;
        public WareHouseController(IWareHouseService warehouseservice)
        {
            _warehouseservice= warehouseservice;
        }
        [HttpGet("allstore")]
        public async Task<ActionResult<IEnumerable<WareHouseDTO>>> GetAllWareHouse()
        {
            try
            {
                var warehouses = await _warehouseservice.GetWareHouseAsync();
                return Ok(warehouses);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<WareHouseDTO>> GetWareHouseByID(int id)
        {
            try
            {
                var warehouse = await _warehouseservice.GetWareHouseByIDAsync(id);
                return Ok(warehouse);
            }
            catch (Exception ex) { 
            return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<WareHouseDTO>> CreateWareHouse([FromBody] WareHouseDTO warehousedto)
        {
            try
            {
                var createwarehouse = await _warehouseservice.AddWareHouseAsync(warehousedto);
                return CreatedAtAction(
                    nameof(GetWareHouseByID),
                    new { id = createwarehouse.WareHouse_ID }, createwarehouse);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut ("{id}")]
        public async Task<ActionResult<WareHouseDTO>> UpdateWareHouse([FromBody]WareHouseDTO warehousedto, int id)
        {
            if (id != warehousedto.WareHouse_ID)
            {
                return BadRequest(new { message = "ID không khớp" });
            }
            try
            {
                var updatewarehouse = await _warehouseservice.UpdateWareHouseAsync(warehousedto);
                return Ok(updatewarehouse);
            }
            catch (NotFoundException)
            {
                return NotFound(new { message = "Không tìm thấy kho hàng" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi cập nhật kho hàng", error = ex.Message });
            }
        }

        [HttpDelete ("{id}")]
        public async Task<ActionResult<WareHouseDTO>> DeleteWareHouse(int id)
        {
            try
            {
                await _warehouseservice.DeleteWareHouseAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi xóa cửa hàng", error = ex.Message });
            }
        }
    }
}
