using Microsoft.AspNetCore.Mvc;
using SQSWebAPI.Publisher.Messaging;
using SQSWebAPI.Publisher.Models;
using System.Net;

namespace SQSWebAPI.Publisher.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class BasketsController(
		SendMessage sqs) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> CreateOrder()
		{
			List<Basket> baskets = Basket.GetAll();
			List<Order> orders = new List<Order>();

			foreach (var basket in baskets)
			{
				Order order = new Order
				{
					Price = basket.Price,
					ProductName = basket.ProductName,
					Quantity = basket.Quantity,
				};

				orders.Add(order);
			}
			//save to db 

			//send mail to customer
			var response = await sqs.SendMessageAsync<List<Order>>(orders);

			if (response.HttpStatusCode == HttpStatusCode.OK)
				return Ok(new { Message = "Order created..." });
			else
				return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Something Went Wrong" });

		}
	}
}
