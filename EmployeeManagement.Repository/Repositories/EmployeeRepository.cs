using EmployeeManagement.Repository.Contracts;
using EmployeeManagement.Repository.DatabaseContext;
using EmployeeManagement.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository.Repositories;

public class EmployeeRepository : IEmpolyeeRepository
{
    private readonly AppDbContext _DbContext;
    public EmployeeRepository(AppDbContext appDbContext)
    {
        _DbContext = appDbContext;
    }

    public async Task<bool> Create(Employee employee, CancellationToken cancellationToken)
    {
        _DbContext.Employees.Add(employee);
        return await _DbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<IReadOnlyList<Employee>> Get(CancellationToken cancellationToken)
    {
        return await _DbContext.Employees.ToListAsync(cancellationToken);
    }

    public async Task<Employee?> Get(int? id, CancellationToken cancellationToken)
    {
        var employee = await _DbContext.Employees.FindAsync(id, cancellationToken);
        if (employee == null)
        {
            Console.WriteLine($"employee with id {id} is not found.");
        }
        return employee;
    }

    public async Task<bool> Update(Employee employee, CancellationToken cancellationToken)
    {
        var existingEmployee = await Get(employee.Id, cancellationToken);
        if (existingEmployee != null)
        {
            _DbContext.Entry(employee).State = EntityState.Modified;
        }
        return await _DbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var employee = await Get(id, cancellationToken);
        if (employee != null)
        {
            _DbContext.Remove(employee);
        }
        return await _DbContext.SaveChangesAsync(cancellationToken) > 0;
    }

}
