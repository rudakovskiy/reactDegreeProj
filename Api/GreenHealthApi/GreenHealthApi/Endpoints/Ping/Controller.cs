using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GreenHealthApi.Endpoints.Ping
{
    [ApiController]
    [Route("v1/ping/")]
    public class Controller : ControllerBase
    {
        private readonly IConnectionStringGetter _conStr;

        public Controller(IConnectionStringGetter conStr)
        {
            _conStr = conStr;
        }
        [HttpGet]
        [Route("dateTime/")]
        public async Task<DateTime> GetDateTime()
        {
            return DateTime.Now;
        }
        [HttpGet]
        [Route("dbConnection/")]
        public async Task<string> DbConnection()
        {
            await using var con = new SqlConnection(_conStr.Get());
            await con.OpenAsync();
            var conState = con.State;
            return Convert.ToString(conState);
        }
    }
}