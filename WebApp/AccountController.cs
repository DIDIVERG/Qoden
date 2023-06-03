using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp
{
    // TODO 4: unauthorized users should receive 401 status code 
    // DONE
    [Authorize]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Authorize]
        public async ValueTask<Account> Get()
        {
            var externalIdClaim = User.FindFirst("ExternalId");
            var userId = externalIdClaim.Value;
            return await _accountService.LoadOrCreateAsync(userId /* TODO 3: Get user id from cookie // DONE*/ );
        }

        //TODO 5: Endpoint should works only for users with "Admin" Role
        // DONE
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async ValueTask<Account> GetByInternalId([FromRoute] long id)
        {
            return await _accountService.LoadOrCreateAsync(id); // нужно копать в этот метод, если со строкой он пашет то беда с числами
        }

        [Authorize]
        [HttpPost("counter")]
        public async Task UpdateAccount()
        {
            //Update account in cache, don't bother saving to DB, this is not an objective of this task.
            var account = await Get();
            var internalIdAccount = await GetByInternalId(account.InternalId);
            int counter = account.Counter;
            Interlocked.Increment(ref counter);
            account.Counter = counter;
            internalIdAccount.Counter = counter;
        }
    }
}