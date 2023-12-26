using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vb.Data;
using Vb.Data.Entity;

namespace VbApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly VbDbContext dbContext;

    public AddressController(VbDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public async Task<List<Address>> Get()
    {
        return await dbContext.Set<Address>()
            .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<Address> Get(int id)
    {
        var address =  await dbContext.Set<Address>()
            .Where(x => x.Id == id).FirstOrDefaultAsync();
       
        return address;
    }

    [HttpPost]
    public async Task Post([FromBody] Address address)
    {
        await dbContext.Set<Address>().AddAsync(address);
        await dbContext.SaveChangesAsync();
    }

    [HttpPut("{id}")]
    public async Task Put(int id, [FromBody] Address address)
    {
        var fromdb = await dbContext.Set<Address>().Where(x => x.Id == id).FirstOrDefaultAsync();
        fromdb.Address1 = address.Address1;
        fromdb.Address2 = address.Address2;
        fromdb.Country = address.Country;
        fromdb.City = address.City;
        fromdb.County = address.County;
        await dbContext.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task Delete(int id)
    {
        var fromdb = await dbContext.Set<Address>().Where(x => x.Id == id).FirstOrDefaultAsync();
        fromdb.IsActive = false;
        await dbContext.SaveChangesAsync();
    }
}