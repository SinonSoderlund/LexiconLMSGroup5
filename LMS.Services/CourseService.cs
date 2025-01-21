using AutoMapper;
using Domain.Contracts;
using Domain.Models.Entities;
using LMS.Shared.DTOs.CourseDTOs;
using LMS.Shared.DTOs.ModuleDTOs;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using System.Linq.Expressions;

namespace LMS.Services
{
    public class CourseService : ICourseService
    {

        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CourseService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<(IEnumerable<CourseDTO> Courses, int TotalCount)> GetAllCoursesAsync(
            bool includeModules = false,
            bool includeEnrollments = false,
            int? pageNr = null,
            int? pageSize = null,
            string? sortBy = null,
            bool isAscending = true,
            string? filteringValue = null)
        {
            Expression<Func<Course, bool>> filter = c =>
               (string.IsNullOrEmpty(filteringValue) || c.Name.Contains(filteringValue));
            
            IQueryable<Course> query = _uow.Courses.Query();

            if (includeModules) query = query.Include(m => m.Modules);

            if (includeEnrollments) query = query.Include(e => e.Enrollments);

            var (courses, totalCount) = await _uow.Courses.GetFilteredAndSortedEntitiesAsync(
                filter: filter,
                sortBy: sortBy,
                isAscending: isAscending,
                pageNr: pageNr,
                pageSize: pageSize
            );

            var courseDTOs = _mapper.Map<IEnumerable<CourseDTO>>(courses);

            return (courseDTOs, totalCount);
        }

        public async Task<CourseDTO> GetCourseByIdAsync(int id, bool includeModules = false, bool includeEnrollments = false)
        {
            IQueryable<Course> query = _uow.Courses.Query().Where(c => c.CourseId == id);

            query = IncludeRelatedEntities(query, includeModules, includeEnrollments);

            var course = await query.FirstOrDefaultAsync();

            if (course == null) return null;

            return _mapper.Map<CourseDTO>(course);
        }

        public async Task<CourseDTO> CreateCourseAsync(CourseCreateDTO courseDto)
        {
            ValidateCourseDates(courseDto);

            var courseToAdd = _mapper.Map<Course>(courseDto);
            await _uow.Courses.AddAsync(courseToAdd);

            await _uow.CompleteASync();

            return _mapper.Map<CourseDTO>(courseToAdd);
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await GetCourseIfExists(id);

            await _uow.Courses.DeleteAsync(course);
            await _uow.CompleteASync();
            return true;
        }

        public async Task<bool> UpdateCourseAsync(int id, CourseUpdateDTO courseDto)
        {
            ValidateCourseDates(courseDto);
            var existingCourse = await GetCourseIfExists(id);

            _mapper.Map(courseDto, existingCourse);

            await _uow.CompleteASync();
            return true;
        }


        private IQueryable<Course> IncludeRelatedEntities(
        IQueryable<Course> query, 
        bool includeModules, 
        bool includeEnrollments)
        {
            if (includeModules) query = query.Include(m => m.Modules);

            if (includeEnrollments) query = query.Include(e => e.Enrollments);

            return query;
        }

        private void ValidateCourseDates(dynamic courseDto)
        {
            if (courseDto.EndDate < courseDto.StartDate)
            {
                throw new ArgumentException("The course cannot end before the start date.");
            }
        }

        private async Task<Course> GetCourseIfExists(int id)
        {
            var existingCourse = await _uow.Courses.GetByIdAsync(id);
            if (existingCourse == null)
            {
                throw new KeyNotFoundException($"Course with ID {id} not found.");
            }
            return existingCourse;
        }


    }
}
