using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace QuanLyKhoSach.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
             //<summary>
             //Trả về response thành công
             //</summary>
             //<param name = "data" > Dữ liệu trả về</param>
             //<returns>IActionResult</returns>
            protected IActionResult Success(object data = null)
            {
                return Ok(new
                {
                    success = true,
                    data = data
                });
            }

            /// <summary>
            /// Xử lý và trả về response lỗi
            /// </summary>
            /// <param name="exception">Ngoại lệ xảy ra</param>
            /// <returns>IActionResult</returns>
            protected IActionResult HandleException(Exception exception)
            {
                return exception switch
                {
                    NotFoundException => NotFound(new
                    {
                        success = false,
                        message = exception.Message
                    }),
                    ArgumentException => BadRequest(new
                    {
                        success = false,
                        message = exception.Message
                    }),
                    _ => StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        success = false,
                        message = "Đã xảy ra lỗi không mong muốn",
                        errorDetails = exception.Message
                    })
                };
            }

            /// <summary>
            /// Trả về response lỗi tùy chỉnh
            /// </summary>
            /// <param name="message">Thông báo lỗi</param>
            /// <param name="statusCode">Mã trạng thái HTTP</param>
            /// <returns>IActionResult</returns>
            protected IActionResult Error(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
            {
                return StatusCode((int)statusCode, new
                {
                    success = false,
                    message = message
                });
            }

            /// <summary>
            /// Validate dữ liệu đầu vào
            /// </summary>
            /// <returns>True nếu dữ liệu hợp lệ, ngược lại False</returns>
            protected bool ValidateModel()
            {
                if (!ModelState.IsValid)
                {
                    throw new ArgumentException(
                        string.Join(", ",
                            ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                        )
                    );
                }
                return true;
            }
        }
}