using System;
using System.Collections.Generic;

namespace Popple.Models;

public partial class Comic
{
    public int ComicsId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime PostDate { get; set; }

    public string? Description { get; set; }

    public int CreatorId { get; set; }

    public virtual Account Creator { get; set; } = null!;
}
