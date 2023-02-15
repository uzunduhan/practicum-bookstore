using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName {get; set;}
        public DateTime Birthday { get; set; }
        public List<Book> Books {get; set; }
    }
}