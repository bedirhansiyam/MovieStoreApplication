using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Movie
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string MovieName { get; set; }
    public DateTime ReleaseDate { get; set; }
    public Genre Genre { get; set; }    
    public Director Director { get; set; }
    public List<Actor> Actors { get; set; }
    public decimal Price { get; set; }

}