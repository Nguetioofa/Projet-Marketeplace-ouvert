using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public bool EstSupprimer { get; set; }

    public virtual ICollection<FonctionUser> FonctionUsers { get; } = new List<FonctionUser>();
}
