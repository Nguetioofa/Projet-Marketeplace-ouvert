using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

public partial class Message
{
    public int Id { get; set; }

    public string Contenu { get; set; } = null!;

    public DateTime DateM { get; set; }

    public int? IdExpediteur { get; set; }

    public int? IdDestinataire { get; set; }

    public bool Lu { get; set; }

    public DateTime? DateLecture { get; set; }

    public bool EstSupprimer { get; set; }

    public virtual Utilisateur? IdDestinataireNavigation { get; set; }

    public virtual Utilisateur? IdExpediteurNavigation { get; set; }

    public virtual ICollection<MessagesPhoto> MessagesPhotos { get; } = new List<MessagesPhoto>();
}
