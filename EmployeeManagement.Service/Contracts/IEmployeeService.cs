using EmployeeManagement.Service.Dtos;

namespace EmployeeManagement.Service.Contracts;

public interface IEmployeeService
{
    Task<bool> Create(CreateEmployeeDto category, CancellationToken cancellationToken);
    Task<IReadOnlyList<EmployeeDto>> Get(CancellationToken cancellationToken);
    Task<EmployeeDto> Get(int id, CancellationToken cancellationToken);
    Task<bool> Update(UpdateEmployeeDto category, CancellationToken cancellationToken);
    Task<bool> Delete(int id, CancellationToken cancellationToken);
}
