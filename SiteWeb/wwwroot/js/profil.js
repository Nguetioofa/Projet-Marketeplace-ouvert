

$(document).ready(function () {
	var form = $("#sendmessageuser");
	form.submit(function (event) {
		event.preventDefault();

		var id = $("input[name='Id']").val();
		var message = $("input[name='message']").val();
		var token = $('input[name="__RequestVerificationToken"]').val();

		$.ajax({
			type: "POST",
			url: "/Messages/EnvoyerMessage",
			data: { idUser: id, message: message,  __RequestVerificationToken: token },
			success: function (data) {
				$("#messageeuure").append(
					"<span  class=\"text-danger\"></span>"
				);

				$("input[name='message']").val("");
			}
		});
	});
});


//list jouets
$(document).ready(function () {


	$('#jouetsTab').click(function () {
		$('#listejouets').empty();
		var id = $("input[name='Id']").val();

		$.ajax({
			url: '/Jouets/GetJouetProfil',
			type: 'GET',
			data: { id: id },
			success: function (data) {
				var html = '';
				for (var i = 0; i < data.length; i++) {
					var jouet = data[i];
					html += '<div class="col-6 col-md-4 col-xl-3">';
					html += '<div class="grid_item">';
					html += '<span class="ribbon new">Nouveau</span>';
					html += '<figure>';
					html += '<a href="/Jouets/Details/' + jouet.id + '">';
					if (jouet.photo != null) {
						html += '<img class="img-fluid lazy" src="' + jouet.photo.urlP + '" alt="' + jouet.nom + '">';
					} else {
						html += '<img class="img-fluid lazy" src="/AllaiaEcommerce/img/products/product_placeholder_square_medium.jpg" alt="' + jouet.nom + '">';
					}
					html += '</a>';
					html += '</figure>';
					html += '<a href="/Jouets/Details/' + jouet.Id + '">';
					html += '<h3>' + jouet.nom + '</h3>';
					html += '</a>';
					html += '<div class="price_box">';
					html += '<span class="new_price">' + jouet.prix + ' Fcfa</span>';
					html += '</div>';
					html += '<ul>';
					if (jouet.acceptAchat) {
						html += '<li><a href="#0" class="tooltip-1" data-bs-toggle="tooltip" data-bs-placement="left" title="Achater"><i class="ti-shopping-cart"></i><span>Achater</span></a></li>';
					}
					if (jouet.acceptTroc) {
						html += '<li><a href="#0" class="tooltip-1" data-bs-toggle="tooltip" data-bs-placement="left" title="Echanger"><i class="ti-control-shuffle"></i><span>Echanger</span></a></li>';
					}
					html += '</ul>';
					html += '</div>';
					html += '</div>';
				}

				$('#listejouets').append(html);
			}
		});
	});

});


$(document).ready(function () {


	$('#annoncesTab').click(function () {
		$('#listeAnnonce').empty();

		var id = $("input[name='Id']").val();

		$.ajax({
			url: '/Annonces/AnnoncesUtilisateur',
			type: 'GET',
			data: { id: id },
			success: function (data) {
				var html = '';
				for (var i = 0; i < data.length; i++) {
					var annonce = data[i];
					html += '<div class="row row_item">';
					html += '<div class="col-sm-4">';
					html += '<figure>';
					html += '<a href="/Annonces/Details/' + annonce.id + '">';
					if (annonce.photo == null) {
						html += '<img class="img-fluid lazy" src="/AllaiaEcommerce/img/products/product_placeholder_square_medium.jpg" data-src="img/products/shoes/6.jpg" alt="">';
					} else {
						html += '<img class="img-fluid lazy" src="' + annonce.photo.urlP + '" alt="' + annonce.photo.nomPhoto + '">';
					}
					html += '</a>';
					html += '</figure>';
					html += '</div>';
					html += '<div class="col-sm-8">';
					html += '<ul>';
					html += '<li>';
					html += '<div class="d-inline-block mb-2 text-primary">';
					html += '<a href="/Utilisateurs/Profil/' + annonce.idUtilisateur + '">' + annonce.utilisateur.nom + ' ' + annonce.utilisateur.prenom + '</a>';
					html += '</div>';
					html += '</li>';
					html += '<li>' + annonce.dateAnnonce + '</li>';
					html += '</ul>';
					html += '<a href="/Annonces/Details/' + annonce.id + '">';
					html += '<h3>' + annonce.titre + '</h3>';
					html += '</a>';
					html += '<p>';
					if (annonce.descriptionAnnonce.length > 238) {
						html += annonce.descriptionAnnonce.substring(0, 237);
						html += '<a href="/Annonces/Details/' + annonce.id + '" class="stretched-link text-primary"> ...voir plus</a>';
					} else {
						html += annonce.descriptionAnnonce;
					}
					html += '</p>';
					html += '<ul>';
					html += '<li>';
					html += '<a class="btn_1" href="/Utilisateurs/Profil/' + annonce.idUtilisateur + '">Contacter</a>';
					html += '</li>';
					html += '<li>';
					html += '<a class="btn_1" href="/Annonces/Details/' + annonce.id + '">Commenter</a>';
					html += '</li>';
					html += '</ul>';
					html += '</div>';
					html += '</div>';
				}
				// Ajoutez le HTML généré à la page
				$('#listeAnnonce').html(html);
			}
		});

	});

});