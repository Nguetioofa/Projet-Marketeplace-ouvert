using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

public partial class Commentaire
{
    public int Id { get; set; }

    public string Contenu { get; set; } = null!;

    public DateTime DateC { get; set; }

    public int? IdAuteur { get; set; }

    public int? IdAnnonce { get; set; }

    public int? IdJouet { get; set; }

    public bool EstSupprimer { get; set; }

    public virtual Annonce? IdAnnonceNavigation { get; set; }

    public virtual Utilisateur? IdAuteurNavigation { get; set; }

    public virtual Jouet? IdJouetNavigation { get; set; }
}
