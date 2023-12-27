using System;

namespace QueueReaderWriteToCosmos.Models
{
	public class Deposant : Item
	{
		public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Bsn { get; set; }
        public Guid BankStreamId { get; set; }
    }
} 