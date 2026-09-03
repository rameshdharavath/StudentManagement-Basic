using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.DTOs;
using StudentManagement.Api.Services;

namespace StudentManagement.Api.Controllers;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _service;

    public StudentsController(IStudentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetStudents(
        string? search = null, int pageNumber = 1, int pageSize = 10)
    {
        return Ok(await _service.GetStudentsAsync(search, pageNumber, pageSize));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetStudent(int id)
    {
        var student = await _service.GetByIdAsync(id);
        return student == null ? NotFound() : Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStudent(CreateStudentDto request)
    {
        try
        {
            var student = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateStudent(
        int id, UpdateStudentDto request)
    {
        try
        {
            var updated = await _service.UpdateAsync(id, request);
            if (!updated) return NotFound();
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
