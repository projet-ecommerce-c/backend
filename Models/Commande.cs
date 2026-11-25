using System;
using System.Collections.Generic;

namespace backend_ecommerce.Models;

public partial class Commande
{
    public int Id { get; set; }

    public DateTime DateCommande { get; set; }

    public string Reference { get; set; } = null!;

    public string Statut { get; set; } = null!;

    public decimal MontantTotal { get; set; }

    public int ClientId { get; set; }

    public int? AdresseLivraisonId { get; set; }

    public int? AdministrateurId { get; set; }

    public virtual Administrateur? Administrateur { get; set; }

    public virtual Adress? AdresseLivraison { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<LignesCommande> LignesCommandes { get; set; } = new List<LignesCommande>();

    public virtual Paiement? Paiement { get; set; }
}
