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
        private readonly Calculate _calculate;

        public HttpConvertCurrency(ILoggerFactory loggerFactory, Calculate calculate)
        {
            _logger = loggerFactory.CreateLogger<HttpConvertCurrency>();
            _calculate = calculate;
        }

        [Function("HttpConvertCurrency")]
        
        public async Task<OutputType> RunAsync([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] 
                                    HttpRequestData req,
                                    [SqlInput(commandText: "SELECT top 500000 * FROM BankAccounts JOIN Banks ON Banks.Id = BankAccounts.BankId", 
                                    commandType: System.Data.CommandType.Text, 
                                    connectionStringSetting:"SqlConnectionString")] 
                                    IEnumerable<BankAccount> bankAccounts)
        {
            _logger.LogInformation("C# HTTP trigger function proccessing Dollar Conversion.");
            
            
            foreach(var bankAccount in bankAccounts)
            {
                
                _calculate.DollarAmountCurrentAccount(bankAccount);
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            
            _logger.LogInformation("C# HTTP trigger function proccessing Dollar Conversion done.");
            
            return new OutputType {
                BankAccounts1 = bankAccounts,
                HttpResponse= req.CreateResponse(System.Net.HttpStatusCode.OK)
            };
        }

        public class OutputType()
        {
            [SqlOutput("dbo.BankAccounts", "SqlConnectionString")] //Output conversion to SQL Database
            public IEnumerable<BankAccount> BankAccounts1 {get;set;}
            
            public HttpResponseData HttpResponse {get;set;}
        }
    }
}
