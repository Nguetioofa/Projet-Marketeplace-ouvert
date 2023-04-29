using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

public partial class Jouet
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public int? Categorie { get; set; }

    public int AgeMin { get; set; }

    public int AgeMax { get; set; }

    public int? EtatId { get; set; }

    public string? Descriptions { get; set; }

    public int? Proprietaire { get; set; }

    public decimal Prix { get; set; }

    public bool EstSupprimer { get; set; }

    public virtual ICollection<Achat> Achats { get; } = new List<Achat>();

    public virtual ICollection<Annonce> Annonces { get; } = new List<Annonce>();

    public virtual CategorieJouet? CategorieNavigation { get; set; }

    public virtual ICollection<Echange> EchangeJouet1Navigations { get; } = new List<Echange>();

    public virtual ICollection<Echange> EchangeJouet2Navigations { get; } = new List<Echange>();

    public virtual EtatJouet? Etat { get; set; }

    public virtual ICollection<JouetsPhoto> JouetsPhotos { get; } = new List<JouetsPhoto>();

    public virtual Utilisateur? ProprietaireNavigation { get; set; }

    public virtual ICollection<UtilisateursProfil> UtilisateursProfils { get; } = new List<UtilisateursProfil>();
}
