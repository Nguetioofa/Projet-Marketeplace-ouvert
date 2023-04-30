using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary.Models.Users
{
    public class UtilisateurL
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom est requis.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Le nom doit avoir entre 3 et 50 caractères.")]
        public string? Nom { get; set; }

        [StringLength(50)]
        public string? Prenom { get; set; }

        [Required(ErrorMessage = "L'Email est requis.")]
        [StringLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Le numero de telephone est requis.")]
        [StringLength(9)]
        [RegularExpression(@"^[62][0-9]{8}$", ErrorMessage = "Le numéro doit contenir 9 chiffres et commencer par 6 ou 2.")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; } = null!;

        [StringLength(50)]
        public string? Adresse { get; set; }

        [Display(Name = "Ville")]
        [StringLength(50)]
        public string? VilleUser { get; set; }

        [Display(Name = "Quatier")]
        [StringLength(50)]
        public string? QuatierUser { get; set; }

        public int? Points { get; set; }

        [Display(Name = "Date de creation")]
        [DataType(DataType.DateTime)]
        public DateTime DateCreation { get; set; }
        [Display(Name = "Derniere connexion")]
        [DataType(DataType.DateTime)]
        public DateTime? DateDerniereConnexion { get; set; }

        public int? StatutId { get; set; }

    }
}
