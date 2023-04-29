using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

public partial class ModeLivraison
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public decimal? Tarif { get; set; }

    public bool EstSupprimer { get; set; }

    public virtual ICollection<Achat> Achats { get; } = new List<Achat>();

    public virtual ICollection<Echange> EchangeModeLivraison1Navigations { get; } = new List<Echange>();

    public virtual ICollection<Echange> EchangeModeLivraison2Navigations { get; } = new List<Echange>();
}
