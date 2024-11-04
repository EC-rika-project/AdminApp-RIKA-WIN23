document.addEventListener('DOMContentLoaded', function () {
    const formErrorHandler = (element, validationResult, errorMessage = '') => {
        let spanElement = document.querySelector(`[data-valmsg-for="${element.name}"]`);

        if (validationResult) {
            element.classList.remove('input-validation-error');
            element.classList.add('input-validation-success');
            spanElement.classList.remove('field-validation-error', 'hidden');
            spanElement.classList.add('field-validation-valid');
            spanElement.innerHTML = '';
        }
        else {
            element.classList.remove('input-validation-success');
            element.classList.add('input-validation-error');
            spanElement.classList.remove('field-validation-valid', 'hidden');
            spanElement.classList.add('field-validation-error');
            spanElement.innerHTML = errorMessage;
        }
    };

    const textValidator = (element, minLength = 2) => {
        const value = element.value.trim();
        let errorMessage = '';
        let valid = true;

        if (value.length === 0) {
            errorMessage = element.dataset.valRequired;
            valid = false;
        } else if (value.length < minLength) {
            errorMessage = `At least ${minLength} characters is required`;
            valid = false;
        }

        formErrorHandler(element, valid, errorMessage);
    };

    const emailValidator = (element) => {
        const regex = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/;
        const value = element.value.trim();
        let errorMessage = '';
        let valid = true;

        if (value.length === 0) {
            errorMessage = element.dataset.valRequired;
            valid = false;
        } else if (!regex.test(value)) {
            errorMessage = 'Your email address is invalid';
            valid = false;
        }

        formErrorHandler(element, valid, errorMessage);
    };

    const passwordValidator = (element) => {
        const regex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,}$/;
        const value = element.value.trim();
        let errorMessage = '';
        let valid = true;

        if (value.length === 0) {
            errorMessage = element.dataset.valRequired;
            valid = false;
        } else if (!regex.test(value)) {
            errorMessage = 'Enter a valid password';
            valid = false;
        }

        formErrorHandler(element, valid, errorMessage);
    };

    const phonenumberValidator = (element) => {
        const regex = /^[0-9]{10}$/;
        const value = element.value.trim();
        let errorMessage = '';
        let valid = true;

        if (value.length === 0) {
            errorMessage = element.dataset.valRequired;
            valid = false;
        } else if (!regex.test(value)) {
            errorMessage = 'Please enter a valid phone number (10 digits)';
            valid = false;
        }

        formErrorHandler(element, valid, errorMessage);
    };

    const postalcodeValidator = (element) => {
        const regex = /^[0-9]{5}$/;
        let value = element.value.replace(/\D/g, '').slice(0, 5);
        element.value = value.replace(/(\d{3})(?=\d)/, '$1 ');
        let errorMessage = '';
        let valid = true;

        if (value.length === 0) {
            errorMessage = element.dataset.valRequired;
            valid = false;
        } else if (value.length < 5) {
            errorMessage = 'Postal code must be exactly 5 digits';
            valid = false;
        } else if (!regex.test(value)) {
            errorMessage = 'Enter a valid 5-digit postal code';
            valid = false;
        }

        formErrorHandler(element, valid, errorMessage);
    };

    const cardNumberValidator = (element) => {
        const regex = /^[0-9]{16}$/;
        let value = element.value.replace(/\D/g, '').slice(0, 16);
        element.value = value.replace(/(\d{4})(?=\d)/g, '$1 ');

        let errorMessage = '';
        let valid = true;

        if (value.length === 0) {
            errorMessage = element.dataset.valRequired || 'Card number is required';
            valid = false;
        } else if (value.length < 16) {
            errorMessage = 'Card number must be exactly 16 digits';
            valid = false;
        } else if (!regex.test(value)) {
            errorMessage = 'Enter a valid 16-digit card number';
            valid = false;
        }

        formErrorHandler(element, valid, errorMessage);
    };

    const expDateValidator = (element) => {
        const regex = /^(0[1-9]|1[0-2])\/([0-9]{2})$/;
        let value = element.value;
        let cursorPosition = element.selectionStart;

        if (cursorPosition === 3 && value.includes('/') && value.length === 3) {
            value = value.substring(0, 2);
            cursorPosition = 2;
        }

        value = value.replace(/\D/g, '').slice(0, 4);

        let errorMessage = '';
        let valid = true;

        if (value.length > 0) {
            if (value.length >= 2) {
                value = value.substring(0, 2) + '/' + value.substring(2);
                if (cursorPosition === 2 && value.length > element.value.length) {
                    cursorPosition = 3;
                }
            }
        }

        element.value = value;
        element.setSelectionRange(cursorPosition, cursorPosition);

        if (value.length === 0) {
            errorMessage = element.dataset.valRequired;
            valid = false;
        } else if (value.length < 5) {
            errorMessage = 'Enter date as MM/YY';
            valid = false;
        } else if (!regex.test(value)) {
            errorMessage = 'Enter a valid date MM/YY';
            valid = false;
        }

        formErrorHandler(element, valid, errorMessage);
    };

    const cvvValidator = (element) => {
        const regex = /^[0-9]{3}$/;
        let value = element.value.replace(/\D/g, '').slice(0, 3);
        element.value = value;

        let errorMessage = '';
        let valid = true;

        if (value.length === 0) {
            errorMessage = element.dataset.valRequired;
            valid = false;
        } else if (!regex.test(value)) {
            errorMessage = 'Enter a valid 3-digit CVV';
            valid = false;
        }

        formErrorHandler(element, valid, errorMessage);
    };

    const checkboxValidator = (element) => {
        let errorMessage = '';

        if (element.checked) {
            formErrorHandler(element, true, '');
        }
        else {
            errorMessage = element.dataset.valRequired;
            formErrorHandler(element, false, errorMessage);
        }
    };

    let forms = document.querySelectorAll('form');

    forms.forEach(form => {

        form.addEventListener('submit', function (event) {
            let inputs = form.querySelectorAll('input, textarea, select');
            let isValid = true;

            inputs.forEach(input => {
                if (input.dataset.val === 'true') {
                    if (input.type === 'checkbox') {
                        checkboxValidator(input);
                        if (!input.checked) {
                            isValid = false;
                        }
                    }
                    else {
                        if (input.name.includes("PostalCode")) {
                            postalcodeValidator(input);
                        } else if (input.name.includes("PhoneNumber")) {
                            phonenumberValidator(input);
                        } else if (input.name.includes("Email") || input.type === 'email') {
                            emailValidator(input);
                        } else if (input.name.includes("Password") || input.type === 'password') {
                            passwordValidator(input);
                        } else if (input.name.includes("CardNumber")) {
                            cardNumberValidator(input);
                        } else if (input.name.includes("ExpiryDate")) {
                            expDateValidator(input);
                        } else if (input.name.includes("CVV")) {
                            cvvValidator(input);
                        } else {
                            textValidator(input);
                        }

                        if (input.classList.contains('input-validation-error')) {
                            isValid = false;
                        }
                    }
                }
            });

            if (!isValid) {
                event.preventDefault();
            }
        });

        let inputs = form.querySelectorAll('input, textarea, select');

        inputs.forEach(input => {
            if (input.dataset.val === 'true') {
                if (input.type === 'checkbox') {
                    input.addEventListener('change', (e) => {
                        checkboxValidator(e.target);
                    });
                }
                else {
                    input.addEventListener('keyup', (e) => {
                        if (input.name.includes("PostalCode")) {
                            postalcodeValidator(e.target);
                        }
                        else if (input.name.includes("PhoneNumber")) {
                            phonenumberValidator(e.target);
                        }
                        else if (input.type === 'email') {
                            emailValidator(e.target);
                        }
                        else if (input.type === 'password') {
                            passwordValidator(e.target);
                        }
                        else if (input.name.includes("CardNumber")) {
                            cardNumberValidator(e.target);
                        }
                        else if (input.name.includes("ExpiryDate")) {
                            expDateValidator(e.target);
                        }
                        else if (input.name.includes("CVV")) {
                            cvvValidator(e.target);
                        }
                        else {
                            textValidator(e.target);
                        }
                    });
                }
            }
        });
    });
});