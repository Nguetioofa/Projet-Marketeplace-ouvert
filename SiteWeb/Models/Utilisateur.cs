using Microsoft.AspNetCore.Routing;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SiteWeb.Models
{
    public class Utilisateur
    {
        //[Required(ErrorMessage = "Le  est requis.")]
        //[Key]
        public int Id { get; set; } = 0;

        [Required(ErrorMessage = "Le nom est requis.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Le nom doit avoir entre 3 et 50 caractères.")]
        public string? Nom { get; set; }

        [StringLength(50)]
        public string? Prenom { get; set; }

        [Required(ErrorMessage = "L'Email est requis.")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [PasswordPropertyText]
        public string MotDePasse { get; set; } = null!;

        [Required(ErrorMessage = "Le nom est requis.")]
        [PasswordPropertyText]
        public string Sel { get; set; } = null!;

        [Required(ErrorMessage = "Le nom est requis.")]
        [StringLength(9)]
        [RegularExpression(@"^[62]\d {8}$", ErrorMessage = "Le numéro doit contenir 9 chiffres et commencer par 6 ou 2.")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; } = null!;

        [StringLength(50)]
        public string? Adresse { get; set; }

        [StringLength(50)]
        public string? VilleUser { get; set; }

        [StringLength(50)]
        public string? QuatierUser { get; set; }

        
        public int? Points { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateCreation { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DateDerniereConnexion { get; set; }        
        public int StatutId { get; set; }
    }
}
