using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models.Toys
{
    public class ToyBoxModel
    {
        public int Id { get; set; }
        public string Nom { get; set; } = null!;
        public int Categorie { get; set; }
        public bool AcceptTroc { get; set; }
        public bool AcceptAchat { get; set; }
        public decimal Prix { get; set; }
        public virtual PhotoL? Photo { get; set; }

    }
}
