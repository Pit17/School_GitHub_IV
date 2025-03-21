//Pietro Malzone 4H 21/03/2025
let passReg = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#\$%\^&\*])(?=.{8,})/; // password format
let mailReg = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/; // email format
let textReg = /^[A-Za-z]{3,25}$/; // 3-25

function validateDateOfBirth(inputDate) {
    const today = new Date();
    const minDate = new Date(today.getFullYear() - 100, today.getMonth(), today.getDate()); // 100 anni fa
    const maxDate = new Date(today.getFullYear() - 10, today.getMonth(), today.getDate());  // 10 anni fa

    const dateToValidate = new Date(inputDate);

    return dateToValidate >= minDate && dateToValidate <= maxDate;
}

function validateForm() {
    // Validazione Nome
    const name = $("#name").val();
    if (!textReg.test(name)) {
        alert("Il nome deve contenere solo lettere e avere una lunghezza tra 3 e 25 caratteri.");
        return false;
    }

    // Validazione Cognome
    const surname = $("#surname").val();
    if (!textReg.test(surname)) {
        alert("Il cognome deve contenere solo lettere e avere una lunghezza tra 3 e 25 caratteri.");
        return false;
    }

    // Validazione Email
    const email = $("#mail").val();
    if (!mailReg.test(email)) {
        alert("Inserisci un'email valida.");
        return false;
    }

    // Validazione Data di Nascita
    const date = $("#date").val();
    if (!validateDateOfBirth(date)) {
        alert("La data di nascita deve essere compresa tra 10 e 100 anni fa.");
        return false;
    }

    // Validazione Username
    const username = $("#username").val();
    if (!textReg.test(username)) {
        alert("Lo username deve contenere solo lettere e avere una lunghezza tra 3 e 25 caratteri.");
        return false;
    }

    // Validazione Password
    const password = $("#password").val();
    if (!passReg.test(password)) {
        alert("La password deve essere lunga tra 8 e 32 caratteri e contenere almeno una lettera e un numero.");
        return false;
    }

    return true;
}

function login() {

    if (validateForm()) {
        alert("Login effettuato con successo!");
    }

}


