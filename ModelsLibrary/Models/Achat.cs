using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary.Models;

public partial class Achat
{
    [Required(ErrorMessage = "L'ID est requis.")]
    [Key]
    public int Id { get; set; }

    public string Reference { get; set; } = null!;

    [DataType(DataType.DateTime)]
    public DateTime DateAchat { get; set; }

    public int? Acheteur { get; set; }

    public int? Vendeur { get; set; }

    public int? Jouet { get; set; }

    public int ModeLivraison { get; set; }

    public decimal Prix { get; set; }

    public int ModePay { get; set; }

    public int Statut { get; set; }

    public string RaisonAnnulation { get; set; } = null!;

    public DateTime? DateConfirmation { get; set; }

    public DateTime? DateTransfert { get; set; }


    public Achat()
    {
        Id = 0;
        Reference = null!;
        Acheteur = 0;
        Vendeur = 0;
        Jouet = 0;
        ModeLivraison = 0;
        ModePay = 0;
        Statut = 0;
        Prix = 0;
        RaisonAnnulation = string.Empty;
        DateConfirmation = DateTime.Now;
        DateTransfert = null;
    }
}
