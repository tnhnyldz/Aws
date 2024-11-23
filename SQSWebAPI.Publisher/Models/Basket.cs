namespace SQSWebAPI.Publisher.Models
{
	public sealed class Basket
	{
		public Basket()
		{
			Id = Guid.NewGuid();
		}
		public Guid Id { get; set; }
		public string ProductName { get; set; } = string.Empty;
		public int Quantity { get; set; }
		public decimal Price { get; set; }

		public static List<Basket> GetAll()
		{
			return new List<Basket>
			{
				new Basket
				{
					ProductName="Sample 1",
					Quantity=1,
					Price=10
				},new Basket
				{

					ProductName="Sample 2",
					Quantity=2,
					Price=20
				},new Basket
				{

					ProductName="Sample 3",
					Quantity=3,
					Price=30
				}
			};
		}
	}
}
