using System;
using System.Collections.Generic;

namespace Popple.Models;

public partial class Comic
{
    public string ComicName { get; set; } = null!;

    public DateTime PostDate { get; set; }

    public string? ComicDescription { get; set; }

    public int CreatorId { get; set; }

    public virtual Account Creator { get; set; } = null!;
}
