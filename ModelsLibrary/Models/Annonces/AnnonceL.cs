using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary.Models.Annonces;

public partial class AnnonceL
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "La description est le titre")]
    public string Titre { get; set; } = null!;
    [Required(ErrorMessage = "La description est obligatoire")]
    public string DescriptionAnnonce { get; set; } = null!;

    public DateTime DateAnnonce { get; set; }

    public int? IdUtilisateur { get; set; }

    public bool EstSupprimer { get; set; }

}
