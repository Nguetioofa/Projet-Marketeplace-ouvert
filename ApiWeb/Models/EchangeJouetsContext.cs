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

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<ModeLivraison> ModeLivraisons { get; set; }

    public virtual DbSet<ModePayement> ModePayements { get; set; }

    public virtual DbSet<Newsletter> Newsletters { get; set; }

    public virtual DbSet<Photo> Photos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<StatutUser> StatutUsers { get; set; }

    public virtual DbSet<StatutsTransaction> StatutsTransactions { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Abonnement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__abonneme__3213E83FADFE3A47");

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
                .HasConstraintName("FK__abonnemen__utili__17F790F9");
        });

        modelBuilder.Entity<Achat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__achat__3213E83F996AC094");

            entity.ToTable("achat");

            entity.HasIndex(e => e.Reference, "UQ__achat__FD90DA99E60DC756").IsUnique();

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
                .HasConstraintName("FK__achat__acheteur__76969D2E");

            entity.HasOne(d => d.JouetNavigation).WithMany(p => p.Achats)
                .HasForeignKey(d => d.Jouet)
                .HasConstraintName("FK__achat__jouet__787EE5A0");

            entity.HasOne(d => d.ModeLivraisonNavigation).WithMany(p => p.Achats)
                .HasForeignKey(d => d.ModeLivraison)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__achat__mode_livr__797309D9");

            entity.HasOne(d => d.ModePayNavigation).WithMany(p => p.Achats)
                .HasForeignKey(d => d.ModePay)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__achat__mode_pay__7A672E12");

            entity.HasOne(d => d.StatutNavigation).WithMany(p => p.Achats)
                .HasForeignKey(d => d.Statut)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__achat__statut__7B5B524B");

            entity.HasOne(d => d.VendeurNavigation).WithMany(p => p.AchatVendeurNavigations)
                .HasForeignKey(d => d.Vendeur)
                .HasConstraintName("FK__achat__vendeur__778AC167");
        });

        modelBuilder.Entity<Annonce>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__annonce__3213E83F21345681");

            entity.ToTable("annonce");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateAnnonce)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date_annonce");
            entity.Property(e => e.DescriptionAnnonce)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description_annonce");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.IdUtilisateur).HasColumnName("id_Utilisateur");
            entity.Property(e => e.Titre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("titre");

            entity.HasOne(d => d.IdUtilisateurNavigation).WithMany(p => p.Annonces)
                .HasForeignKey(d => d.IdUtilisateur)
                .HasConstraintName("FK__annonce__id_Util__6383C8BA");
        });

        modelBuilder.Entity<CategorieJouet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83FF71DFE2A");

            entity.ToTable("categorie_jouet");

            entity.HasIndex(e => e.Nom, "UQ__categori__DF90DC2CB8D2FDC7").IsUnique();

            entity.HasIndex(e => e.Nom, "idx_nom_categorie_jouet");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Commentaire>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__commenta__3213E83F9E458403");

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
            entity.Property(e => e.IdJouet).HasColumnName("id_jouet");

            entity.HasOne(d => d.IdAnnonceNavigation).WithMany(p => p.Commentaires)
                .HasForeignKey(d => d.IdAnnonce)
                .HasConstraintName("FK__commentai__id_an__2A164134");

            entity.HasOne(d => d.IdAuteurNavigation).WithMany(p => p.Commentaires)
                .HasForeignKey(d => d.IdAuteur)
                .HasConstraintName("FK__commentai__id_au__29221CFB");

            entity.HasOne(d => d.IdJouetNavigation).WithMany(p => p.Commentaires)
                .HasForeignKey(d => d.IdJouet)
                .HasConstraintName("FK__commentai__id_jo__2B0A656D");
        });

        modelBuilder.Entity<Echange>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__echange__3213E83F038B5F48");

            entity.ToTable("echange");

            entity.HasIndex(e => e.Reference, "UQ__echange__FD90DA999BBE9A9B").IsUnique();

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
                .HasConstraintName("FK__echange__id_Util__03F0984C");

            entity.HasOne(d => d.IdUtilisateur2Navigation).WithMany(p => p.EchangeIdUtilisateur2Navigations)
                .HasForeignKey(d => d.IdUtilisateur2)
                .HasConstraintName("FK__echange__id_Util__04E4BC85");

            entity.HasOne(d => d.Jouet1Navigation).WithMany(p => p.EchangeJouet1Navigations)
                .HasForeignKey(d => d.Jouet1)
                .HasConstraintName("FK__echange__jouet1__0D7A0286");

            entity.HasOne(d => d.Jouet2Navigation).WithMany(p => p.EchangeJouet2Navigations)
                .HasForeignKey(d => d.Jouet2)
                .HasConstraintName("FK__echange__jouet2__0E6E26BF");

            entity.HasOne(d => d.ModeLivraison1Navigation).WithMany(p => p.EchangeModeLivraison1Navigations)
                .HasForeignKey(d => d.ModeLivraison1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__echange__mode_li__05D8E0BE");

            entity.HasOne(d => d.ModeLivraison2Navigation).WithMany(p => p.EchangeModeLivraison2Navigations)
                .HasForeignKey(d => d.ModeLivraison2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__echange__mode_li__07C12930");

            entity.HasOne(d => d.ModePayUtilisateur1Navigation).WithMany(p => p.EchangeModePayUtilisateur1Navigations)
                .HasForeignKey(d => d.ModePayUtilisateur1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__echange__mode_pa__09A971A2");

            entity.HasOne(d => d.ModePayUtilisateur2Navigation).WithMany(p => p.EchangeModePayUtilisateur2Navigations)
                .HasForeignKey(d => d.ModePayUtilisateur2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__echange__mode_pa__0B91BA14");

            entity.HasOne(d => d.StatutNavigation).WithMany(p => p.Echanges)
                .HasForeignKey(d => d.Statut)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__echange__statut__02084FDA");
        });

        modelBuilder.Entity<EtatJouet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__etat_jou__3213E83F9658810F");

            entity.ToTable("etat_jouet");

            entity.HasIndex(e => e.Nom, "UQ__etat_jou__DF90DC2C42F7B4BF").IsUnique();

            entity.HasIndex(e => e.Nom, "idx_nom_etat_jouet");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__evaluati__3213E83F2C52EE81");

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
                .HasConstraintName("FK__evaluatio__id_ev__236943A5");

            entity.HasOne(d => d.IdEvalueNavigation).WithMany(p => p.EvaluationIdEvalueNavigations)
                .HasForeignKey(d => d.IdEvalue)
                .HasConstraintName("FK__evaluatio__id_ev__245D67DE");
        });

        modelBuilder.Entity<FonctionUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__fonction__3213E83FD5088352");

            entity.ToTable("fonction_user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.RolesId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("roles_id");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.FonctionUsers)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__fonction___id_us__14270015");

            entity.HasOne(d => d.Roles).WithMany(p => p.FonctionUsers)
                .HasForeignKey(d => d.RolesId)
                .HasConstraintName("FK__fonction___roles__1332DBDC");
        });

        modelBuilder.Entity<Jouet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__jouets__3213E83F0DFF07F4");

            entity.ToTable("jouets");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AcceptAchat)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("accept_achat");
            entity.Property(e => e.AcceptTroc)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("accept_troc");
            entity.Property(e => e.AgeMax).HasColumnName("age_max");
            entity.Property(e => e.AgeMin).HasColumnName("age_min");
            entity.Property(e => e.Categorie).HasColumnName("categorie");
            entity.Property(e => e.Descriptions)
                .HasColumnType("text")
                .HasColumnName("descriptions");
            entity.Property(e => e.EstPublier)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("est_publier");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.EtatId).HasColumnName("etat_id");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("nom");
            entity.Property(e => e.Prix).HasColumnType("money");
            entity.Property(e => e.Proprietaire).HasColumnName("proprietaire");

            entity.HasOne(d => d.CategorieNavigation).WithMany(p => p.Jouets)
                .HasForeignKey(d => d.Categorie)
                .HasConstraintName("FK__jouets__categori__59FA5E80");

            entity.HasOne(d => d.Etat).WithMany(p => p.Jouets)
                .HasForeignKey(d => d.EtatId)
                .HasConstraintName("FK__jouets__etat_id__5AEE82B9");

            entity.HasOne(d => d.ProprietaireNavigation).WithMany(p => p.Jouets)
                .HasForeignKey(d => d.Proprietaire)
                .HasConstraintName("FK__jouets__propriet__5DCAEF64");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__messages__3213E83F271E4DA5");

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
                .HasConstraintName("FK__messages__id_des__693CA210");

            entity.HasOne(d => d.IdExpediteurNavigation).WithMany(p => p.MessageIdExpediteurNavigations)
                .HasForeignKey(d => d.IdExpediteur)
                .HasConstraintName("FK__messages__id_exp__68487DD7");
        });

        modelBuilder.Entity<ModeLivraison>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__mode_liv__3213E83F48139600");

            entity.ToTable("mode_livraison");

            entity.HasIndex(e => e.Nom, "UQ__mode_liv__DF90DC2CA9F8ADE9").IsUnique();

            entity.HasIndex(e => e.Nom, "idx_nom_mode_livraison");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<ModePayement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__mode_pay__3213E83F1AD9BECE");

            entity.ToTable("mode_payement");

            entity.HasIndex(e => e.Nom, "UQ__mode_pay__DF90DC2C21FDE779").IsUnique();

            entity.HasIndex(e => e.Nom, "idx_nom_mode_payement");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Newsletter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__newslett__3213E83F682EA624");

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
            entity.HasKey(e => e.Id).HasName("PK__photos__3213E83F036A0BB1");

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
            entity.Property(e => e.Jouet).HasColumnName("jouet");
            entity.Property(e => e.Messages).HasColumnName("messages");
            entity.Property(e => e.Annonce).HasColumnName("annonce");

			entity.Property(e => e.NomPhoto)
                .HasMaxLength(10)
                .HasColumnName("nom_photo");
            entity.Property(e => e.Profil).HasColumnName("profil");
            entity.Property(e => e.Taille).HasColumnName("taille");
            entity.Property(e => e.UrlP)
                .HasMaxLength(255)
                .HasColumnName("url_p");

            entity.HasOne(d => d.JouetNavigation).WithMany(p => p.Photos)
                .HasForeignKey(d => d.Jouet)
                .HasConstraintName("FK__photos__jouet__6FE99F9F");

            entity.HasOne(d => d.MessagesNavigation).WithMany(p => p.Photos)
                .HasForeignKey(d => d.Messages)
                .HasConstraintName("FK__photos__messages__6EF57B66");

            entity.HasOne(d => d.AnnonceNavigation).WithMany(p => p.Photos)
            .HasForeignKey(d => d.Annonce)
            .HasConstraintName("FK__photos__messages__6WF57B49");

			entity.HasOne(d => d.ProfilNavigation).WithMany(p => p.Photos)
                .HasForeignKey(d => d.Profil)
                .HasConstraintName("FK__photos__profil__70DDC3D8");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__roles__3213E83F6C7E3C62");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Nom, "UQ__roles__DF90DC2C57D885E7").IsUnique();

            entity.HasIndex(e => e.Nom, "idx_nom_roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Nom)
                .HasMaxLength(20)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<StatutUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__statut_u__3213E83FB1DFA212");

            entity.ToTable("statut_user");

            entity.HasIndex(e => e.Nom, "UQ__statut_u__DF90DC2C86315AE8").IsUnique();

            entity.HasIndex(e => e.Nom, "idx_nom_statut_user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Nom)
                .HasMaxLength(10)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<StatutsTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__statuts___3213E83F8D608CF5");

            entity.ToTable("statuts_transaction");

            entity.HasIndex(e => e.Nom, "UQ__statuts___DF90DC2CBB83AF50").IsUnique();

            entity.HasIndex(e => e.Nom, "idx_nom_statuts_transaction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstSupprimer).HasColumnName("est_supprimer");
            entity.Property(e => e.Nom)
                .HasMaxLength(20)
                .HasColumnName("nom");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__utilisat__3213E83F6200ACED");

            entity.ToTable("utilisateurs", tb => tb.HasTrigger("trg_ajouter_role"));

            entity.HasIndex(e => e.Email, "UQ__utilisat__AB6E61641F05D5EE").IsUnique();

            entity.HasIndex(e => e.Email, "idx_email");

            entity.HasIndex(e => e.Nom, "idx_nom_utilisateurs");

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
                .HasConstraintName("FK__utilisate__statu__5629CD9C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
