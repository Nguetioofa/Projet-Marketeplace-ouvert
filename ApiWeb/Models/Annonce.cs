using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

public partial class Annonce
{
    public int Id { get; set; }

    public string Titre { get; set; } = null!;

    public string DescriptionAnnonce { get; set; } = null!;

    public DateTime DateAnnonce { get; set; }

    public int? IdUtilisateur { get; set; }

    public bool EstSupprimer { get; set; }

    public virtual ICollection<Commentaire> Commentaires { get; } = new List<Commentaire>();

    public virtual Utilisateur? IdUtilisateurNavigation { get; set; }

    public virtual ICollection<Photo> Photos { get; } = new List<Photo>();
}
