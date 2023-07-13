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
				$(".commatireliste").append(
					"<li>" +
					"<div class='avatar'>" +
					"<a href='#'>" +
					"<img src='/AllaiaEcommerce/img/avatar1.jpg' alt=''>" +
					"</a>" +
					"</div>" +
					"<div class='comment_right clearfix'>" +
					"<div class='comment_info'>" +
					"Par <a href='#'>" + data.auteur + "</a><span>|</span>" + data.date + "<span>|</span><a href='#'><i class='icon-reply'></i></a>" +
					"</div>" +
					"<p>" + data.contenu + "</p>" +
					"</div>" +
					"</li>"
				);

				$("textarea[name='commentaire']").val("");
			}
		});
	});
});