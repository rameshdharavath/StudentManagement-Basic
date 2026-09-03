using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Data;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Models;

namespace StudentManagement.Api.Services;

public class StudentService : IStudentService
{
    private readonly StudentDbContext _context;

    public StudentService(StudentDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<StudentDto>> GetStudentsAsync(
        string? search, int pageNumber, int pageSize)
    {
        pageNumber = Math.Max(pageNumber, 1);
        pageSize = pageSize is < 1 or > 100 ? 10 : pageSize;

        IQueryable<Student> query = _context.Students.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();

            query = query.Where(x =>
                x.FirstName.Contains(search) ||
                x.LastName.Contains(search) ||
                x.Email.Contains(search) ||
                (x.Course != null && x.Course.Contains(search)) ||
                (x.Department != null && x.Department.Contains(search)));
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(x => x.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new StudentDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Phone = x.Phone,
                DateOfBirth = x.DateOfBirth,
                Gender = x.Gender,
                Course = x.Course,
                Department = x.Department,
                Address = x.Address
            })
            .ToListAsync();

        return new PagedResult<StudentDto>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<StudentDto?> GetByIdAsync(int id)
    {
        return await _context.Students
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new StudentDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Phone = x.Phone,
                DateOfBirth = x.DateOfBirth,
                Gender = x.Gender,
                Course = x.Course,
                Department = x.Department,
                Address = x.Address
            })
            .FirstOrDefaultAsync();
    }

    public async Task<StudentDto> CreateAsync(CreateStudentDto request)
    {
        if (await _context.Students.AnyAsync(x => x.Email == request.Email))
            throw new InvalidOperationException("Email already exists.");

        var student = new Student
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Course = request.Course,
            Department = request.Department,
            Address = request.Address,
            CreatedDate = DateTime.UtcNow
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        return new StudentDto
        {
            Id = student.Id,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            Phone = student.Phone,
            DateOfBirth = student.DateOfBirth,
            Gender = student.Gender,
            Course = student.Course,
            Department = student.Department,
            Address = student.Address
        };
    }

    public async Task<bool> UpdateAsync(int id, UpdateStudentDto request)
    {
        var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
        if (student == null) return false;

        if (await _context.Students.AnyAsync(x => x.Email == request.Email && x.Id != id))
            throw new InvalidOperationException("Email already exists.");

        student.FirstName = request.FirstName;
        student.LastName = request.LastName;
        student.Email = request.Email;
        student.Phone = request.Phone;
        student.DateOfBirth = request.DateOfBirth;
        student.Gender = request.Gender;
        student.Course = request.Course;
        student.Department = request.Department;
        student.Address = request.Address;
        student.ModifiedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var student = await _context.Students.FirstOrDefaultAsync(x => x.Id == id);
        if (student == null) return false;

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return true;
    }
}
