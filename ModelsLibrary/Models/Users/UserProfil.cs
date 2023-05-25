using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models.Users
{
	public class UserProfil
	{
		public UtilisateurL? utilisateur { get; set; }
		public virtual StatutUserL statutUserL { get; set; }
		public virtual List<Jouet> jouetList { get; set;}
		public virtual List<Annonce> annonceList { get; set; }
		public virtual List<Echange> echangeList { get; set; }
		public virtual List<Achat> achatList { get; set; }

	}
}
