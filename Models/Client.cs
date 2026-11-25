using System;
using System.Collections.Generic;

namespace backend_ecommerce.Models;

public partial class Client
{
    public int Id { get; set; }

    public bool EstActif { get; set; }

    public virtual ICollection<Adress> Adresses { get; set; } = new List<Adress>();

    public virtual ICollection<Commande> Commandes { get; set; } = new List<Commande>();

    public virtual Utilisateur IdNavigation { get; set; } = null!;
}
