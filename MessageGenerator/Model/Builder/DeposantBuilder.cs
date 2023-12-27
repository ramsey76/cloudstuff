using System;
using MessageGenerator.Model;
namespace MessageGenerator.Model.Builder
{
	public class DeposantBuilder
	{
		private Deposant deposant;

		public DeposantBuilder()
		{
			deposant = new Deposant();
			deposant.Id = Guid.NewGuid();
		}

		public DeposantBuilder WithName(string firstName, string lastName)
		{
			this.deposant.FirstName = firstName;
			this.deposant.LastName = lastName;

			return this;
		}

		public DeposantBuilder WithCity(string city)
		{
			this.deposant.City = city;
			return this;
		}

		public DeposantBuilder WithAddress(string address)
		{
			this.deposant.Address = address;
			return this;
		}

		public DeposantBuilder WithBsn(string bsn)
		{
			this.deposant.Bsn = bsn;
			return this;
		}

		public DeposantBuilder WithTelephone(string telephone)
		{
			this.deposant.Telephone = telephone;
			return this;
		}

		public DeposantBuilder WithEmail(string email)
		{
			this.deposant.Email = email;
			return this;
		}

		public Deposant Build()
		{
			return this.deposant;
		}
	}
}

