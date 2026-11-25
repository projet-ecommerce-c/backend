using System;
using System.Collections.Generic;

namespace backend_ecommerce.Models;

public partial class Utilisateur
{
    public int Id { get; set; }

    public string Nom { get; set; } = null!;

    public string Prenom { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string MotDePasseHash { get; set; } = null!;

    public string? Telephone { get; set; }

    public virtual Administrateur? Administrateur { get; set; }

    public virtual Client? Client { get; set; }
}
