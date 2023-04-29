using System;
using System.Collections.Generic;

namespace SiteWeb.Models;

public partial class Annonce
{
    public int Id { get; set; }

    public string Titre { get; set; } = null!;

    public string DescriptionAnnonce { get; set; } = null!;

    public DateTime DateAnnonce { get; set; }

    public int? IdUtilisateur { get; set; }

    public bool AcceptTroc { get; set; }

    public bool AcceptAchat { get; set; }

    public int? IdJouet { get; set; }



}
