using AutoMapper;
using Employee_Management_System.Data;
using Employee_Management_System.Models;
using Employee_Management_System.Models.Dto;
using Employee_Management_System.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Employee_Management_System.Controllers
{
    [Route("api/Employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _dbEmployee;
        private readonly IMapper _mapper;
        protected ApiResponse _response;
        public EmployeeController(IEmployeeRepository dbEmployee, IMapper mapper)
        {
            _dbEmployee = dbEmployee;
            _mapper = mapper;
            _response = new();

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> GetEmployees()
        {
            try
            {
                IEnumerable<Employee> employees = await _dbEmployee.GetAllAsync();
                _response.Result = _mapper.Map<List<EmployeeDTO>>(employees);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { e.ToString() };
            }
            return _response;

        }

        [HttpGet("{id:int}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ApiResponse>> GetEmployee(int id)
        {
            try
            {
                if (id == 0)
                {

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);

                }

                var employee = await _dbEmployee.GetAsync(u => u.Id == id);
                if (employee == null)
                {

                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);


                }

                _response.Result = _mapper.Map<EmployeeDTO>(employee);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse>> CreateEmployees([FromBody] EmployeeDTO employeeCreateDTO)
        {
            try
            {
                if (await _dbEmployee.GetAsync(u => u.Name.ToLower() == employeeCreateDTO.Name.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Employee already exists!");
                    return BadRequest(ModelState);
                }
                if (employeeCreateDTO == null)
                {
                    return BadRequest();
                }

                Employee employee = _mapper.Map<Employee>(employeeCreateDTO);
                await _dbEmployee.CreateAsync(employee);
                _response.Result = _mapper.Map<EmployeeDTO>(employee);
                _response.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetEmployee", new { id = employee.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateEmployees")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdateEmployee(int id, [FromBody] EmployeeUpdateDTO employeeUpdateDTO)
        {
            try
            {
                if (employeeUpdateDTO == null || id != employeeUpdateDTO.Id)
                {
                    return BadRequest();
                }

                Employee model = _mapper.Map<Employee>(employeeUpdateDTO);



                await _dbEmployee.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }

        [HttpDelete("{id:int}", Name = "DeleteEmployee")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> DeleteEmployees(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var employee = await _dbEmployee.GetAsync(u => u.Id == id);
                if (employee == null)
                {
                    return NotFound();
                }

                await _dbEmployee.RemoveAsync(employee);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;

        }

    }
}
