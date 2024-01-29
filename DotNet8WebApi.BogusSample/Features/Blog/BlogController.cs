using Bogus;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DotNet8WebApi.BogusSample.Features.Blog
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : Controller
    {
        private readonly static List<Blog> _blogList = new List<Blog>();

        [HttpPost(Name = "CreateBlog")]
        public IActionResult CreateBlog(int count)
        {
            var blogFaker = new BlogFaker();

            var blogs = blogFaker.Generate(count);
            _blogList.AddRange(blogs);
            Console.WriteLine(JsonConvert.SerializeObject(blogs, Formatting.Indented));
            return Ok(blogs);
        }

        [HttpGet(Name = "GetBlog")]
        public IActionResult GetBlog()
        {
            return Ok(_blogList);
        }
    }
}
