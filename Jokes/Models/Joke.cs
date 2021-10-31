namespace Jokes.Models
{
    public class Joke
    {
        /// <summary>
        /// Not a preferred code style for lower case properties but it will map well with the outside api
        /// </summary>
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