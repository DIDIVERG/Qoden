using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using NUnit.Framework;

namespace WebApp.Test
{
    public class ExampleTest : MyTestBase
    {
        // This is example test for TODO6. It should works correctly if you've solved other TODOs in the right way.
        // The test structure also is an solution hint.
        [Test(Description = "TODO 6"), Order(1)]
        [Repeat(30)]
        public async Task Todo6()
        {
            var tasks = new List<Task>();
            for (var i = 0; i < 200; i++)
            {
                tasks.Add(new Task(async () => await AliceClient.GetAccountAsync()));
            }

            tasks.AsParallel().ForAll(x => x.Start());
            await Task.WhenAll(tasks);
            await AliceClient.CountAsync();
            await AliceClient.CountAsync();
            await AliceClient.CountAsync();
            await AliceClient.CountAsync();
            await AliceClient.CountAsync();
            var account1 = await (await AliceClient.GetAccountByIdAsync(1)).Response<Account>();
            Assert.True(account1.Counter == 5, $"Account counter = {account1.Counter}");
        }
        
        [Test(Description = "TODO 1")]
        public async Task Todo1_MustReturnCookieIfAuthorized()
        {
            var response =  await Client.SignInAsync("alice@mailinator.com");
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Assert.True((response.Headers.GetValues(HeaderNames.SetCookie).Count() != 0));
        }
        
        [Test(Description = "TODO 2")]
        public async Task Todo2_MustReturnNotFound()
        {
          var response =  await Client.SignInAsync("username@gmail.com");
          Assert.True(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Test(Description = "TODO3")]
        public async Task Todo3_MustPullUser()
        {
            var response = await AliceClient.GetAccountAsync();
            var account = await response.Response<Account>();
            Assert.True(account.ExternalId == "alice" && account.InternalId == 1);
        }

        [Test(Description = "TODO4")]
        public async Task Todo4_IfUnauthorizedReturns401StatusCode()
        {
            var respone = await Client.GetAccountAsync();
            Assert.True(respone.StatusCode == HttpStatusCode.Unauthorized);

        }

        [Test(Description = "TODO5")]
        public async Task Todo5_MustReturn403StatusCodeIfNotAdmin()
        {
            var userWithoutAdminResponse = await BobClient.GetAccountByIdAsync(1);
            Assert.ThrowsAsync<Exception>(() => userWithoutAdminResponse.Response<Account>());
        }
    }
}
