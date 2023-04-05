using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Director
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public virtual ICollection<Movie> MoviesDirected { get; set; }
}