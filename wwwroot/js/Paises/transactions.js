$(document).ready(function () {
	buildPaginer();
});

function buildPaginer() {
	$.ajax({
		url: '/Paises/getPagedList/',
		type: 'POST',
		async: true,
		dataType: 'json',
		contentType: 'application/json; charset=utf-8',
		success: function (data) {
			$(".page-navigation ul li").remove();
			console.log('data: ' + data);
			var countPage = 0;
			if (data.hasPreviousPage == true) {
				var previousPage = data.page - 1;
				$(".page-navigation ul").append('<li i class="page-item"><a class="page-link" href="Paises?page=' + previousPage + '"><i class="fas fa-chevron-left" style="color:#4285f4;font-size:18px;"></i></a></li> ');
			}
			for (var i = 0; i < data.pageCount; i++) {
				countPage++;
				_page = countPage;
				_pageSize = data.pageSize;
				if (data.page == i + 1) {
					CurrentPage = i + 1;
					$(".page-navigation ul").append('<li i class="page-item active"><a class="page-link" href="Paises?page=' + _page + '">' + _page + '</a></li>');
				}
				else
					$(".page-navigation ul").append('<li i class="page-item"><a class="page-link" href="Paises?page=' + _page + '">' + _page + '</a></li>');
			}
			if (data.hasNextPage == true) {
				var nextPage = data.page + 1;
				$(".page-navigation ul").append('<li i class="page-item"><a class="page-link" href="Paises?page=' + nextPage + '"><i class="fas fa-chevron-right" style="color:#4285f4;font-size:18px;"></i></a></li> ');
			}
		}
	});
}

function deleteRecord(PaisID) {
	Swal.fire({
		title: '¿Desea eliminar el país?',
		text: "¡No podrás revertir esto!",
		icon: 'warning',
		showCancelButton: true,
		confirmButtonColor: '#3085d6',
		cancelButtonColor: '#d33',
		confirmButtonText: 'Si, Elimínalo!'
	}).then((result) => {
		if (result.value) {
			$.ajax({
				type: "DELETE",
				url: "/Paises/Delete",
				data: "id=" + PaisID,
				dataType: "json",
				success: function (response) {
					if (response) {
						let timerInterval
						Swal.fire({
							title: 'País Eliminado!',
							html: 'El registro ha sido eliminado satisfactoriamente',
							icon: 'success',
							timer: 3000,
							timerProgressBar: true,
							onBeforeOpen: () => { Swal.showLoading() },
							onClose: () => {
								clearInterval(timerInterval);
								location.href = '/Paises'
							}
						})
					}
					else {
						Swal.fire('Error!', 'Ha ocurrido un error al eliminar el pais, por favor intente de nuevo.', 'Error')
					}
				}
			});
		}
	});
}