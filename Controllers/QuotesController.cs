using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Framework.Runtime;
using FileHelpers;

namespace famousquotes.Controllers
{
    [Route("api/[controller]")]
    public class QuotesController : Controller
    {
        private readonly IApplicationEnvironment _appEnvironment;
                
        public QuotesController(IApplicationEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }
        
        private Quote[] LoadQuotes() 
        {
            FileHelperEngine<Quote> engine = new FileHelperEngine<Quote>();
            
            String path = Path.Combine(_appEnvironment.ApplicationBasePath, "Data/litemind-quotes.csv");
            Console.Out.WriteLine("Reading the file from the path " + path);
            
            try
            {
                Quote[] quotes = engine.ReadFile(path);
                Console.Out.WriteLine("Read the file, got " + quotes.Length + " quotes");
                return quotes;
            }
            catch (System.Exception ex)
            {
                Console.Error.WriteLine("The request terminated with an error.");
                Console.Error.WriteLine("Message: {0}", ex.Message);
                Console.Error.WriteLine("Stack Trace: {0}", ex.StackTrace);
                Console.Error.WriteLine("Inner Fault: {0}",
                    null == ex.InnerException.Message ? "No Inner Fault" : ex.InnerException.Message);
                throw;
            }
            
        }

        // GET: api/quotes
        [HttpGet]
        public IEnumerable<Quote> Get()
        {
            Console.Out.WriteLine("In GET method");
            Quote[] quotes = LoadQuotes();              
            return quotes;
        }

        // GET api/quotes/5
        [HttpGet("{id}")]
        public Quote Get(int id)
        {
            Quote[] quotes = LoadQuotes();
            return quotes[id];
        }
        
        // GET api/quotes/random
        [HttpGet("random")]
        public Quote GetRandomQuote()
        {
            Quote[] quotes = LoadQuotes();
            Random rand = new Random();
            int index = rand.Next(0, quotes.Length - 1);
            return quotes[index];
        }
    }
}
