using Microsoft.SemanticKernel;
using SalesIA.Models;
using SalesIA.Repositories;
using System.ComponentModel;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace SalesIA.Plugins;

[Description("Representa um cliente")]
public class CustomersPlugin
{
    private readonly CustomerRepository _repository;

    public CustomersPlugin(CustomerRepository repository)
    {
        _repository = repository;
    }

    [KernelFunction, Description("Filtra clientes baseados em uma expressão. A expressão deve conter um predicado C# válido, como 'customer => customer.Name == \"Amado Batista\" && customer.Id == 1'. O tipo das propriedades pode ser obtido a partir da função GetAllProperties")]
    [return: Description("Lista de clientes que correspondem a expressão recebida")]
    public List<Customer> Filter(string predicate)
    {
        var items = _repository.GetAll();

        var parameter = Expression.Parameter(typeof(Customer), "customer");
        var lambda = DynamicExpressionParser.ParseLambda(new[] { parameter }, null, predicate);

        var filtered = items
            .AsQueryable()
            .Where((Expression<Func<Customer, bool>>)lambda)
            .ToList();

        return filtered;
    }

    [KernelFunction, Description("Obtem a descrição de todas as propriedades da classe cliente no formato 'nomePropriedade - Format - Descrição'")]
    [return: Description("Lista de propriedades da classe cliente")]
    public List<string> GetAllProperties()
    {
        Type entityType = typeof(Customer);
        var properties = entityType.GetProperties();

        var propertyNames = new List<string>();

        foreach (var property in properties)
        {
            var name = property.Name;

            var attribute = property.GetCustomAttribute<DescriptionAttribute>();
            var type = property.PropertyType.Name;
            var description = attribute == null ? name : attribute.Description;

            propertyNames.Add($"{name} - {type} - {description}");
        }

        return propertyNames;
    }
}
