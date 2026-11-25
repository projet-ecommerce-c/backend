using System;
using System.Collections.Generic;

namespace backend_ecommerce.Models;

public partial class LignesCommande
{
    public int Id { get; set; }

    public int Quantite { get; set; }

    public decimal PrixUnitaire { get; set; }

    public int CommandeId { get; set; }

    public int ProduitId { get; set; }

    public virtual Commande Commande { get; set; } = null!;

    public virtual Produit Produit { get; set; } = null!;
}
