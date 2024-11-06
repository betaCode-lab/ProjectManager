using AutoMapper;
using TaskManager.DTOs.Projects;
using TaskManager.Models;
using TaskManager.Repositories.Core.Interfaces;
using TaskManager.Services.Projects.Interfaces;

namespace TaskManager.Services.Projects
{
    public class ProjectService : IProjectService<ProjectDto, ProjectInsertDto, ProjectUpdateDto>
    {
        private readonly IGenericRepository<Project> _projectRepository;
        private readonly IMapper _mapper;

        public ProjectService(IGenericRepository<Project> projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public List<string> Errors { get; set; }

        public IEnumerable<ProjectDto> Get(int idUser)
        {
            try
            {
                var projects = _projectRepository.Search(p => p.IdUser == idUser && p.Active);
                return projects.Select(p => _mapper.Map<ProjectDto>(p));
            }
            catch
            {
                Errors.Add("An error was ocurred while getting projects.");
                return null;
            }
        }

        public ProjectDto GetById(int id, int idUser)
        {
            try
            {
                if(id == 0)
                {
                    Errors.Add("Project identifier invalid.");
                    return null;
                }

                var project = _projectRepository.Search(p => p.Id == id && p.IdUser == idUser).FirstOrDefault();

                if(project == null)
                {
                    Errors.Add("Project not found.");
                    return null;
                }

                var projectDto = _mapper.Map<ProjectDto>(project);

                return projectDto;
            }
            catch
            {
                Errors.Add("An error ocurred while getting project.");
                return null;
            }
        }

        public async Task<ProjectDto> Add(ProjectInsertDto projectInsertDto)
        {
            try
            {
                var project = _mapper.Map<Project>(projectInsertDto);

                await _projectRepository.Add(project);
                await _projectRepository.Save();

                var projectDto = _mapper.Map<ProjectDto>(project);

                return projectDto;
            }
            catch
            {
                Errors.Add("An error ocurred creating a project.");
                return null;
            }
        }

        public async Task<ProjectDto> Update(int id, ProjectUpdateDto projectUpdateDto)
        {
            try
            {
                if (id == 0 || id != projectUpdateDto.Id)
                {
                    Errors.Add("Project identifier invalid.");
                    return null;
                }

                var project = _projectRepository.Search(p => p.Id == id && p.IdUser == projectUpdateDto.IdUser).FirstOrDefault();

                if (project == null)
                {
                    Errors.Add("Project not found.");
                    return null;
                }

                _mapper.Map(projectUpdateDto, project);

                _projectRepository.Update(project);
                await _projectRepository.Save();

                var projectDto = _mapper.Map<ProjectDto>(project);

                return projectDto;
            }
            catch
            {
                Errors.Add("An error ocurred while updating project.");
                return null;
            }
        }

        public async Task<ProjectDto> Delete(int id, int idUser)
        {
            try
            {
                if (id == 0)
                {
                    Errors.Add("Project identifier invalid.");
                    return null;
                }

                var project = _projectRepository.Search(p => p.Id == id && p.IdUser == idUser).FirstOrDefault();

                if (project == null)
                {
                    Errors.Add("Project not found.");
                    return null;
                }

                project.Active = false;

                _projectRepository.Update(project);
                await _projectRepository.Save();

                var projectDto = _mapper.Map<ProjectDto>(project);

                return projectDto;
            }
            catch
            {
                Errors.Add("An error ocurred while deleting project.");
                return null;
            }
        }
    }
}
