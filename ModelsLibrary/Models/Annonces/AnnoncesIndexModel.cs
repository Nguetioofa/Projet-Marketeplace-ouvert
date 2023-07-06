using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models.Annonces
{
	public class AnnoncesIndexModel
	{
		public int Id { get; set; }

		public string Titre { get; set; } = null!;

		public string DescriptionAnnonce { get; set; } = null!;

		public DateTime DateAnnonce { get; set; }

		public int? IdUtilisateur { get; set; }

		public bool EstSupprimer { get; set; }
		public virtual List<AnnonceL>? listAnnonces { get; set; }
	}
}
