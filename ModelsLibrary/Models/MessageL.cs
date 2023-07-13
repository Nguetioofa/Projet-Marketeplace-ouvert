using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary.Models;

public partial class MessageL
{
    public int? Id { get; set; }
    [Required(ErrorMessage ="vous ne pouvez pas envoye un message vide")]
    public string Contenu { get; set; } = null!;

    [DataType(DataType.DateTime)]
    public DateTime? DateM { get; set; }

    public int? IdExpediteur { get; set; }

    public int? IdDestinataire { get; set; }

    public bool Lu { get; set; } = false;

    [DataType(DataType.DateTime)]
    public DateTime? DateLecture { get; set; }


}
