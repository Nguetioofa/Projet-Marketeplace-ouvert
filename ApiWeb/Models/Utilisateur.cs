using System;
using System.Collections.Generic;

namespace ApiWeb.Models;

public partial class Utilisateur
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string? Prenom { get; set; }

    public string Email { get; set; } = null!;

    public byte[]? MotDePasse { get; set; }

    public byte[]? Sel { get; set; }

    public string Telephone { get; set; } = null!;

    public string? Adresse { get; set; }

    public string? VilleUser { get; set; }

    public string? QuatierUser { get; set; }

    public DateTime DateCreation { get; set; }

    public DateTime? DateDerniereConnexion { get; set; }

    public int? StatutId { get; set; }

    public bool EstSupprimer { get; set; }

    public virtual ICollection<Abonnement> Abonnements { get; } = new List<Abonnement>();

    public virtual ICollection<Achat> AchatAcheteurNavigations { get; } = new List<Achat>();

    public virtual ICollection<Achat> AchatVendeurNavigations { get; } = new List<Achat>();

    public virtual ICollection<Annonce> Annonces { get; } = new List<Annonce>();

    public virtual ICollection<Commentaire> Commentaires { get; } = new List<Commentaire>();

    public virtual ICollection<Echange> EchangeIdUtilisateur1Navigations { get; } = new List<Echange>();

    public virtual ICollection<Echange> EchangeIdUtilisateur2Navigations { get; } = new List<Echange>();

    public virtual ICollection<Evaluation> EvaluationIdEvaluateurNavigations { get; } = new List<Evaluation>();

    public virtual ICollection<Evaluation> EvaluationIdEvalueNavigations { get; } = new List<Evaluation>();

    public virtual ICollection<FonctionUser> FonctionUsers { get; } = new List<FonctionUser>();

    public virtual ICollection<Jouet> Jouets { get; } = new List<Jouet>();

    public virtual ICollection<Message> MessageIdDestinataireNavigations { get; } = new List<Message>();

    public virtual ICollection<Message> MessageIdExpediteurNavigations { get; } = new List<Message>();

    public virtual ICollection<Photo> Photos { get; } = new List<Photo>();

    public virtual StatutUser? Statut { get; set; }
}
