namespace Domain.Models
{
    public class Source
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Source(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static Source Create(int id, string name)
        {
            return new Source(id, name);
        }
    }
}
