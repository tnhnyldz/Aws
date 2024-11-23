using Amazon.SQS;
using Amazon.SQS.Model;

namespace SQSWebAPI.Consumer.Messaging
{
	public sealed class ReceiveMessage
	{
		public async Task ReceiveMessageAsync<T>(CancellationToken cancellationToken = default)
		{
			var awsRegion = Amazon.RegionEndpoint.EUCentral1;
			var sqsClient = new AmazonSQSClient(awsRegion);

			var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

			var receiveMessageRequest = new ReceiveMessageRequest
			{
				QueueUrl = queueUrlResponse.QueueUrl,
				AttributeNames = new List<string> { "All" },
				MessageAttributeNames = new List<string> { "All" }

			};
			while (!cancellationToken.IsCancellationRequested)
			{
				var response = await sqsClient.ReceiveMessageAsync(receiveMessageRequest, cancellationToken);

				foreach (var responeMessage in response.Messages)
				{
					//sms mail or do something

					await sqsClient.DeleteMessageAsync(queueUrlResponse.QueueUrl, responeMessage.ReceiptHandle);
				}
			}

		}
	}
}
