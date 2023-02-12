using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResault<T>(CustomResponseDto<T> responce)
        {
            if (responce.StatusCode == 204)
            {
                return new ObjectResult(null)
                {
                    StatusCode = responce.StatusCode
                };
            }
            return new ObjectResult(responce)
            {
                StatusCode = responce.StatusCode
            };

        }
    }
}
