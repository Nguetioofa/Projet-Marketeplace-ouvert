using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiWeb.Models;

public partial class EchangeJouetsContext : DbContext
{
    public EchangeJouetsContext()
    {
    }

    public EchangeJouetsContext(DbContextOptions<EchangeJouetsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abonnement> Abonnements { get; set; }

    public virtual DbSet<Achat> Achats { get; set; }

    public virtual DbSet<Annonce> Annonces { get; set; }

    public virtual DbSet<CategorieJouet> CategorieJouets { get; set; }

    public virtual DbSet<Commentaire> Commentaires { get; set; }

    public virtual DbSet<Echange> Echanges { get; set; }

    public virtual DbSet<EtatJouet> EtatJouets { get; set; }

    public virtual DbSet<Evaluation> Evaluations { get; set; }

    public virtual DbSet<FonctionUser> FonctionUsers { get; set; }

    public virtual DbSet<Jouet> Jouets { get; set; }

    public virtual DbSet<JouetsPhoto> JouetsPhotos { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessagesPhoto> MessagesPhotos { get; set; }

    public virtual DbSet<ModeLivraison> ModeLivraisons { get; set; }

    public virtual DbSet<ModePayement> ModePayements { get; set; }

    public virtual DbSet<Newsletter> Newsletters { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StatutUser> StatutUsers { get; set; }

    public virtual DbSet<StatutsTransaction> StatutsTransactions { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    public virtual DbSet<UtilisateursProfil> UtilisateursProfils { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Abonnement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__abonneme__3213E83F99DEB9F6");

            entity.ToTable("abonnements");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Consentement)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("consentement");
            entity.Property(e => e.DateDesinscription)
                .HasColumnType("datetime")
                .HasColumnName("date_desinscription");
            entity.Property(e => e.DateInscription)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_inscription");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Utilisateur).HasColumnName("utilisateur");

            entity.HasOne(d => d.UtilisateurNavigation).WithMany(p => p.Abonnements)
                .HasForeignKey(d => d.Utilisateur)
                .HasConstraintName("FK__abonnemen__utili__46B27FE2");
        });

        modelBuilder.Entity<Achat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__achat__3213E83F0B156F7B");

            entity.ToTable("achat");

            entity.HasIndex(e => e.Reference, "UQ__achat__FD90DA999AEC21C4").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Acheteur).HasColumnName("acheteur");
            entity.Property(e => e.DateAchat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_achat");
            entity.Property(e => e.DateConfirmation)
                .HasColumnType("datetime")
                .HasColumnName("date_confirmation");
            entity.Property(e => e.DateTransfert)
                .HasColumnType("datetime")
                .HasColumnName("date_transfert");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Jouet).HasColumnName("jouet");
            entity.Property(e => e.ModeLivraison).HasColumnName("mode_livraison");
            entity.Property(e => e.ModePay).HasColumnName("mode_pay");
            entity.Property(e => e.Prix)
                .HasColumnType("money")
                .HasColumnName("prix");
            entity.Property(e => e.RaisonAnnulation)
                .HasColumnType("text")
                .HasColumnName("raison_annulation");
            entity.Property(e => e.Reference)
                .HasMaxLength(15)
                .HasColumnName("reference");
            entity.Property(e => e.Statut)
                .HasDefaultValueSql("((1))")
                .HasColumnName("statut");
            entity.Property(e => e.Vendeur).HasColumnName("vendeur");

            entity.HasOne(d => d.AcheteurNavigation).WithMany(p => p.AchatAcheteurNavigations)
                .HasForeignKey(d => d.Acheteur)
                .HasConstraintName("FK__achat__acheteur__4B7734FF");

            entity.HasOne(d => d.JouetNavigation).WithMany(p => p.Achats)
                .HasForeignKey(d => d.Jouet)
                .HasConstraintName("FK__achat__jouet__17F790F9");

            entity.HasOne(d => d.ModeLivraisonNavigation).WithMany(p => p.Achats)
                .HasForeignKey(d => d.ModeLivraison)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__achat__mode_livr__18EBB532");

            entity.HasOne(d => d.ModePayNavigation).WithMany(p => p.Achats)
                .HasForeignKey(d => d.ModePay)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__achat__mode_pay__19DFD96B");

            entity.HasOne(d => d.StatutNavigation).WithMany(p => p.Achats)
                .HasForeignKey(d => d.Statut)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__achat__statut__1AD3FDA4");

            entity.HasOne(d => d.VendeurNavigation).WithMany(p => p.AchatVendeurNavigations)
                .HasForeignKey(d => d.Vendeur)
                .HasConstraintName("FK__achat__vendeur__43D61337");
        });

        modelBuilder.Entity<Annonce>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__annonce__3213E83FEE3769E5");

            entity.ToTable("annonce");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcceptAchat).HasColumnName("accept_achat");
            entity.Property(e => e.AcceptTroc).HasColumnName("accept_troc");
            entity.Property(e => e.DateAnnonce)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_annonce");
            entity.Property(e => e.DescriptionAnnonce)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description_annonce");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.IdJouet).HasColumnName("id_Jouet");
            entity.Property(e => e.IdUtilisateur).HasColumnName("id_Utilisateur");
            entity.Property(e => e.Titre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("titre");

            entity.HasOne(d => d.IdJouetNavigation).WithMany(p => p.Annonces)
                .HasForeignKey(d => d.IdJouet)
                .HasConstraintName("FK__annonce__id_Joue__6B24EA82");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.Annonces)
                .HasForeignKey(d => d.IdUtilisateur)
                .HasConstraintName("FK__annonce__id_Util__44CA3770");
        });

        modelBuilder.Entity<CategorieJouet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83F8304D4BB");

            entity.ToTable("categorie_jouet");

            entity.HasIndex(e => e.Nom, "UQ__categori__DF90DC2C9C3A1E31").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Commentaire>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__commenta__3213E83F0281F225");

            entity.ToTable("commentaires");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contenu)
                .HasColumnType("text")
                .HasColumnName("contenu");
            entity.Property(e => e.DateC)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_c");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.IdAnnonce).HasColumnName("id_annonce");
            entity.Property(e => e.IdAuteur).HasColumnName("id_auteur");

            entity.HasOne(d => d.IdAnnonceNavigation).WithMany(p => p.Commentaires)
                .HasForeignKey(d => d.IdAnnonce)
                .HasConstraintName("FK__commentai__id_an__10566F31");

            entity.HasOne(d => d.IdAuteurNavigation).WithMany(p => p.Commentaires)
                .HasForeignKey(d => d.IdAuteur)
                .HasConstraintName("FK__commentai__id_au__4A8310C6");
        });

        modelBuilder.Entity<Echange>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__echange__3213E83F280CEA64");

            entity.ToTable("echange");

            entity.HasIndex(e => e.Reference, "UQ__echange__FD90DA990B1ACAF6").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateConfirmation)
                .HasColumnType("datetime")
                .HasColumnName("date_confirmation");
            entity.Property(e => e.DateInit)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_init");
            entity.Property(e => e.DateTransfert)
                .HasColumnType("datetime")
                .HasColumnName("date_transfert");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.IdUtilisateur1).HasColumnName("id_Utilisateur1");
            entity.Property(e => e.IdUtilisateur2).HasColumnName("id_Utilisateur2");
            entity.Property(e => e.Jouet1).HasColumnName("jouet1");
            entity.Property(e => e.Jouet2).HasColumnName("jouet2");
            entity.Property(e => e.ModeLivraison1)
                .HasDefaultValueSql("((1))")
                .HasColumnName("mode_livraison1");
            entity.Property(e => e.ModeLivraison2)
                .HasDefaultValueSql("((1))")
                .HasColumnName("mode_livraison2");
            entity.Property(e => e.ModePayUtilisateur1)
                .HasDefaultValueSql("((1))")
                .HasColumnName("mode_pay_Utilisateur1");
            entity.Property(e => e.ModePayUtilisateur2)
                .HasDefaultValueSql("((1))")
                .HasColumnName("mode_pay_Utilisateur2");
            entity.Property(e => e.PrixUtilisateur1)
                .HasColumnType("money")
                .HasColumnName("prix_Utilisateur1");
            entity.Property(e => e.PrixUtilisateur2)
                .HasColumnType("money")
                .HasColumnName("prix_Utilisateur2");
            entity.Property(e => e.RaisonAnnulation)
                .HasColumnType("text")
                .HasColumnName("raison_annulation");
            entity.Property(e => e.Reference)
                .HasMaxLength(15)
                .HasColumnName("reference");
            entity.Property(e => e.Statut)
                .HasDefaultValueSql("((1))")
                .HasColumnName("statut");

            entity.HasOne(d => d.IdUtilisateur1Navigation).WithMany(p => p.EchangeIdUtilisateur1Navigations)
                .HasForeignKey(d => d.IdUtilisateur1)
                .HasConstraintName("FK__echange__id_Util__4D5F7D71");

            entity.HasOne(d => d.IdUtilisateur2Navigation).WithMany(p => p.EchangeIdUtilisateur2Navigations)
                .HasForeignKey(d => d.IdUtilisateur2)
                .HasConstraintName("FK__echange__id_Util__42E1EEFE");

            entity.HasOne(d => d.Jouet1Navigation).WithMany(p => p.EchangeJouet1Navigations)
                .HasForeignKey(d => d.Jouet1)
                .HasConstraintName("FK__echange__jouet1__2CF2ADDF");

            entity.HasOne(d => d.Jouet2Navigation).WithMany(p => p.EchangeJouet2Navigations)
                .HasForeignKey(d => d.Jouet2)
                .HasConstraintName("FK__echange__jouet2__2DE6D218");

            entity.HasOne(d => d.ModeLivraison1Navigation).WithMany(p => p.EchangeModeLivraison1Navigations)
                .HasForeignKey(d => d.ModeLivraison1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__echange__mode_li__25518C17");

            entity.HasOne(d => d.ModeLivraison2Navigation).WithMany(p => p.EchangeModeLivraison2Navigations)
                .HasForeignKey(d => d.ModeLivraison2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__echange__mode_li__2739D489");

            entity.HasOne(d => d.ModePayUtilisateur1Navigation).WithMany(p => p.EchangeModePayUtilisateur1Navigations)
                .HasForeignKey(d => d.ModePayUtilisateur1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__echange__mode_pa__29221CFB");

            entity.HasOne(d => d.ModePayUtilisateur2Navigation).WithMany(p => p.EchangeModePayUtilisateur2Navigations)
                .HasForeignKey(d => d.ModePayUtilisateur2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__echange__mode_pa__2B0A656D");

            entity.HasOne(d => d.StatutNavigation).WithMany(p => p.Echanges)
                .HasForeignKey(d => d.Statut)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__echange__statut__2180FB33");
        });

        modelBuilder.Entity<EtatJouet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__etat_jou__3213E83F0F38AE2C");

            entity.ToTable("etat_jouet");

            entity.HasIndex(e => e.Nom, "UQ__etat_jou__DF90DC2C0EC89987").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__evaluati__3213E83F1BA99029");

            entity.ToTable("evaluations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Commentaire)
                .HasColumnType("text")
                .HasColumnName("commentaire");
            entity.Property(e => e.DateE)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_e");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.IdEvaluateur).HasColumnName("id_evaluateur");
            entity.Property(e => e.IdEvalue).HasColumnName("id_evalue");
            entity.Property(e => e.Note).HasColumnName("note");

            entity.HasOne(d => d.IdEvaluateurNavigation).WithMany(p => p.EvaluationIdEvaluateurNavigations)
                .HasForeignKey(d => d.IdEvaluateur)
                .HasConstraintName("FK__evaluatio__id_ev__489AC854");

            entity.HasOne(d => d.IdEvalueNavigation).WithMany(p => p.EvaluationIdEvalueNavigations)
                .HasForeignKey(d => d.IdEvalue)
                .HasConstraintName("FK__evaluatio__id_ev__41EDCAC5");
        });

        modelBuilder.Entity<FonctionUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__fonction__3213E83F2031C7B8");

            entity.ToTable("fonction_user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.RolesId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("roles_id");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.FonctionUsers)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__fonction___id_us__45BE5BA9");

            entity.HasOne(d => d.Roles).WithMany(p => p.FonctionUsers)
                .HasForeignKey(d => d.RolesId)
                .HasConstraintName("FK__fonction___roles__5CD6CB2B");
        });

        modelBuilder.Entity<Jouet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__jouets__3213E83FED6883B3");

            entity.ToTable("jouets");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AgeMax).HasColumnName("age_max");
            entity.Property(e => e.AgeMin).HasColumnName("age_min");
            entity.Property(e => e.Categorie).HasColumnName("categorie");
            entity.Property(e => e.Descriptions)
                .HasColumnType("text")
                .HasColumnName("descriptions");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.EtatId).HasColumnName("etat_id");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("nom");
            entity.Property(e => e.Prix).HasColumnType("money");
            entity.Property(e => e.Proprietaire).HasColumnName("proprietaire");

            entity.HasOne(d => d.CategorieNavigation).WithMany(p => p.Jouets)
                .HasForeignKey(d => d.Categorie)
                .HasConstraintName("FK__jouets__categori__619B8048");

            entity.HasOne(d => d.Etat).WithMany(p => p.Jouets)
                .HasForeignKey(d => d.EtatId)
                .HasConstraintName("FK__jouets__etat_id__628FA481");

            entity.HasOne(d => d.ProprietaireNavigation).WithMany(p => p.Jouets)
                .HasForeignKey(d => d.Proprietaire)
                .HasConstraintName("FK__jouets__propriet__4E53A1AA");
        });

        modelBuilder.Entity<JouetsPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__jouets_p__3213E83F82A3D82A");

            entity.ToTable("jouets_photos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Jouet).HasColumnName("jouet");
            entity.Property(e => e.Photo).HasColumnName("photo");

            entity.HasOne(d => d.JouetNavigation).WithMany(p => p.JouetsPhotos)
                .HasForeignKey(d => d.Jouet)
                .HasConstraintName("FK__jouets_ph__jouet__7E37BEF6");

            entity.HasOne(d => d.PhotoNavigation).WithMany(p => p.JouetsPhotos)
                .HasForeignKey(d => d.Photo)
                .HasConstraintName("FK__jouets_ph__photo__7F2BE32F");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__messages__3213E83F8E3DF644");

            entity.ToTable("messages");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contenu)
                .HasColumnType("text")
                .HasColumnName("contenu");
            entity.Property(e => e.DateLecture)
                .HasColumnType("datetime")
                .HasColumnName("date_lecture");
            entity.Property(e => e.DateM)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_m");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.IdDestinataire).HasColumnName("id_destinataire");
            entity.Property(e => e.IdExpediteur).HasColumnName("id_expediteur");
            entity.Property(e => e.Lu).HasColumnName("lu");

            entity.HasOne(d => d.IdDestinataireNavigation).WithMany(p => p.MessageIdDestinataireNavigations)
                .HasForeignKey(d => d.IdDestinataire)
                .HasConstraintName("FK__messages__id_des__4C6B5938");

            entity.HasOne(d => d.IdExpediteurNavigation).WithMany(p => p.MessageIdExpediteurNavigations)
                .HasForeignKey(d => d.IdExpediteur)
                .HasConstraintName("FK__messages__id_exp__47A6A41B");
        });

        modelBuilder.Entity<MessagesPhoto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__messages__3213E83F7F7DBCAC");

            entity.ToTable("messages_photos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Messages).HasColumnName("messages");
            entity.Property(e => e.Photo).HasColumnName("photo");

            entity.HasOne(d => d.MessagesNavigation).WithMany(p => p.MessagesPhotos)
                .HasForeignKey(d => d.Messages)
                .HasConstraintName("FK__messages___messa__797309D9");

            entity.HasOne(d => d.PhotoNavigation).WithMany(p => p.MessagesPhotos)
                .HasForeignKey(d => d.Photo)
                .HasConstraintName("FK__messages___photo__7A672E12");
        });

        modelBuilder.Entity<ModeLivraison>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__mode_liv__3213E83F51692FC3");

            entity.ToTable("mode_livraison");

            entity.HasIndex(e => e.Nom, "UQ__mode_liv__DF90DC2C045F2796").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("nom");
            entity.Property(e => e.Tarif)
                .HasDefaultValueSql("((0))")
                .HasColumnType("money")
                .HasColumnName("tarif");
        });

        modelBuilder.Entity<ModePayement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__mode_pay__3213E83FA08DD39D");

            entity.ToTable("mode_payement");

            entity.HasIndex(e => e.Nom, "UQ__mode_pay__DF90DC2C4AF9E3ED").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Newsletter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__newslett__3213E83F9796CD64");

            entity.ToTable("newsletters");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contenu)
                .HasColumnType("text")
                .HasColumnName("contenu");
            entity.Property(e => e.DateEnvoi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_envoi");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Objet)
                .HasMaxLength(50)
                .HasColumnName("objet");
            entity.Property(e => e.Outil)
                .HasMaxLength(50)
                .HasColumnName("outil");
        });

        modelBuilder.Entity<Photo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__photos__3213E83F83FDFEFC");

            entity.ToTable("photos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DatePublication)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_publication");
            entity.Property(e => e.DescriptionPhoto)
                .HasMaxLength(10)
                .HasColumnName("description_photo");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Format)
                .HasMaxLength(10)
                .HasColumnName("format");
            entity.Property(e => e.NomPhoto)
                .HasMaxLength(10)
                .HasColumnName("nom_photo");
            entity.Property(e => e.Taille).HasColumnName("taille");
            entity.Property(e => e.UrlP)
                .HasMaxLength(255)
                .HasColumnName("url_p");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83FBF269697");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Nom, "UQ__roles__DF90DC2C7214ABEE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Nom)
                .HasMaxLength(20)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<StatutUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__statut_u__3213E83F600544F1");

            entity.ToTable("statut_user");

            entity.HasIndex(e => e.Nom, "UQ__statut_u__DF90DC2CB1BF15AC").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Nom)
                .HasMaxLength(10)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<StatutsTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__statuts___3213E83FC31762C3");

            entity.ToTable("statuts_transaction");

            entity.HasIndex(e => e.Nom, "UQ__statuts___DF90DC2CA57B914A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Nom)
                .HasMaxLength(20)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3213E83FEA030BF3");

            entity.ToTable("utilisateurs");

            entity.HasIndex(e => e.Email, "UQ__tmp_ms_x__AB6E616486BCCAB8").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Adresse)
                .HasColumnType("text")
                .HasColumnName("adresse");
            entity.Property(e => e.DateCreation)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_creation");
            entity.Property(e => e.DateDerniereConnexion)
                .HasColumnType("datetime")
                .HasColumnName("date_derniere_connexion");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.MotDePasse).HasColumnName("mot_de_passe");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("nom");
            entity.Property(e => e.Points)
                .HasDefaultValueSql("((0))")
                .HasColumnName("points");
            entity.Property(e => e.Prenom)
                .HasMaxLength(50)
                .HasColumnName("prenom");
            entity.Property(e => e.QuatierUser)
                .HasMaxLength(50)
                .HasColumnName("quatier_user");
            entity.Property(e => e.Sel).HasColumnName("sel");
            entity.Property(e => e.StatutId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("statut_id");
            entity.Property(e => e.Telephone)
                .HasMaxLength(20)
                .HasColumnName("telephone");
            entity.Property(e => e.VilleUser)
                .HasMaxLength(50)
                .HasColumnName("ville_user");

            entity.HasOne(d => d.Statut).WithMany(p => p.Utilisateurs)
                .HasForeignKey(d => d.StatutId)
                .HasConstraintName("FK__utilisate__statu__498EEC8D");
        });

        modelBuilder.Entity<UtilisateursProfil>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__utilisat__3213E83FF0AB14D2");

            entity.ToTable("utilisateurs_profil");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Jouet).HasColumnName("jouet");
            entity.Property(e => e.Photo).HasColumnName("photo");

            entity.HasOne(d => d.JouetNavigation).WithMany(p => p.UtilisateursProfils)
                .HasForeignKey(d => d.Jouet)
                .HasConstraintName("FK__utilisate__jouet__02FC7413");

            entity.HasOne(d => d.PhotoNavigation).WithMany(p => p.UtilisateursProfils)
                .HasForeignKey(d => d.Photo)
                .HasConstraintName("FK__utilisate__photo__03F0984C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
