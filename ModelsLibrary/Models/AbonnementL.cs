﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary.Models;

public partial class AbonnementL
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

    

    public AbonnementL()
    {
        Id = 0;
        Utilisateur = 0;
        Consentement = true;
        DateInscription = DateTime.Now;
        DateDesinscription = null;
    }
}
