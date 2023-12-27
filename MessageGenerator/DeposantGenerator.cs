using MessageGenerator.Model;
using MessageGenerator.Model.Builder;
using RandomPersonLib;

namespace MessageGenerator
{
	public class DeposantGenerator
    {
		private ILogger<DeposantGenerator> _logger;
		private readonly IRandomPerson _randomPerson;
		

		public DeposantGenerator(ILogger<DeposantGenerator> logger, IRandomPerson randomPerson)
		{
			this._logger = logger;
			this._randomPerson = randomPerson;

		}

		public IEnumerable<Deposant> Generate()
		{
			var people = this._randomPerson.CreatePeople(1000000, Country.Netherlands);
			var deposanten = new List<Deposant>();

			foreach(var person in people)
			{
                var deposantBuilder = new DeposantBuilder();
				deposantBuilder.WithName(person.FirstName, person.LastName)
					.WithAddress($"{person.Address1} {person.Address2}")
					.WithTelephone(person.MobilePhone)
					.WithBsn(person.SSN)
					.WithCity(person.City)
					.WithEmail(person.Email);
				deposanten.Add(deposantBuilder.Build());
			}
			

			return deposanten;
		}
	}
}

