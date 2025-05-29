namespace Ordering.Infrastructure.Data.Extensions;
internal class InitialData
{
	public static IEnumerable<Customer> Customers =>
		new List<Customer>
		{
				Customer.Create(CustomerId.Of(new Guid("d13b50c1-2b5a-4d9c-a0df-18c228b95b2d")),"mehmet","mehmet@gmail.com"),
				Customer.Create(CustomerId.Of(new Guid("a3c0ef54-5f93-4c47-91ab-3d1e9057fd2a")),"john","john@gmail.com")
		};

	public static IEnumerable<Product> Products =>
		new List<Product>
		{
			Product.Create(ProductId.Of(new Guid("c1f645a5-7b7c-4e7d-bf7d-76323e2c62f1")), "Product 1", 100),
			Product.Create(ProductId.Of(new Guid("e8a0c0f2-4bb0-4c6d-9a72-2549c9c26f33")), "Product 2", 200),
			Product.Create(ProductId.Of(new Guid("b0f607de-f847-4564-87df-2c63be75b449")), "Product 3", 600),
			Product.Create(ProductId.Of(new Guid("a3db4c77-6881-4c5f-a03a-7b5c2f2b12a0")), "Product 4", 800)
		};

	public static IEnumerable<Order> OrdersWithItems
	{
		get
		{
			var address1 = Address.Of("Filan", "Fisteku", "State 1", "Country 1", "Kosove", "State 1", "123");
			var address2 = Address.Of("Filan1", "Fisteku2", "State 2", "Country 2", "Kosove", "State 2", "123");

			var payment1 = Payment.Of("123", "1234567890123456", "12/25", "123", 1);
			var payment2 = Payment.Of("123", "1234567890123456", "12/25", "123", 2);

			var order1 = Order.Create(OrderId.Of(Guid.NewGuid()), CustomerId.Of(new Guid("d13b50c1-2b5a-4d9c-a0df-18c228b95b2d")), OrderName.Of("ORD_1"), address1, address1, payment1);

			order1.Add(ProductId.Of(new Guid("c1f645a5-7b7c-4e7d-bf7d-76323e2c62f1")), 2, 500);
			order1.Add(ProductId.Of(new Guid("e8a0c0f2-4bb0-4c6d-9a72-2549c9c26f33")), 1, 400);

			var order2 = Order.Create(
					OrderId.Of(Guid.NewGuid()),
					CustomerId.Of(new Guid("a3c0ef54-5f93-4c47-91ab-3d1e9057fd2a")),
					OrderName.Of("ORD_2"),
					address2,
					address2,
					payment2);

			order2.Add(ProductId.Of(new Guid("b0f607de-f847-4564-87df-2c63be75b449")), 2, 500);
			order2.Add(ProductId.Of(new Guid("a3db4c77-6881-4c5f-a03a-7b5c2f2b12a0")), 1, 400);

			return new List<Order> { order1, order2 };
		}
	}
}
