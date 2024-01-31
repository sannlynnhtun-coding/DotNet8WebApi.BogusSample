using Bogus;

public class BlogModel
{
    public string BlogId { get; set; } = Guid.NewGuid().ToString("N");
    public string Title { get; set; }
    public string Content { get; set; }
}

public class BlogFaker : Faker<BlogModel>
{
    public BlogFaker()
    {
        //RuleFor(o => o.BlogId, f => f.Random.Number(1, 100));
        RuleFor(o => o.Title, f => f.Lorem.Sentence(10));
        RuleFor(o => o.Content, f => f.Lorem.Word());
    }
}