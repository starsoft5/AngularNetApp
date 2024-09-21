using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Dependent
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public string Name { get; set; } = null!;
}
