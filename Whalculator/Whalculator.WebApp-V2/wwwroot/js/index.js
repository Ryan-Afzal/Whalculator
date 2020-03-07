"use strict";

/**********************
 * © Whalculator 2020 *
 **********************/

const apiURI = 'api/Calculator';

$(document).ready(function () {
    printMessage("", "2 + 2");
    printMessage("🠖", " 4");
    printMessage("", "1 + 5 + 3 + ln(10)");
    printMessage("🠖", " 9 + ln(10)");
    printMessage("🠖", "11.3025851");
    getResult(1);
});

function getResult(input) {
    fetch(`${apiURI}/${input}`, {
        method: 'GET'
    })
        .then(res => res.text())
        .then(data => {;
            printMessage("API RETURNED: ", data);
        })
        .catch(error => {
            console.error('ERROR', error);
        });
}

function printMessage(head, message) {
    var output = getConsoleOutput();

    output.append(
        $('<div />').addClass("output-text-container")
            .append(
                $('<div />').addClass("output-text-head").text(head)
            )
            .append(
                $('<div />').addClass("output-text").text(message)
            )
    );
}

function getConsoleOutput() {
    return $("#console-output");
}

function getConsoleInput() {
    return $("#console-input");
}
