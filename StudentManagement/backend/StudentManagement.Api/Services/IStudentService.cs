using StudentManagement.Api.DTOs;

namespace StudentManagement.Api.Services;

public interface IStudentService
{
    Task<PagedResult<StudentDto>> GetStudentsAsync(string? search, int pageNumber, int pageSize);
    Task<StudentDto?> GetByIdAsync(int id);
    Task<StudentDto> CreateAsync(CreateStudentDto request);
    Task<bool> UpdateAsync(int id, UpdateStudentDto request);
    Task<bool> DeleteAsync(int id);
}
