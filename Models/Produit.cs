using System;
using System.Collections.Generic;

namespace backend_ecommerce.Models;

public partial class Produit
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string? Description { get; set; }

    public int QuantiteDisponible { get; set; }

    public decimal Prix { get; set; }

    public string? Image { get; set; }

    public int CategorieId { get; set; }

    public int AdministrateurId { get; set; }

    public virtual Administrateur Administrateur { get; set; } = null!;

    public virtual Category Categorie { get; set; } = null!;

    public virtual ICollection<LignesCommande> LignesCommandes { get; set; } = new List<LignesCommande>();
}
