using System;
using System.Collections.Generic;
using Popple.Models.Comic;

namespace Popple.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string? Email { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Comic> Comics { get; } = new List<Comic>(); //Intended for a list of a Creator's uploaded comics

    public virtual ICollection<Comic.Name> Favorites { get; set;} = new List<Comic.Name>(); //Intended for a list of any user's favorited comics
    //Ok why does Comic.name not work? 
}
