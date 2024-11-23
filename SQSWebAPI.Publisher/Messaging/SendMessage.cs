using Amazon.SQS;
using Amazon.SQS.Model;
using System.Net;
using System.Text.Json;

namespace SQSWebAPI.Publisher.Messaging
{
	public sealed class SendMessage(
		IAmazonSQS sqsClient)
	{
		public async Task<SendMessageResponse> SendMessageAsync<T>(T message, CancellationToken cancellationToken = default)
		{
			//var awsRegion = Amazon.RegionEndpoint.EUCentral1;
			//var sqsClient = new AmazonSQSClient(awsRegion);

			var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers", cancellationToken);

			var sendMessageRequest = new SendMessageRequest
			{
				QueueUrl = queueUrlResponse.QueueUrl,
				MessageBody = JsonSerializer.Serialize(message),
				MessageAttributes = new Dictionary<string, MessageAttributeValue>
				{
					{
						"MessageType", new MessageAttributeValue
						{
							DataType = "String",
							StringValue = typeof(T).Name
						}
					}
				}
			};

			var response = await sqsClient.SendMessageAsync(sendMessageRequest, cancellationToken);

			return response;
		}
	}
}
