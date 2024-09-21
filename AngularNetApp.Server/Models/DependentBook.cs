using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class DependentBook
{
    public int Id { get; set; }

    public int DependentId { get; set; }

    public int BookId { get; set; }
}
