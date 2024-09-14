using EmployeeManagement.Repository.Entities;

namespace EmployeeManagement.Repository.Contracts;

public interface IEmpolyeeRepository
{
    Task<bool> Create(Employee employee, CancellationToken cancellationToken);
    Task<IReadOnlyList<Employee>> Get(CancellationToken cancellationToken);
    Task<Employee?> Get(int? id, CancellationToken cancellationToken);
    Task<bool> Update(Employee employee, CancellationToken cancellationToken);
    Task<bool> Delete(int id, CancellationToken cancellationToken);

}
