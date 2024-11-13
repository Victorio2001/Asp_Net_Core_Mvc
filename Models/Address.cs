using System.ComponentModel.DataAnnotations;

namespace NomDuProjet.Models;

public class Address
{
    [Key]
    public int Id_address { get; set; }
    public string street_address { get; set; }
    public string city_address { get; set; }
    public string state_address { get; set; }

    public Address()
    {
    }
}