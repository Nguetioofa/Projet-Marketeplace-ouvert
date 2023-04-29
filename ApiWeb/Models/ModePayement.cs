using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

public partial class ModePayement
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public bool EstSupprimer { get; set; }

    public virtual ICollection<Achat> Achats { get; } = new List<Achat>();

    public virtual ICollection<Echange> EchangeModePayUtilisateur1Navigations { get; } = new List<Echange>();

    public virtual ICollection<Echange> EchangeModePayUtilisateur2Navigations { get; } = new List<Echange>();
}
