using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.DTOs.Projects;
using TaskManager.Services.Projects.Interfaces;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService<ProjectDto, ProjectInsertDto, ProjectUpdateDto> _projectService;

        public ProjectController(IProjectService<ProjectDto, ProjectInsertDto, ProjectUpdateDto> projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<ProjectDto>> Get()
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if (userId == 0)
            {
                return BadRequest("User not valid.");
            }

            var projects = _projectService.Get(userId);

            if (projects == null)
            {
                return BadRequest(_projectService.Errors);
            }

            return Ok(projects);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ProjectDto>> Add(ProjectInsertDto projectInsertDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectDto = await _projectService.Add(projectInsertDto);

            if(projectDto == null)
            {
                return BadRequest(_projectService.Errors);
            }

            return Ok(projectDto);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ProjectDto>> Update(int id, ProjectUpdateDto projectUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var projectDto = await _projectService.Update(id, projectUpdateDto);

            if (projectDto == null)
            {
                return BadRequest(_projectService.Errors);
            }

            return Ok(projectDto);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<ProjectDto>> Delete(int id)
        {
            int userId = Convert.ToInt32(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var projectDto = await _projectService.Delete(id, userId);

            if(projectDto == null)
            {
                return BadRequest(_projectService.Errors);
            }

            return Ok(projectDto);
        }
    }
}
