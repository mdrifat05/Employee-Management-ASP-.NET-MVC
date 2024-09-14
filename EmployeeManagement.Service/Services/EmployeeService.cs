using EmployeeManagement.Repository.Contracts;
using EmployeeManagement.Repository.Entities;
using EmployeeManagement.Service.Contracts;
using EmployeeManagement.Service.Dtos;

namespace EmployeeManagement.Service.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmpolyeeRepository _employeeRepository;
    public EmployeeService(IEmpolyeeRepository empolyeeRepository)
    {
        _employeeRepository = empolyeeRepository;
    }
    public async Task<bool> Create(CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken)
    {
        string PhotoPath = string.Empty;
        // File upload logic
        if (createEmployeeDto.Photo != null)
        {
            // Define the folder path where the file will be saved
            var folderPath = Path.Combine("wwwroot", "uploads");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath); // Create folder if not exists
            }

            // Create unique file name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(createEmployeeDto.Photo.FileName);

            // Combine folder path and file name
            var filePath = Path.Combine(folderPath, fileName);

            // Save the file to wwwroot/uploads folder
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await createEmployeeDto.Photo.CopyToAsync(stream);
            }

            // Set the file path to save into the database (relative path)
            PhotoPath = "/uploads/" + fileName;
        }
        var employee = new Employee
        {
            FirstName = createEmployeeDto.FirstName,
            LastName = createEmployeeDto.LastName,
            PhoneNumber = createEmployeeDto.PhoneNumber,
            Email = createEmployeeDto.Email,
            DateOfBirth = createEmployeeDto.DateOfBirth,
            PhotoPath = PhotoPath,
        };
        return await _employeeRepository.Create(employee, cancellationToken);
    }

    public async Task<IReadOnlyList<EmployeeDto>> Get(CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.Get(cancellationToken);
        var employeeDto = employee.Select(x => new EmployeeDto
        {
            FirstName = x.FirstName,
            LastName = x.LastName,
            PhoneNumber = x.PhoneNumber,
            Email = x.Email,
            DateOfBirth = x.DateOfBirth,
            PhotoPath = x.PhotoPath,
        }).ToList() ?? [];

        return employeeDto;
    }

    public async Task<EmployeeDto> Get(int id, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.Get(id, cancellationToken);
        var employeeDto = new EmployeeDto
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            PhoneNumber = employee.PhoneNumber,
            Email = employee.Email,
            DateOfBirth = employee.DateOfBirth,
            PhotoPath = employee.PhotoPath
        } ?? new();

        return employeeDto;
    }

    public async Task<bool> Update(UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken)
    {
        var employee = new Employee
        {
            FirstName = updateEmployeeDto.FirstName,
            LastName = updateEmployeeDto.LastName,
            PhoneNumber = updateEmployeeDto.PhoneNumber,
            Email = updateEmployeeDto.Email,
            DateOfBirth = updateEmployeeDto.DateOfBirth,
            PhotoPath = updateEmployeeDto.PhotoPath
        };

        return await _employeeRepository.Update(employee, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        return await _employeeRepository.Delete(id, cancellationToken);
    }
}
