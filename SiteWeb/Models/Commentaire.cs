using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SiteWeb.Models;

public partial class Commentaire
{
    public int Id { get; set; }

    public string Contenu { get; set; } = null!;

    [DataType(DataType.DateTime)]
    public DateTime DateC { get; set; }

    public int? IdAuteur { get; set; }

    public int? IdAnnonce { get; set; }



}
