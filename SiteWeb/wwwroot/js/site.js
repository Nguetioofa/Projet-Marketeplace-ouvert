$(document).ready(function () {
	var form = $("#comment-form");
	form.submit(function (event) {
		event.preventDefault();

		var id = $("input[name='Id']").val();
		var typeCommentaire = $("input[name='TypeCommantaire']").val();

		var commentaire = $("textarea[name='commentaire']").val();
		var token = $('input[name="__RequestVerificationToken"]').val();

		$.ajax({
			type: "POST",
			url: "/Commentaires/Create",
			data: { typeCommentaire: typeCommentaire, id: id, commentaire: commentaire, __RequestVerificationToken: token },
			success: function (data) {
				$(".card-body").append(
					"<div class='review_content'>" +
					"<div class='clearfix add_bottom_10'>" +
					"<em>" + data.date + "</em>" +
					"</div>" +
					"<h4>" + data.auteur + "</h4>" +
					"<p>" + data.contenu + "</p>" +
					"</div>"
				);

				$("textarea[name='commentaire']").val("");
			}
		});
	});
});