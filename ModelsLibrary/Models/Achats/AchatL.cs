using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary.Models.Achats;

public partial class AchatL
{
	public int Id { get; set; }

	public string? Reference { get; set; }

	public DateTime? DateAchat { get; set; }

	public int? Acheteur { get; set; }

	public int? Vendeur { get; set; }

	public int? Jouet { get; set; }

	public int? ModeLivraison { get; set; }

	public decimal? Prix { get; set; }

	public int? ModePay { get; set; }

	public int? Statut { get; set; }

	public string? RaisonAnnulation { get; set; }

	public DateTime? DateConfirmation { get; set; }

	public DateTime? DateTransfert { get; set; }
}
