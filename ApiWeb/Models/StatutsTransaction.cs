using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

public partial class StatutsTransaction
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public bool EstSupprimer { get; set; }

    public virtual ICollection<Achat> Achats { get; } = new List<Achat>();

    public virtual ICollection<Echange> Echanges { get; } = new List<Echange>();
}
