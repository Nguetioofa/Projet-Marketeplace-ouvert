using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary.Models.Achats;
using ModelsLibrary.Models.Annonces;
using ModelsLibrary.Models.Echanges;
using ModelsLibrary.Models.Toys;

namespace ModelsLibrary.Models.Users
{
    public class UserProfil
	{
		public UtilisateurL? utilisateur { get; set; }
		public virtual StatutUserL statutUserL { get; set; }

		///public virtual List<>

	}
}
