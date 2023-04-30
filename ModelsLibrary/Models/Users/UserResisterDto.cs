using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models.Users
{
    public class UserResisterDto
    {

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

        [Required(ErrorMessage = "Le numero de telephone est requis.")]
        [StringLength(9)]
        [RegularExpression(@"^[62][0-9]{8}$", ErrorMessage = "Le numéro doit contenir 9 chiffres et commencer par 6 ou 2.")]
        [DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; } = null!;

        [StringLength(50)]
        public string? Adresse { get; set; }

        [StringLength(50)]
        public string? VilleUser { get; set; }

        [StringLength(50)]
        public string? QuatierUser { get; set; }

    }
}
