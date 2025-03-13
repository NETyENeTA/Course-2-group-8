using System;
using System.Collections.Generic;

namespace Ale.Models;

public partial class Connection
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    public int IdBook { get; set; }


    public virtual Book IdBookNavigation { get; set; } = null!;
    public virtual User IdUserNavigation { get; set; } = null!;


    public Connection(int user, int book)
    {
        IdUser = user;
        IdBook = book;
    }
}
