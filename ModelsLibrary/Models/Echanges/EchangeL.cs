using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary.Models.Echanges;

public partial class EchangeL
{
	public int Id { get; set; }

	public string? Reference { get; set; }

	public DateTime? DateInit { get; set; }

	public DateTime? DateConfirmation { get; set; }

	public DateTime? DateTransfert { get; set; }

	public int? Statut { get; set; } = 1;

	public string? RaisonAnnulation { get; set; }
	[Required]

	public int? IdUtilisateur1 { get; set; } = 1;
    [Required]

    public int? IdUtilisateur2 { get; set; } = 1;

    public decimal? PrixUtilisateur1 { get; set; }

	public decimal? PrixUtilisateur2 { get; set; }

	public int? ModeLivraison1 { get; set; } = 1;

    public int? ModeLivraison2 { get; set; } = 1;

    public int? ModePayUtilisateur1 { get; set; } = 1;

    public int? ModePayUtilisateur2 { get; set; } = 1;
    [Required]

    public int? Jouet1 { get; set; }

	[Required]
	public int? Jouet2 { get; set; }
}
