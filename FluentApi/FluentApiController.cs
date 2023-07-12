using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FluentApi
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FluentApiController : ControllerBase
    {
        private readonly FluentApiDBContext _dbContext;

        public FluentApiController(FluentApiDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<X1Model> Get()
        {
            return _dbContext.Get();
        }

        [HttpGet("{intProperty}")]
        public X1Model? Get(int intProperty)
        {
            return _dbContext.Get(intProperty);
        }

        [HttpPost]
        public void Post([FromBody] string stringProperty)
        {
            _dbContext.Add(stringProperty);
        }

        [HttpPut("{intProperty}")]
        public void Put(int intProperty, [FromBody] string stringProperty)
        {
            _dbContext.Update(intProperty, stringProperty);
        }

        [HttpDelete("{intProperty}")]
        public void Delete(int intProperty)
        {
            _dbContext.Delete(intProperty);
        }
    }
}
