using System;
using System.Collections.Generic;

namespace Popple.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string? Email { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Comic> Comics { get; } = new List<Comic>();
}
