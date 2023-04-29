using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

public partial class MessagesPhoto
{
    public int Id { get; set; }

    public int? Messages { get; set; }

    public int? Photo { get; set; }

    public bool EstSupprimer { get; set; }

    public virtual Message? MessagesNavigation { get; set; }

    public virtual Photo? PhotoNavigation { get; set; }
}
