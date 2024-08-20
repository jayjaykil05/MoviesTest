using System;
using System.Collections.Generic;

namespace MoviesTest.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string? MovieTitle { get; set; }

    public string? MovieDescription { get; set; }

    public bool? IsRented { get; set; }

    public DateTime? RentalDate { get; set; }

    public bool? IsDeleted { get; set; }
}
