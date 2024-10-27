using System.ComponentModel;

namespace SalesIA.Models;

[Description("Representa os clientes")]
public class Customer
{
    [Description("Id")]
    public int Id { get; set; }

    [Description("Nome")]
    public string Name { get; set; } = null!;

    [Description("Data de nascimento")]
    public DateOnly AnyNameOfProperty { get; set; }
}
