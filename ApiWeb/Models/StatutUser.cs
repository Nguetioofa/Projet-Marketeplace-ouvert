using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

public partial class StatutUser
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public bool EstSupprimer { get; set; }

    public virtual ICollection<Utilisateur> Utilisateurs { get; } = new List<Utilisateur>();
}
