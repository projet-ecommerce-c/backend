using System;
using System.Collections.Generic;

namespace backend_ecommerce.Models;

public partial class Paiement
{
    public int Id { get; set; }

    public string Reference { get; set; } = null!;

    public decimal Montant { get; set; }

    public string Statut { get; set; } = null!;

    public string Mode { get; set; } = null!;

    public int CommandeId { get; set; }

    public virtual Commande Commande { get; set; } = null!;
}
