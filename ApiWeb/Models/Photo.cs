using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

public partial class Photo
{
    public int Id { get; set; }

    public string? NomPhoto { get; set; }

    public string? DescriptionPhoto { get; set; }

    public string UrlP { get; set; } = null!;

    public int Taille { get; set; }

    public string Format { get; set; } = null!;

    public DateTime DatePublication { get; set; }

    public int? Messages { get; set; }

    public int? Jouet { get; set; }

    public int? Profil { get; set; }

    public int? Annonce { get; set; }

    public bool EstSupprimer { get; set; }

    public virtual Annonce? AnnonceNavigation { get; set; }

    public virtual Jouet? JouetNavigation { get; set; }

    public virtual Message? MessagesNavigation { get; set; }

    public virtual Utilisateur? ProfilNavigation { get; set; }
}
