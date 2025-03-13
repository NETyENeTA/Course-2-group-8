using System;
using System.Collections.Generic;

namespace Ale.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Connection> Connections { get; set; } = new List<Connection>();


    public User(string name) => Name = name;
}
