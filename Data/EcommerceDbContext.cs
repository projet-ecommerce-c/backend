using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using backend_ecommerce.Models;

namespace backend_ecommerce.Data;

public partial class EcommerceDbContext : DbContext
{
    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrateur> Administrateurs { get; set; }

    public virtual DbSet<Adress> Adresses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Commande> Commandes { get; set; }

    public virtual DbSet<LignesCommande> LignesCommandes { get; set; }

    public virtual DbSet<Paiement> Paiements { get; set; }

    public virtual DbSet<Produit> Produits { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrateur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("administrateurs_pkey");

            entity.ToTable("administrateurs");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Administrateur)
                .HasForeignKey<Administrateur>(d => d.Id)
                .HasConstraintName("administrateurs_id_fkey");
        });

        modelBuilder.Entity<Adress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("adresses_pkey");

            entity.ToTable("adresses");

            entity.HasIndex(e => e.ClientId, "idx_adresses_client");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CodePostal)
                .HasMaxLength(20)
                .HasColumnName("code_postal");
            entity.Property(e => e.NumeroMaison)
                .HasMaxLength(20)
                .HasColumnName("numero_maison");
            entity.Property(e => e.Pays)
                .HasMaxLength(100)
                .HasColumnName("pays");
            entity.Property(e => e.Rue)
                .HasMaxLength(255)
                .HasColumnName("rue");
            entity.Property(e => e.Ville)
                .HasMaxLength(100)
                .HasColumnName("ville");

            entity.HasOne(d => d.Client).WithMany(p => p.Adresses)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("adresses_client_id_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.HasIndex(e => e.Nom, "categories_nom_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AdministrateurId).HasColumnName("administrateur_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");

            entity.HasOne(d => d.Administrateur).WithMany(p => p.Categories)
                .HasForeignKey(d => d.AdministrateurId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("categories_administrateur_id_fkey");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("clients_pkey");

            entity.ToTable("clients");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.EstActif)
                .HasDefaultValue(true)
                .HasColumnName("est_actif");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Client)
                .HasForeignKey<Client>(d => d.Id)
                .HasConstraintName("clients_id_fkey");
        });

        modelBuilder.Entity<Commande>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("commandes_pkey");

            entity.ToTable("commandes");

            entity.HasIndex(e => e.Reference, "commandes_reference_key").IsUnique();

            entity.HasIndex(e => e.ClientId, "idx_commandes_client");

            entity.HasIndex(e => e.DateCommande, "idx_commandes_date");

            entity.HasIndex(e => e.Statut, "idx_commandes_statut");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AdministrateurId).HasColumnName("administrateur_id");
            entity.Property(e => e.AdresseLivraisonId).HasColumnName("adresse_livraison_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.DateCommande)
                .HasDefaultValueSql("now()")
                .HasColumnName("date_commande");
            entity.Property(e => e.MontantTotal)
                .HasPrecision(12, 2)
                .HasColumnName("montant_total");
            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .HasColumnName("reference");
            entity.Property(e => e.Statut)
                .HasMaxLength(20)
                .HasDefaultValueSql("'EnAttente'::character varying")
                .HasColumnName("statut");

            entity.HasOne(d => d.Administrateur).WithMany(p => p.Commandes)
                .HasForeignKey(d => d.AdministrateurId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("commandes_administrateur_id_fkey");

            entity.HasOne(d => d.AdresseLivraison).WithMany(p => p.Commandes)
                .HasForeignKey(d => d.AdresseLivraisonId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("commandes_adresse_livraison_id_fkey");

            entity.HasOne(d => d.Client).WithMany(p => p.Commandes)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("commandes_client_id_fkey");
        });

        modelBuilder.Entity<LignesCommande>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lignes_commande_pkey");

            entity.ToTable("lignes_commande");

            entity.HasIndex(e => e.CommandeId, "idx_lignes_commande");

            entity.HasIndex(e => new { e.CommandeId, e.ProduitId }, "lignes_commande_commande_id_produit_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CommandeId).HasColumnName("commande_id");
            entity.Property(e => e.PrixUnitaire)
                .HasPrecision(10, 2)
                .HasColumnName("prix_unitaire");
            entity.Property(e => e.ProduitId).HasColumnName("produit_id");
            entity.Property(e => e.Quantite).HasColumnName("quantite");

            entity.HasOne(d => d.Commande).WithMany(p => p.LignesCommandes)
                .HasForeignKey(d => d.CommandeId)
                .HasConstraintName("lignes_commande_commande_id_fkey");

            entity.HasOne(d => d.Produit).WithMany(p => p.LignesCommandes)
                .HasForeignKey(d => d.ProduitId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("lignes_commande_produit_id_fkey");
        });

        modelBuilder.Entity<Paiement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("paiements_pkey");

            entity.ToTable("paiements");

            entity.HasIndex(e => e.CommandeId, "paiements_commande_id_key").IsUnique();

            entity.HasIndex(e => e.Reference, "paiements_reference_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CommandeId).HasColumnName("commande_id");
            entity.Property(e => e.Mode)
                .HasMaxLength(20)
                .HasColumnName("mode");
            entity.Property(e => e.Montant)
                .HasPrecision(12, 2)
                .HasColumnName("montant");
            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .HasColumnName("reference");
            entity.Property(e => e.Statut)
                .HasMaxLength(20)
                .HasDefaultValueSql("'EnAttente'::character varying")
                .HasColumnName("statut");

            entity.HasOne(d => d.Commande).WithOne(p => p.Paiement)
                .HasForeignKey<Paiement>(d => d.CommandeId)
                .HasConstraintName("paiements_commande_id_fkey");
        });

        modelBuilder.Entity<Produit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("produits_pkey");

            entity.ToTable("produits");

            entity.HasIndex(e => e.CategorieId, "idx_produits_categorie");

            entity.HasIndex(e => e.Nom, "idx_produits_nom");

            entity.HasIndex(e => e.Prix, "idx_produits_prix");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AdministrateurId).HasColumnName("administrateur_id");
            entity.Property(e => e.CategorieId).HasColumnName("categorie_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Image)
                .HasMaxLength(500)
                .HasColumnName("image");
            entity.Property(e => e.Nom)
                .HasMaxLength(200)
                .HasColumnName("nom");
            entity.Property(e => e.Prix)
                .HasPrecision(10, 2)
                .HasColumnName("prix");
            entity.Property(e => e.QuantiteDisponible).HasColumnName("quantite_disponible");

            entity.HasOne(d => d.Administrateur).WithMany(p => p.Produits)
                .HasForeignKey(d => d.AdministrateurId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("produits_administrateur_id_fkey");

            entity.HasOne(d => d.Categorie).WithMany(p => p.Produits)
                .HasForeignKey(d => d.CategorieId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("produits_categorie_id_fkey");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("utilisateurs_pkey");

            entity.ToTable("utilisateurs");

            entity.HasIndex(e => e.Email, "utilisateurs_email_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.MotDePasseHash)
                .HasMaxLength(255)
                .HasColumnName("mot_de_passe_hash");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(100)
                .HasColumnName("prenom");
            entity.Property(e => e.Telephone)
                .HasMaxLength(20)
                .HasColumnName("telephone");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
