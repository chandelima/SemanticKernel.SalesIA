using Microsoft.SemanticKernel;
using SalesIA.Models;
using SalesIA.Repositories;
using System.ComponentModel;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;

namespace SalesIA.Plugins;

[Description("Representa um relacionamento entre clientes e os produtos que foram comprados pelo mesmo")]
public class BuyedProductPlugin
{
    private readonly BuyedProductRepository _repository;

    public BuyedProductPlugin(BuyedProductRepository repository)
    {
        _repository = repository;
    }

    [KernelFunction, Description("Filtra os relacionamentos entre clientes e lista de produtos comprados baseado em uma expressão. A expressão deve conter um predicado C# válido, como 'relation => relation.Product.Name.ToLower().Contains(\"carne\")'.")]
    [return: Description("Lista de relacionamentos que correspondem a expressão recebida")]
    public List<BuyedProduct> Filter(string predicate)
    {
        var items = _repository.GetAll();

        var parameter = Expression.Parameter(typeof(BuyedProduct), "relation");
        var lambda = DynamicExpressionParser.ParseLambda(new[] { parameter }, null, predicate);

        var filtered = items
            .AsQueryable()
            .Where((Expression<Func<BuyedProduct, bool>>)lambda)
            .ToList();

        return filtered;
    }

    [KernelFunction, Description("Obtem a descrição de todas as propriedades da classe que representa produtos comprados por clientes no formato 'nomePropriedade - Type - Descrição'")]
    [return: Description("Lista de propriedades da classe que representa produtos comprados por clientes")]
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
