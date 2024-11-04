document.addEventListener('DOMContentLoaded', (event) => {
    try {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": true,
            "progressBar": true,
            "positionClass": "toast-top-center",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }

        var message = document.getElementById('toast-message')
        var messageType = document.getElementById('toast-message-type')

        if (message && messageType) {

            switch (messageType.value) {
                case "success":
                    toastr.success(message.value)
                    break;
                case "error":
                    toastr.error(message.value)
                    break;
                case "warning":
                    toastr.warning(message.value)
                    break;
                case "info":
                    toastr.info(message.value)
                    break;
            }
        }

    } catch (error) {
        console.error('Error occurred while showing toastr:', error)
    }
});