namespace SQLWriter.Models;

public class User
{
		public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string StreetAddress { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string Bsn { get; set; }

        public List <BankAccountUser> BankAccountUsers {get;set;}
}
