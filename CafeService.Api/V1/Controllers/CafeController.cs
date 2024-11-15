using CafeService.Api.Services;
using CafeService.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using CafeService.Api.Entities;
using static CafeService.Api.Enums.CommonResources;

namespace CafeService.Api.V1.Controllers
{
    [ApiController]
    public class CafeController : Controller
    {
        private const string API_ROUTE_NAME = "/api";
        private readonly ICafeService _iCafeService;
        public CafeController(ICafeService iCafeService)
        {
            _iCafeService = iCafeService;
        }

        [HttpGet]
        [Route(API_ROUTE_NAME + "/cafes")]
        public async Task<IActionResult> GetAllCafes([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string? location = null)
        {
            var result = await _iCafeService.GetAllCafes(location);



            // Calculate pagination information
            int totalItems = result.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / items_per_page);
            int start = (page - 1) * items_per_page;

            // Retrieve paginated data
            var paginatedData = result.Skip(start).Take(items_per_page).ToList();

            // Construct pagination buttons
            var paginationButtons = new List<Arrylsit>();
            if (page > 1)
            {
                paginationButtons.Add(new Arrylsit
                {
                    Page = page - 1,
                    Active = false,
                    Label = "&laquo; Previous",
                    Url = $"/?page={page - 1}&items_per_page={items_per_page}&search={location}"
                });
            }

            // Calculate the range of pages to display, limiting to 4 pages
            int startPage = Math.Max(1, page - 2);
            int endPage = Math.Min(startPage + 3, totalPages);

            for (int i = startPage; i <= endPage; i++)
            {
                paginationButtons.Add(new Arrylsit
                {
                    Page = i,
                    Active = i == page,
                    Label = i.ToString(),
                    Url = $"/?page={i}&items_per_page={items_per_page}&search={location}"
                });
            }
            if (page < totalPages)
            {
                paginationButtons.Add(new Arrylsit
                {
                    Page = page + 1,
                    Active = false,
                    Label = "Next &raquo;",
                    Url = $"/?page={page + 1}&items_per_page={items_per_page}&search={location}"
                });
            }

            var paginationInfo = new PaginationInfo
            {
                First_page_url = $"/?page=1&items_per_page={items_per_page}&search={location}",
                Page = page,
                From = start + 1,
                To = Math.Min(start + items_per_page, totalItems),
                Last_page = totalPages,
                Items_per_page = items_per_page,
                Prev_page_url = page > 1 ? $"/?page={page - 1}&items_per_page={items_per_page}&search={location}" : null,
                Next_page_url = page < totalPages ? $"/?page={page + 1}&items_per_page={items_per_page}&search={location}" : null,
                Total = totalItems,
                Links = paginationButtons
            };

            var payload = new Payload
            {
                Pagination = paginationInfo,
            };

            var response = new DataResponse<List<CafeViewModel>>
            {
                Data = paginatedData,
                Payload = payload
            };

            return Ok(response);
        }


        [HttpPost]
        [Route(API_ROUTE_NAME + "/cafe")]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]      
        public async Task<IActionResult> Insert(CafeInsertModel model)
        {
            try
            {
                if ((await _iCafeService.GetCafeByName(model.Name)).Count>0)
                {
                    return Conflict("Café already exists!");                   
                }
               
                var result = await _iCafeService.Insert(new Cafe
                {
                    Name = model.Name,
                    Description=model.Description,
                    Location = model.Location,
                    CreatedDateTime= DateTime.UtcNow,
                    ModifiedDateTime = DateTime.UtcNow,
                    Logo= null
                });
                return result!=null?Ok("Successfully inserted!"): StatusCode(500,"Insert error!");

            }
            catch (Exception)
            {
                return StatusCode(500, "Insert error!");
            }
        }

        [HttpPut]
        [Route(API_ROUTE_NAME + "/cafe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(CafeEditModel model)
        {
            try
            {
                var cafeObject = await _iCafeService.GetCafeById(model.Id);
                if (cafeObject == null)
                {
                    return Conflict("Invalid cafe!");
                }

                cafeObject.Name = model.Name != null ? model.Name : cafeObject.Name;
                cafeObject.Description = model.Description != null ? model.Description : cafeObject.Description;
                cafeObject.Location = model.Location != null ? model.Location : cafeObject.Location;
                cafeObject.Logo = model.Logo != null ? model.Logo : cafeObject.Logo;               
                cafeObject.ModifiedDateTime = DateTime.UtcNow;

                var result = await _iCafeService.Update(cafeObject);
                return result ? Ok("Successfully updated!") : StatusCode(500, "Update error!");

            }
            catch (Exception)
            {
                return StatusCode(500, "Update error!");
            }
        }

        [HttpDelete]
        [Route(API_ROUTE_NAME + "/cafe")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(CafeDeleteModel model)
        {
            try
            {
                var cafeObject = await _iCafeService.GetCafeById(model.Id);
                if (cafeObject == null)
                {
                    return Conflict("Invalid cafe!");
                }

                var result = await _iCafeService.Delete(cafeObject);
                return result ? Ok("Successfully deleted!") : StatusCode(500, "Delete error!");

            }
            catch (Exception)
            {
                return StatusCode(500, "Delete error!");
            }
        }

    }


}
