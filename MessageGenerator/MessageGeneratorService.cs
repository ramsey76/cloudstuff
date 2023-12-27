using System;
namespace MessageGenerator
{
	public class MessageGeneratorService
	{
		private readonly DeposantGenerator deposantenGenerator;
		private readonly MessageSender _messageSender;

		public MessageGeneratorService(DeposantGenerator deposantenGenerator, MessageSender messageSender)
		{
			this.deposantenGenerator = deposantenGenerator;
			this._messageSender = messageSender;
		}

		public Task GenerateAndPublish()
		{
			var deposanten = deposantenGenerator.Generate();
			return _messageSender.SendBatchAsync(deposanten.ToList());
		}
	}
}

