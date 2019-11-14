using System;

namespace BloggingEngineApi.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Post))
            {
                return false;
            }
            else
            {
                return this.PostId == ((Post)obj).PostId &&
                       this.Title == ((Post)obj).Title &&
                       this.Content == ((Post)obj).Content &&
                       this.BlogId == ((Post)obj).BlogId;
            }
        }

        public override int GetHashCode()
        {
            return this.Content.GetHashCode();
        }
    }
}