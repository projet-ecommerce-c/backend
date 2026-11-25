using System;
using System.Collections.Generic;

namespace backend_ecommerce.Models;

public partial class Administrateur
{
    public int Id { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

    public virtual Utilisateur IdNavigation { get; set; } = null!;

    public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();
}
