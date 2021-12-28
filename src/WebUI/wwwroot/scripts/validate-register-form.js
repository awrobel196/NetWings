function validate(email, name, phone, password) {

    //Jeśli którykolwiek z warunków nie został spełniony, zwróci false
    var validationStatus = true;

    //Else używane aby w przypadku gdy w pierwszym podejściu alert został wyświetlony a w drugim podejściu warunki są spełnione, to alert zostanie ukryty
    if (email == "" || !(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/.test(email))) {
        validationStatus = false;
        $("#email-alert").removeClass("display-none");
        $("#email").addClass("incorrect-input");
    } else {
        $("#email-alert").addClass("display-none");
        $("#email").removeClass("incorrect-input");
    }

    if (name == "") {
        validationStatus = false;
        $("#name-alert").removeClass("display-none");
        $("#name").addClass("incorrect-input");
    } else {
        $("#name-alert").addClass("display-none");
        $("#name").removeClass("incorrect-input");
    }

    if (phone == "" || !(/^(()?\d{3}())?(-|\s)?\d{3}(-|\s)?\d{3}$/.test(phone))) {
        validationStatus = false;
        $("#phone-alert").removeClass("display-none");
        $("#phone").addClass("incorrect-input");
    } else {
        $("#phone-alert").addClass("display-none");
        $("#phone").removeClass("incorrect-input");
    }

    if (password == "" || password.length <= 8) {
        validationStatus = false;
        $("#password-alert").removeClass("display-none");
        $("#password").addClass("incorrect-input");
    } else {
        $("#password-alert").addClass("display-none");
        $("#password").removeClass("incorrect-input");
    }

    return validationStatus;
}