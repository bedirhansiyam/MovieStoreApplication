using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Actor : Director
{  
    public List<Movie> MoviesActed { get; set; }
}