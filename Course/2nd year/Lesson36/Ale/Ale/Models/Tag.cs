using System;
using System.Collections.Generic;

namespace Ale.Models;

public partial class Tag
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int IdBook { get; set; }

    public virtual Book IdBookNavigation { get; set; } = null!;


    public Tag(string name, int book)
    {
        Name = name;
        IdBook = book;
    } 
}
