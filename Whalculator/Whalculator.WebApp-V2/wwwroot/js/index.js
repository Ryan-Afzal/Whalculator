"use strict";

/**********************
 * © Whalculator 2020 *
 **********************/

const apiURI = 'api/Calculator';// Calculator API URI

var variables = {};// Variables
var functions = {};// Functions

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

/*
 * Hash function for strings, used to make an ID key for variables and functions.
 */
String.prototype.hashCode = function () {
    var hash = 0;

    if (this.length == 0) {
        return hash;
    } else {
        var char;

        for (var i = 0; i < this.length; i++) {
            char = this.charCodeAt(i);
            hash = ((hash << 5) - hash) + char;
            hash |= 0;
        }

        return hash;
    }
};

/*
 * Sends a POST request to the API and calls the callback function with the returned JSON result.
 */
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

/*
 * Prints a message to the console.
 */
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

/*
 * Prints a message formatted as a user input.
 */
function printInput(input) {
    printMessage("", input);
}

/*
 * Prints a message formatted as a returned API response.
 */
function printOutput(output) {
    printMessage("🠖", output);
}

/*
 * Processes a returned API response.
 */
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
            putVariable(head, body);
        } else {
            functions[head] = body;
            putFunction(head, body);
        }

        printOutput(response);
    }
}

/*
 * Returns the console output.
 */
function getConsoleOutput() {
    return $("#console-output");
}

/*
 * Returns the console input. Contains a boolean parameter to determine whether to return a JQuery object or a regular DOM object.
 * This distinction is useful in certain scenarios.
 */
function getConsoleInput(jQuery) {
    if (jQuery) {
        return $("#console-input");
    } else {
        return document.getElementById("console-input");
    }
}

/*
 * Puts data to the a list group.
 */
function putData(node, key, value) {
    key = key.trim();
    value = value.trim();

    var idKey = `key-${key.hashCode()}`;

    var test = $(`#${idKey}`);

    var containerNode;
    var dataNode;

    if (test.length == 0) {
        dataNode = $('<div />').addClass("data-display-container");
        containerNode = $(`<li id="${idKey}"/>`)
            .addClass("data-display-container list-group-item")
            .append(dataNode);
    } else {
        containerNode = test;
        dataNode = containerNode
            .children()
            .first();
    }

    dataNode.text(`${key} = ${value}`);
    node.append(containerNode);
}

/*
 * Puts data to the 'variables-list' group.
 */
function putVariable(head, body) {
    putData($("#variables-list"), head, body);
}

/*
 * Puts data to the 'functions-list' group.
 */
function putFunction(head, body) {
    putData($("#functions-list"), head, body);
}
