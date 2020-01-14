(function ($) {

	"use strict";

	var fullHeight = function () {

		$('.js-fullheight').css('height', $(window).height());
		$(window).resize(function () {
			$('.js-fullheight').css('height', $(window).height());
		});

	};
	fullHeight();

	$('#sidebarCollapse2').on('click', function () {
		$('#sidebar').toggleClass('active');
	});
	$('input[type=text], input[type=password], input[type=email], input[type=url], input[type=tel], input[type=number], input[type=search], input[type=date], input[type=time], textarea').each(function () {
		if ($(this).val()) {
			$(this).siblings('label').addClass('active');
		}
		else {
			$(this).siblings('label').removeClass('active');
		}
	})

	$('input[type=text], input[type=password], input[type=email], input[type=url], input[type=tel], input[type=number], input[type=search], input[type=date], input[type=time], textarea').on('input', function () {
		fireLabelClassAvtive();
	});
	$('#Pais').on('input', function () {
		setTimeout(function () {
			if ($('#Pais').val() == '')
				$('.aspValidationFor').text('El campo "País" es obligatorio');
			else
				$('.aspValidationFor').text('');
		}, 200);
	});

	// ===== Scroll to Top ==== 
	$(window).scroll(function () {
		if ($(this).scrollTop() >= 50) {        // If page is scrolled more than 50px
			$('#return-to-top').fadeIn(200);    // Fade in the arrow
		} else {
			$('#return-to-top').fadeOut(200);   // Else fade out the arrow
		}
	});
	$('#return-to-top').click(function () {      // When arrow is clicked
		$('body,html').animate({
			scrollTop: 0                       // Scroll to top of body
		}, 500);
	});
})(jQuery);


function fireLabelClassAvtive() {
	$('input[type=text], input[type=password], input[type=email], input[type=url], input[type=tel], input[type=number], input[type=search], input[type=date], input[type=time], textarea').each(function () {
		if ($(this).val()) {
			$(this).siblings('label').addClass('active');
		}
		else {
			$(this).siblings('label').removeClass('active');
		}
	})
}
