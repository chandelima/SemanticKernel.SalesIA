using System.ComponentModel;

namespace SalesIA.Models;

[Description("Representa produtos comprados por clientes")]
public class BuyedProduct
{
    [Description("Id do produto")]
    public int ProductId { get; set; }

    [Description("Id do cliente")]
    public int CustomerId { get; set; }

    [Description("Quantidade comprada")]
    public int Amount { get; set; }

    [Description("Tipo complexo que representa o produto comprado")]
    public Product Product { get; set; } = null!;

    [Description("Tipo complexo que representa o cliente comprador")]
    public Customer Customer { get; set; } = null!;
}
