using Amazon.SQS;
using Amazon.SQS.Model;

/*
for server or computers that not have aws cli

var awsAccesKeyId = "";
var awsSecretAccesKey = "";
var awsRegion = Amazon.RegionEndpoint.EUCentral1;
var sqsClient = new AmazonSQSClient(awsAccesKeyId, awsAccesKeyId, awsRegion);
*/

var awsRegion = Amazon.RegionEndpoint.EUCentral1;
var sqsClient = new AmazonSQSClient(awsRegion);

var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var receiveMessageRequest = new ReceiveMessageRequest
{
	QueueUrl = queueUrlResponse.QueueUrl,
	AttributeNames = new List<string> { "All" },
	MessageAttributeNames = new List<string> { "All" }
};
var cts = new CancellationTokenSource();

while (!cts.IsCancellationRequested)
{
	var response = await sqsClient.ReceiveMessageAsync(receiveMessageRequest);

	foreach (var message in response.Messages)
	{
		//send sms or send email or do something

		Console.WriteLine("----------------------------------------");
		Console.WriteLine($"MessageId:{message.MessageId}");
		Console.WriteLine($"MessageBody{message.Body}");

		//await sqsClient.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle);
	}

	await Task.Delay(100, cts.Token);
}