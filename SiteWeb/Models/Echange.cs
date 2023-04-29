using System;
using System.Collections.Generic;

namespace SiteWeb.Models;

public partial class Echange
{
    public int Id { get; set; }

    public string Reference { get; set; } = null!;

    public DateTime DateInit { get; set; }

    public DateTime? DateConfirmation { get; set; }

    public DateTime? DateTransfert { get; set; }

    public int Statut { get; set; }

    public string? RaisonAnnulation { get; set; }

    public int? IdUtilisateur1 { get; set; }

    public int? IdUtilisateur2 { get; set; }

    public decimal? PrixUtilisateur1 { get; set; }

    public decimal? PrixUtilisateur2 { get; set; }

    public int ModeLivraison1 { get; set; }

    public int ModeLivraison2 { get; set; }

    public int ModePayUtilisateur1 { get; set; }

    public int ModePayUtilisateur2 { get; set; }

    public int? Jouet1 { get; set; }

    public int? Jouet2 { get; set; }


}
