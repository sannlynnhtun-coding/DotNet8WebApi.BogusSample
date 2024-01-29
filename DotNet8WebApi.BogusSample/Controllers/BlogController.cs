using Bogus;
using DotNet8WebApi.BogusSample;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8WebApi.BogusSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : Controller
    {
        private readonly static List<Blog> _blogList = new List<Blog>();

        [HttpPost(Name = "CreateBlog")]
        public void CreateBlog(int count)
        {
            var blogFaker = new BlogFaker();

            for (int i = 0; i < count; i++)
            {
                var blog = blogFaker.Generate();
                _blogList.Add(blog);
                Console.WriteLine($"Blog {i + 1}:");
                Console.WriteLine($"BlogId: {blog.BLogId}, Title: {blog.Title}, Content: {blog.Content}\n");
            }
        }

        [HttpGet(Name = "GetBlog")]
        public IEnumerable<Blog> GetBlog()
        {
            return _blogList;
        }

        [HttpGet("{id}", Name = "GetBlogById")]
        public Blog GetBlogById(int id)
        {
            return _blogList.FirstOrDefault(b => b.BLogId == id)!;
        }

        [HttpPut("{id}", Name = "UpdateBlog")]
        public Blog UpdateBlog(int id, string title, string content)
        {
            var blog = _blogList.FirstOrDefault(b => b.BLogId == id);
            if (blog != null)
            {
                blog.Title = title;
                blog.Content = content;
            }
            return blog!;
        }

        [HttpDelete("{id}", Name = "DeleteBlog")]
        public void DeleteBlog(int id)
        {
            var blog = _blogList.FirstOrDefault(b => b.BLogId == id);
            if (blog != null)
            {
                _blogList.Remove(blog);
            }
        }
    }
}
