using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.DTOs.Tasks;
using TaskManager.Services.Tasks.Interfaces;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IProjectTaskService<ProjectTaskDto, ProjectTaskInsertDto, ProjectTaskUpdateDto> _projectTaskService;

        public TaskController(IProjectTaskService<ProjectTaskDto, ProjectTaskInsertDto, ProjectTaskUpdateDto> projectTaskService)
        {
            _projectTaskService = projectTaskService;
        }

        [HttpGet("{idProject}")]
        [Authorize]
        public ActionResult<IEnumerable<ProjectTaskDto>> Get(int idProject)
        {
            var projectTasksDto = _projectTaskService.Get(idProject);

            if (projectTasksDto == null)
            {
                return BadRequest(_projectTaskService.Errors);
            }

            return Ok(projectTasksDto);
        }

        [HttpGet("{id}/{idProject}")]
        [Authorize]
        public ActionResult<ProjectTaskDto> GetById(int id, int idProject)
        {
            var projectTaskDto = _projectTaskService.GetById(id, idProject);

            if (projectTaskDto == null)
            {
                return BadRequest(_projectTaskService.Errors);
            }

            return Ok(projectTaskDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProjectTaskDto>> Add(ProjectTaskInsertDto projectTaskInsertDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectTaskDto = await _projectTaskService.Add(projectTaskInsertDto);

            if (projectTaskDto == null)
            {
                return BadRequest(_projectTaskService.Errors);
            }

            return Ok(projectTaskDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ProjectTaskDto>> Update(int id, ProjectTaskUpdateDto projectTaskUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectTaskDto = await _projectTaskService.Update(id, projectTaskUpdateDto);

            if (projectTaskDto == null)
            {
                return BadRequest(_projectTaskService.Errors);
            }

            return Ok(projectTaskDto);
        }

        [HttpDelete("{id}/{idProject}")]
        [Authorize]
        public async Task<ActionResult<ProjectTaskDto>> Delete(int id, int idProject)
        {
            var projectTaskDto = await _projectTaskService.Delete(id, idProject);

            if (projectTaskDto == null)
            {
                return BadRequest(_projectTaskService.Errors);
            }

            return Ok(projectTaskDto);
        }
    }
}
