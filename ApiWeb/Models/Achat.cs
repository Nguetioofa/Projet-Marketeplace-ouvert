﻿using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

public partial class Achat
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

    public bool EstSupprimer { get; set; }

    public virtual Utilisateur? AcheteurNavigation { get; set; }

    public virtual Jouet? JouetNavigation { get; set; }

    public virtual ModeLivraison? ModeLivraisonNavigation { get; set; }

    public virtual ModePayement? ModePayNavigation { get; set; }

    public virtual StatutsTransaction? StatutNavigation { get; set; }

    public virtual Utilisateur? VendeurNavigation { get; set; }
}
