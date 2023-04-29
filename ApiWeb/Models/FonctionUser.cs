using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

public partial class FonctionUser
{
    public int Id { get; set; }

    public int? RolesId { get; set; }

    public int? IdUser { get; set; }

    public bool EstSupprimer { get; set; }

    public virtual Utilisateur? IdUserNavigation { get; set; }

    public virtual Role? Roles { get; set; }
}
