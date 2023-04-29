using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SiteWeb.Models;

public partial class Abonnement
{
    [Required(ErrorMessage = "L'ID est requis.")]
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "L'ID de l'utilisateur est requis.")]
    public int? Utilisateur { get; set; }

 
    public bool? Consentement { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime DateInscription { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? DateDesinscription { get; set; }

    

    public Abonnement()
    {
        Id = 0;
        Utilisateur = 0;
        Consentement = true;
        DateInscription = DateTime.Now;
        DateDesinscription = null;
    }
}
