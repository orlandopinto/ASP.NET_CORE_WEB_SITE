var typeMesaageOptions = Object.freeze({
	error: 'error',
	success: 'success',
	info: 'info',
	warning: 'warning'
});

function fireMessage(title, message, typeMesaageOptions) {
	Swal.fire(title, message, typeMesaageOptions);
}