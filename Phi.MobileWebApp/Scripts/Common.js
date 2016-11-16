// Guid part calculator.
function s4() {
    return Math.floor((1 + Math.random()) * 0x10000)
           .toString(16)
           .substring(1);
}

// Guid calculator.
function guid() {
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
            s4() + '-' + s4() + s4() + s4();
}

// Hides specified html element.
function CloseElement(element) {

    element.style.display = 'none';
}

function ShowError(text) {

    console.error(text);
}

function LogError(e) {

    console.error(e);
}