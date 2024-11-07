using AutoMapper;
using TaskManager.DTOs.Tasks;
using TaskManager.Models;
using TaskManager.Repositories.Core.Interfaces;
using TaskManager.Services.Tasks.Interfaces;

namespace TaskManager.Services.Tasks
{
    public class ProjectTaskService : IProjectTaskService<ProjectTaskDto, ProjectTaskInsertDto, ProjectTaskUpdateDto>
    {
        private readonly IGenericRepository<ProjectTask> _projectTaskRepository;
        private readonly IMapper _mapper;

        public ProjectTaskService(IGenericRepository<ProjectTask> projectTaskRepository, IMapper mapper)
        {
            _projectTaskRepository = projectTaskRepository;
            _mapper = mapper;

            Errors = new List<string>();
        }

        public List<string> Errors { get; set; }

        public IEnumerable<ProjectTaskDto> Get(int idProject)
        {
            try
            {
                var projectTasks = _projectTaskRepository.Search(t => t.IdProject == idProject);
                var projectTaskDto = projectTasks.Select(t => _mapper.Map<ProjectTask, ProjectTaskDto>(t)).ToList();

                return projectTaskDto;
            }
            catch
            {
                Errors.Add("An error ocurred while getting tasks.");
                return null!;
            }
        }

        public ProjectTaskDto GetById(int id, int idProject)
        {
            try
            {
                var projectTask = _projectTaskRepository.Search(t => t.Id == id && t.IdProject == idProject).FirstOrDefault();

                if (projectTask == null)
                {
                    Errors.Add("Task not found.");
                    return null!;
                }

                var projectTaskDto = _mapper.Map<ProjectTask, ProjectTaskDto>(projectTask);

                return projectTaskDto;
            }
            catch
            {
                Errors.Add("An error ocurred while getting task.");
                return null!;
            }
        }

        public async Task<ProjectTaskDto> Add(ProjectTaskInsertDto projectTaskInsertDto)
        {
            try
            {
                var projectTask = _mapper.Map<ProjectTaskInsertDto, ProjectTask>(projectTaskInsertDto);

                await _projectTaskRepository.Add(projectTask);
                await _projectTaskRepository.Save();

                var projectTaskDto = _mapper.Map<ProjectTask, ProjectTaskDto>(projectTask);

                return projectTaskDto;
            }
            catch
            {
                Errors.Add("An error ocurred while adding task.");
                return null!;
            }
        }

        public async Task<ProjectTaskDto> Update(int id, ProjectTaskUpdateDto projectTaskUpdateDto)
        {
            try
            {
                if (id == 0 || id != projectTaskUpdateDto.Id)
                {
                    Errors.Add("Task identifier invalid.");
                    return null!;
                }

                var projectTask = _projectTaskRepository.Search(p => p.Id == id && p.IdProject == projectTaskUpdateDto.IdProject).FirstOrDefault();

                if (projectTask == null)
                {
                    Errors.Add("Task not found.");
                    return null!;
                }

                _mapper.Map(projectTaskUpdateDto, projectTask);

                _projectTaskRepository.Update(projectTask);
                await _projectTaskRepository.Save();

                var projectTaskDto = _mapper.Map<ProjectTask, ProjectTaskDto>(projectTask);

                return projectTaskDto;
            }
            catch
            {
                Errors.Add("An error ocurred while updating task.");
                return null!;
            }
        }

        public async Task<ProjectTaskDto> Delete(int id, int idProject)
        {
            try
            {
                if (id == 0)
                {
                    Errors.Add("Task identifier invalid.");
                    return null!;
                }

                var projectTask = _projectTaskRepository.Search(p => p.Id == id && p.IdProject == idProject).FirstOrDefault();

                if (projectTask == null)
                {
                    Errors.Add("Task not found.");
                    return null!;
                }

                _projectTaskRepository.Delete(projectTask);
                await _projectTaskRepository.Save();

                var projectTaskDto = _mapper.Map<ProjectTask, ProjectTaskDto>(projectTask);

                return projectTaskDto;
            }
            catch
            {
                Errors.Add("An error ocurred deleting task.");
                return null!;
            }
        }
    }
}
