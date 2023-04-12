using System;
using System.Collections.Generic;

namespace Popple.Models;

public partial class Favorite
{
    public int AccountId { get; set; }

    public string ComicName { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;

    public virtual Comic ComicNameNavigation { get; set; } = null!;
    
}
