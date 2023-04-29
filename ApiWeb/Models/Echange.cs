using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

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

    public bool EstSupprimer { get; set; }

    public virtual Utilisateur? IdUtilisateur1Navigation { get; set; }

    public virtual Utilisateur? IdUtilisateur2Navigation { get; set; }

    public virtual Jouet? Jouet1Navigation { get; set; }

    public virtual Jouet? Jouet2Navigation { get; set; }

    public virtual ModeLivraison ModeLivraison1Navigation { get; set; } = null!;

    public virtual ModeLivraison ModeLivraison2Navigation { get; set; } = null!;

    public virtual ModePayement ModePayUtilisateur1Navigation { get; set; } = null!;

    public virtual ModePayement ModePayUtilisateur2Navigation { get; set; } = null!;

    public virtual StatutsTransaction StatutNavigation { get; set; } = null!;
}
