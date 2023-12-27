﻿namespace SQLWriter.Models;

public class User
{
		public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Bsn { get; set; }

        public List <BankAccountUsers> BankAccountUsers {get;set;}
}
