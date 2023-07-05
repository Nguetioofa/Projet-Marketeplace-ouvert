using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models.Toys
{
    public class Toy
    {
        public int Id { get; set; }

        public string Nom { get; set; } = null!;

        public int Categorie { get; set; }

        public string? Descriptions { get; set; }

        public int AgeMin { get; set; }

        public int AgeMax { get; set; }

        public int EtatId { get; set; }

        public bool AcceptTroc { get; set; }

        public bool AcceptAchat { get; set; }

        public int Proprietaire { get; set; }

        public decimal Prix { get; set; }

        public bool EstPublier { get; set; }

        public virtual List<PhotoL>? listPhotos { get; set; }

    }
}
