﻿using System;
using System.Collections.Generic;

namespace SiteWeb.Models;

public partial class Jouet
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public int? Categorie { get; set; }

    public int AgeMin { get; set; }

    public int AgeMax { get; set; }

    public int? EtatId { get; set; }

    public string? Descriptions { get; set; }

    public int? Proprietaire { get; set; }

    public decimal Prix { get; set; }



}
