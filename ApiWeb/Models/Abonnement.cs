using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

public partial class Abonnement
{
    public int Id { get; set; }

    public int? Utilisateur { get; set; }

    public bool? Consentement { get; set; }

    public DateTime DateInscription { get; set; }

    public DateTime? DateDesinscription { get; set; }

    public bool EstSupprimer { get; set; }

    public virtual Utilisateur? UtilisateurNavigation { get; set; }
}
