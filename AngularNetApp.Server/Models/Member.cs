using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Member
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
}
