﻿"use strict";

/**********************
 * © Whalculator 2020 *
 **********************/

const apiURI = 'api/Calculator';

var variables = {};
var functions = {};

$(document).ready(function () {
    getConsoleInput(true).keydown(function (event) {
        if (event.key == "Enter" && !event.shiftKey) {
            var node = getConsoleInput(false);
            var input = node.textContent;

            if (input.trim() == "") {
                // Do Nothing
            } else {
                node.textContent = "";

                printInput(input);
                getResult(input, processResponse);

                event.preventDefault();
            }
        }
    });
});

function getResult(input, callback) {
    var strings = [];
    var i = 0;

    for (var v in variables) {
        strings[i] = `${v}=${variables[v]}`;
        i++;
    }

    for (var f in functions) {
        strings[i] = `${f}=${functions[f]}`;
        i++;
    }

    strings[i] = input;

    const body = {
        input: strings
    };

    fetch(`${apiURI}`, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(body)
    })
        .then(res => res.json())
        .then(data => {;
            callback(data.response);
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

    $("#console-output-container").scrollTop(output[0].scrollHeight);
}

function printInput(input) {
    printMessage("", input);
}

function printOutput(output) {
    printMessage("🠖", output);
}

function processResponse(response) {
    var equals = response.indexOf('=');

    if (equals == -1) {
        var index = response.indexOf("\n");
        if (index == -1) {
            printOutput(response);
        } else {
            var string = response;
            do {
                var sub = string.substring(0, index);
                printOutput(sub);
                string = string.substring(index + 1);
                index = string.indexOf("\n");
            } while (index != -1);

            printOutput(string);
        }
    } else {
        var head = response.substring(0, equals);
        var body = response.substring(equals + 1);

        var paren = head.indexOf('(');

        if (paren == -1) {
            variables[head] = body;
        } else {
            functions[head] = body;
        }

        printOutput(response);
    }
}

function getConsoleOutput() {
    return $("#console-output");
}

function getConsoleInput(jQuery) {
    if (jQuery) {
        return $("#console-input");
    } else {
        return document.getElementById("console-input");
    }
}
