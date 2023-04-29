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

    public bool EstSupprimer { get; set; }

    public virtual ICollection<JouetsPhoto> JouetsPhotos { get; } = new List<JouetsPhoto>();

    public virtual ICollection<MessagesPhoto> MessagesPhotos { get; } = new List<MessagesPhoto>();

    public virtual ICollection<UtilisateursProfil> UtilisateursProfils { get; } = new List<UtilisateursProfil>();
}
