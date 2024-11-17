using CafeService.Api.Entities;
using CafeService.Api.Models;
using CafeService.Api.Services;
using Microsoft.AspNetCore.Mvc;
using static CafeService.Api.Enums.CommonResources;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CafeService.Api.V1.Controllers
{
    [ApiController]
    public class EmployeeController : Controller
    {
        private const string API_ROUTE_NAME = "/api";
        private readonly IEmployeeService _iEmployeeService;
        public EmployeeController(IEmployeeService iEmployeeService)
        {
            _iEmployeeService = iEmployeeService;
        }

        [HttpGet]
        [Route(API_ROUTE_NAME + "/employees")]
        public async Task<IActionResult> GetAllCafes([FromQuery] int page = 1, [FromQuery] int items_per_page = 10, [FromQuery] string cafe = null)
        {
            var result = await _iEmployeeService.GetAllEmployees(cafe);



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
                    Url = $"/?page={page - 1}&items_per_page={items_per_page}&search={cafe}"
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
                    Url = $"/?page={i}&items_per_page={items_per_page}&search={cafe}"
                });
            }
            if (page < totalPages)
            {
                paginationButtons.Add(new Arrylsit
                {
                    Page = page + 1,
                    Active = false,
                    Label = "Next &raquo;",
                    Url = $"/?page={page + 1}&items_per_page={items_per_page}&search={cafe}"
                });
            }

            var paginationInfo = new PaginationInfo
            {
                First_page_url = $"/?page=1&items_per_page={items_per_page}&search={cafe}",
                Page = page,
                From = start + 1,
                To = Math.Min(start + items_per_page, totalItems),
                Last_page = totalPages,
                Items_per_page = items_per_page,
                Prev_page_url = page > 1 ? $"/?page={page - 1}&items_per_page={items_per_page}&search={cafe}" : null,
                Next_page_url = page < totalPages ? $"/?page={page + 1}&items_per_page={items_per_page}&search={cafe}" : null,
                Total = totalItems,
                Links = paginationButtons
            };

            var payload = new Payload
            {
                Pagination = paginationInfo,
            };

            var response = new DataResponse<List<EmployeeViewModel>>
            {
                Data = paginatedData,
                Payload = payload
            };

            return Ok(response);
        }

        [HttpGet]
        [Route(API_ROUTE_NAME + "/employees/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var result = await _iEmployeeService.GetEmployeesById(id);
                if (result == null)
                {
                    return Conflict("Invalid employee!");
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Access error!");
            }
        }



        [HttpPost]
        [Route(API_ROUTE_NAME + "/employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Insert([FromForm] EmployeeInsertModel model)
        {
            try
            {
                if ((await _iEmployeeService.GetEmployeesById(model.Id))!=null)
                {
                    return Conflict("Employee already exists!");
                }

                var result = await _iEmployeeService.Insert(new Employee
                {
                    Id = model.Id,
                    Name = model.Name,
                    EmailAddress = model.EmailAddress,
                    PhoneNumber = model.PhoneNumber,
                    Gender = model.Gender,
                    StartDate = model.StartDate,
                    FK_CafeId = model.CafeId,
                    CreatedDateTime = DateTime.UtcNow,
                    ModifiedDateTime = DateTime.UtcNow
                });
                return result != null ? Ok("Successfully inserted!") : StatusCode(500, "Insert error!");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Insert error!");
            }
        }

        [HttpPut]
        [Route(API_ROUTE_NAME + "/employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromForm]  EmployeeEditModel model)
        {
            try
            {
                var employeeObject = await _iEmployeeService.GetEmployeesById(model.Id);
                if (employeeObject == null)
                {
                    return Conflict("Invalid employee!");
                }               

                employeeObject.Name = model.Name!= null?model.Name: employeeObject.Name;
                employeeObject.EmailAddress = model.EmailAddress != null ? model.EmailAddress: employeeObject.EmailAddress;
                employeeObject.PhoneNumber = model.PhoneNumber != null ? model.PhoneNumber : employeeObject.PhoneNumber;
                employeeObject.Gender = model.Gender != null ? (Gender)model.Gender : employeeObject.Gender;
                employeeObject.StartDate = model.StartDate;
                employeeObject.FK_CafeId = model.CafeId;
                employeeObject.ModifiedDateTime = DateTime.UtcNow;

                var result = await _iEmployeeService.Update(employeeObject);
                return result? Ok("Successfully updated!") : StatusCode(500, "Update error!");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Update error!");
            }
        }

        [HttpDelete]
        [Route(API_ROUTE_NAME + "/employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(EmployeeDeleteModel model)
        {
            try
            {
                var employeeObject = await _iEmployeeService.GetEmployeesById(model.Id);
                if (employeeObject == null)
                {
                    return Conflict("Invalid employee!");
                }                

                var result = await _iEmployeeService.Delete(employeeObject);
                return result ? Ok("Successfully deleted!") : StatusCode(500, "Delete error!");

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Delete error!");
            }
        }
    }
}
