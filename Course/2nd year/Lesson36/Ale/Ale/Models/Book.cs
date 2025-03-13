using System;
using System.Collections.Generic;
using Ale.Models;

namespace Ale;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Author { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Connection> Connections { get; set; } = new List<Connection>();

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
