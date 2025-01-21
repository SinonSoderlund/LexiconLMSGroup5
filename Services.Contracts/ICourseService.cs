using LMS.Shared.DTOs.CourseDTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace Services.Contracts
{
    public interface ICourseService
    {
        Task<(IEnumerable<CourseDTO> Courses, int TotalCount)> GetAllCoursesAsync(
            bool includeModules = false, 
            bool includeEnrollments = false, 
            int? pageNr = null,
            int? pageSize = null,
            string? sortBy = null,
            bool isAscending = true,
            string? filteringValue = null);
        Task<CourseDTO> GetCourseByIdAsync(int id, bool includeModules = false, bool includeEnrollments = false);
        Task<CourseDTO> CreateCourseAsync(CourseCreateDTO courseDto);
        Task<bool> UpdateCourseAsync(int id, CourseUpdateDTO courseDto);
        Task<bool> DeleteCourseAsync(int id);

    }
}
