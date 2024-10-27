using SalesIA.Models;

namespace SalesIA.Repositories;

public class ProductRepository
{
    private List<Product> _data = new()
    {
        new() {
            Id = 1,
            Name = "Coca-cola lata 350ml"
        },
        new() {
            Id = 2,
            Name = "Cartela de ovos - 12 unidades"
        },
        new() {
            Id = 3,
            Name = "Mouse sem fio Multilaser"
        },
        new()
        {
            Id = 4,
            Name = "Arroz - 1 Kg"
        },
        new()
        {
            Id = 5,
            Name = "Feijão - 1Kg"
        },
        new()
        {
            Id = 6,
            Name = "Macarrão - 1Kg"
        }
    };

    public List<Product> GetAll() => _data;

    public Product? GetById(int id) => _data.First(x => x.Id == id);
}
