using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary.Models;

public partial class CommentaireL
{
    public int Id { get; set; }

    public string Contenu { get; set; } = null!;

    [DataType(DataType.DateTime)]
    public DateTime DateC { get; set; }

    public int? IdAuteur { get; set; }
	public int? IdJouet { get; set; }
	public int? IdAnnonce { get; set; }

}
