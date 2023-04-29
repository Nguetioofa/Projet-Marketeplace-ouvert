using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

public partial class JouetsPhoto
{
    public int Id { get; set; }

    public int? Jouet { get; set; }

    public int? Photo { get; set; }

    public bool EstSupprimer { get; set; }

    public virtual Jouet? JouetNavigation { get; set; }

    public virtual Photo? PhotoNavigation { get; set; }
}
