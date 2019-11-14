using System;
using System.Collections.Generic;
using System.Linq;

namespace BloggingEngineApi.Models
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj == null || !(obj is Blog))
            {
                return false;
            }
            else
            {
                return this.BlogId == ((Blog)obj).BlogId &&
                       this.Url == ((Blog)obj).Url 
                       //&& this.PostsAreEqual(((Blog)obj).Posts)
                                                            ;
            }
        }

        public override int GetHashCode()
        {
            return this.Url.GetHashCode();
        }

        private bool PostsAreEqual(ICollection<Post> otherPosts)
        {
            return this.Posts.Count == otherPosts.Count && this.Posts.Except(otherPosts).Count() == 0;
        }
    }
}