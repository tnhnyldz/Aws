using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;


/*
for server or computers that not have aws cli

var awsAccesKeyId = "";
var awsSecretAccesKey = "";
var awsRegion = Amazon.RegionEndpoint.EUCentral1;
var sqsClient = new AmazonSQSClient(awsAccesKeyId, awsAccesKeyId, awsRegion);
*/

var awsRegion = Amazon.RegionEndpoint.EUCentral1;
var sqsClient = new AmazonSQSClient(awsRegion);

var customer = new
{
	FirstName = "Teoman",
	LastName = "Yildiz",
	Age = 22
};

//if you are authenticated you can get your url with this method
var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var sendMessageRequest = new SendMessageRequest
{
	QueueUrl = queueUrlResponse.QueueUrl,
	MessageBody = JsonSerializer.Serialize(customer),
	MessageAttributes = new Dictionary<string, MessageAttributeValue>
	{ 
		{
			"MessageType",
			new MessageAttributeValue
			{
				DataType = "String",
				StringValue = "Customer"
			}
		}
	}
};



var response = sqsClient.SendMessageAsync(sendMessageRequest);

Console.ReadLine();