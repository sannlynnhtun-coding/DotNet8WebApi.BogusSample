using Bogus;

public class Blog
{
    public int BLogId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
}

public class BlogFaker : Faker<Blog>
{
    public BlogFaker()
    {
        RuleFor(o => o.BLogId, f => f.Random.Number(1, 100));
        RuleFor(o => o.Title, f => f.Lorem.Sentence(10));
        RuleFor(o => o.Content, f => f.Lorem.Word());
    }
}