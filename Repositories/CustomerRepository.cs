using SalesIA.Models;

namespace SalesIA.Repositories;

public class CustomerRepository
{
    private List<Customer> _data = new()
    {
        new() 
        {
            Id = 1,
            Name = "Amado Batista",
            AnyNameOfProperty = new DateOnly(1970, 01, 01)
        },
        new() 
        {
            Id = 2,
            Name = "Fernando Mendes",
            AnyNameOfProperty = new DateOnly(1980, 01, 01)
        },
        new() 
        {
            Id = 3,
            Name = "José Augusto",
            AnyNameOfProperty = new DateOnly(1990, 01, 01)
        },
        new()
        {
            Id = 4,
            Name = "Zezo dos teclados",
            AnyNameOfProperty = new DateOnly(2000, 01, 01)
        }
    };

    public List<Customer> GetAll() => _data;

    public Customer? GetById(int id) => _data.First(x => x.Id == id);
}
