using AutoMapper;
using Employee_Management_System.Data;
using Employee_Management_System.Models;
using Employee_Management_System.Models.Dto;
using Employee_Management_System.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Employee_Management_System.Controllers
{
    [Route("api/Department")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _dbDepartment;
        private readonly IMapper _mapper;
        protected ApiResponse _response;
        public DepartmentController(IDepartmentRepository dbDepartment, IMapper mapper)
        {
            _dbDepartment = dbDepartment;
            _mapper = mapper;
            _response = new();

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ApiResponse>> GetDepartments()
        {
            try
            {
                IEnumerable<Department> departments = await _dbDepartment.GetAllAsync();
                _response.Result = _mapper.Map<List<DepartmentDTO>>(departments);
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

        [HttpGet("{id:int}", Name = "GetDepartment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ApiResponse>> GetDepartment(int id)
        {
            try
            {
                if (id == 0)
                {

                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);

                }

                var department = await _dbDepartment.GetAsync(u => u.Id == id);
                if (department == null)
                {

                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);


                }

                _response.Result = _mapper.Map<DepartmentDTO>(department);
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
        public async Task<ActionResult<ApiResponse>> CreateDepartments([FromBody] DepartmentCreateDTO departmentCreateDTO)
        {
            try
            {
                if (await _dbDepartment.GetAsync(u => u.DepartCode.ToLower() == departmentCreateDTO.DepartCode.ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Department already exists!");
                    return BadRequest(ModelState);
                }
                if (departmentCreateDTO == null)
                {
                    return BadRequest();
                }

                Department department = _mapper.Map<Department>(departmentCreateDTO);
                await _dbDepartment.CreateAsync(department);
                _response.Result = _mapper.Map<UserDTO>(department);
                _response.StatusCode = HttpStatusCode.OK;
                return CreatedAtRoute("GetDepartment", new { id = department.Id }, _response);
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateDepartments")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiResponse>> UpdateDepartment(int id, [FromBody] DepartmentUpdateDTO departmentUpdateDTO)
        {
            try
            {
                if (departmentUpdateDTO == null || id != departmentUpdateDTO.Id)
                {
                    return BadRequest();
                }

                Department model = _mapper.Map<Department>(departmentUpdateDTO);



                await _dbDepartment.UpdateAsync(model);
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

        [HttpDelete("{id:int}", Name = "DeleteDepartment")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponse>> DeleteDepartments(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var department = await _dbDepartment.GetAsync(u => u.Id == id);
                if (department == null)
                {
                    return NotFound();
                }

                await _dbDepartment.RemoveAsync(department);
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


