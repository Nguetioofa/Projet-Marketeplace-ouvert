using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary.Models.Commentaires
{
	public class ModelPatialCommentaire
	{
		public int Id { get; set; }
		public string TypeCommantaire { get; set; }
		public virtual List<CommentaireL>? listCommentaires { get; set; }
	}
}
