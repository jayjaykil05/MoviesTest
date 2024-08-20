using System;
using System.Collections.Generic;

namespace MoviesTest.Models;

public partial class User
{
    public long UserId { get; set; }

    public string? Fullname { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string Password { get; set; } = null!;
}
