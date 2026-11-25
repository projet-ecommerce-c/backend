using System;
using System.Collections.Generic;

namespace backend_ecommerce.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string? Description { get; set; }

    public int AdministrateurId { get; set; }

    public virtual Administrateur Administrateur { get; set; } = null!;

    public virtual ICollection<Produit> Produits { get; set; } = new List<Produit>();
}
