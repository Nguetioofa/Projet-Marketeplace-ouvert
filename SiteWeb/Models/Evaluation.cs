using System;
using System.Collections.Generic;

namespace SiteWeb.Models;

public partial class Evaluation
{
    public int Id { get; set; }

    public int Note { get; set; }

    public string? Commentaire { get; set; }

    public DateTime DateE { get; set; }

    public int? IdEvaluateur { get; set; }

    public int? IdEvalue { get; set; }


}
