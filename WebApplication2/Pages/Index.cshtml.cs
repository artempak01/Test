using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.models;
using System.IO;
namespace WebApplication2.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<Balance> Balances;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Balances = GetBalances(808251);
        }

        static List<Balance> GetBalances(int accountId)
        {
            List<Balance> Balanses = new List<Balance>();
            var All_Balanse = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(System.IO.File.ReadAllText("Source/balance_202105270825.json"));
            foreach (var item in All_Balanse["balance"])
            {
                Balanses.Add(new Balance(double.Parse(item["in_balance"].ToString()), double.Parse(item["calculation"].ToString()), Int32.Parse(item["account_id"].ToString()), item["period"].ToString()));
            }
            Balanses = Balanses.Where(balance => balance.AccountId == accountId).
                OrderByDescending(s => s.Period.Year).
                ThenByDescending(s=>s.Period.Month).
                ToList();

            return Balanses;
        }

        
    }
}
