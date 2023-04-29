using System;
using System.Collections.Generic;

namespace SiteWeb.Models;

public partial class ModeLivraison
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public decimal? Tarif { get; set; }



}
