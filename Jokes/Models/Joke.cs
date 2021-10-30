namespace Jokes.Models
{
    public class Joke
    {
        public string id { get; set; }
        public string joke { get; set; }
        public int? status { get; set; }
        public GroupType group_type { get; set; }

        public enum GroupType 
        {
            small,
            medium,
            large,
            invalid
        }
    }
}