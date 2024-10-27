using SalesIA.Models;

namespace SalesIA.Repositories;

public class BuyedProductRepository
{
    private CustomerRepository _customerRepository;
    private ProductRepository _productRepository;

    public BuyedProductRepository(
        CustomerRepository customerRepository, 
        ProductRepository productRepository)
    {
        _customerRepository = customerRepository;
        _productRepository = productRepository;
    }

    private BuyedProduct GenerateByCustomerAndProductId(int customerId, int productId)
    {
        return new()
        {
            CustomerId = customerId,
            Customer = _customerRepository.GetById(customerId)!,
            ProductId = productId,
            Product = _productRepository.GetById(productId)!
        };
    }

    public List<BuyedProduct> GetAll()
    {
        List<BuyedProduct> data = new()
        {
            GenerateByCustomerAndProductId(1, 1),
            GenerateByCustomerAndProductId(1, 2),
            GenerateByCustomerAndProductId(2, 2),
            GenerateByCustomerAndProductId(2, 6),
            GenerateByCustomerAndProductId(2, 3),
            GenerateByCustomerAndProductId(3, 3),
            GenerateByCustomerAndProductId(3, 4),
            GenerateByCustomerAndProductId(4, 4),
            GenerateByCustomerAndProductId(4, 6),
            GenerateByCustomerAndProductId(4, 5),
            GenerateByCustomerAndProductId(4, 5),
            GenerateByCustomerAndProductId(4, 6)
        };

        return data;
    }
}
