﻿using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

public partial class Newsletter
{
    public int Id { get; set; }

    public string Objet { get; set; } = null!;

    public string Contenu { get; set; } = null!;

    public DateTime DateEnvoi { get; set; }

    public string Outil { get; set; } = null!;

    public bool EstSupprimer { get; set; }
}
