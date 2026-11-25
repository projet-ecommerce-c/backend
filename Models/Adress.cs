using System;
using System.Collections.Generic;

namespace backend_ecommerce.Models;

public partial class Adress
{
    public int Id { get; set; }

    public string Rue { get; set; } = null!;

    public string NumeroMaison { get; set; } = null!;

    public string CodePostal { get; set; } = null!;

    public string Ville { get; set; } = null!;

    public string Pays { get; set; } = null!;

    public int ClientId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();
}
