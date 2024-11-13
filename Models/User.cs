using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace NomDuProjet.Models;

public class User : IdentityUser
{ 
    [Key]
    public int Id_utilisateur { get; set; }
    public string prenom_utilisateur { get; set; }
    public string nom_utilisateur { get; set; }
    public string mail_utilisateur { get; set; }
    public string[] allergie_utilisateur { get; set; }
    public string[] handicap_utilisateur { get; set; }
    public bool valide_compte_utilisateur { get; set; }
    [DataType(DataType.Date)]
    public DateTime created_at_utilisateur { get; set; } = DateTime.Today;
    [DataType(DataType.Date)]
    public DateTime updated_at_utilisateur { get; set; } = DateTime.Today;
    [ForeignKey("Address")]
    public int addressId { get; set; }
    public Address? Address_User { get; set; }
}