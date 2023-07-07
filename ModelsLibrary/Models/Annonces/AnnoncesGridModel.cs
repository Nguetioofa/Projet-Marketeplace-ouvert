using ModelsLibrary.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models.Annonces
{
	public class AnnoncesGridModel
	{
		public int Id { get; set; }

		public string Titre { get; set; } = null!;

		public string DescriptionAnnonce { get; set; } = null!;

		public DateTime DateAnnonce { get; set; }

		public int? IdUtilisateur { get; set; }
		public virtual UtilisateurL utilisateur { get; set; }
		public bool EstSupprimer { get; set; }
		public virtual PhotoL Photo { get; set; }
	}
}
