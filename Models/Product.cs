using System.ComponentModel;

namespace SalesIA.Models;

[Description("Representa os produtos")]
public class Product
{
    [Description("Id")]
    public int Id { get; set; }

    [Description("Nome")]
    public string Name { get; set; } = null!;
}
