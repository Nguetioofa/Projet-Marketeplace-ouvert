using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SiteWeb.Models;

public partial class Message
{
    public int Id { get; set; }

    public string Contenu { get; set; } = null!;

    [DataType(DataType.DateTime)]
    public DateTime DateM { get; set; }

    public int? IdExpediteur { get; set; }

    public int? IdDestinataire { get; set; }

    public bool Lu { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime? DateLecture { get; set; }


}
