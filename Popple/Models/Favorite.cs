using System;
using System.Collections.Generic;

namespace Popple.Models;

public partial class Favorite
{
    public int AccountId { get; set; }

    public int ComicsId { get; set; }

    public virtual Account Account { get; set; } = null!;

    public virtual Comic Comics { get; set; } = null!;
}
