using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;

namespace Function2
{
    public class HttpConvertCurrency
    {
        private readonly ILogger _logger;

        public HttpConvertCurrency(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<HttpConvertCurrency>();
        }

        [Function("HttpConvertCurrency")]
        [SqlOutput("dbo.BankAccounts", "SqlConnectionString")] //Output conversion to SQL Database
        public IEnumerable<BankAccount> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] 
                                    HttpRequestData req,
                                    [SqlInput(commandText: "SELECT * FROM BankAccounts JOIN Banks ON Banks.Id = BankAccounts.BankId", 
                                    commandType: System.Data.CommandType.Text, 
                                    connectionStringSetting:"SqlConnectionString")] 
                                    IEnumerable<BankAccount> bankAccounts)
        {
            _logger.LogInformation("C# HTTP trigger function proccessing Dollar Conversion.");
            
            
            //var totalAmount = 0m;
            foreach(var bankAccount in bankAccounts)
            {
                bankAccount.DollarAmount = bankAccount.CurrentAmount * 1.08m;
                //totalAmount =+ bankAccount.DollarAmount;
                 
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            
            _logger.LogInformation("C# HTTP trigger function proccessing Dollar Conversion done.");
            
            return bankAccounts;
        }
    }
}
