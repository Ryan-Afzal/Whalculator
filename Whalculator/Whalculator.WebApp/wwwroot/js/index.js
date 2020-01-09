"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/calcHub").build();

connection.start().then(function () {

}).catch(function (err) {
    return console.error(err.toString());
});

connection.on("ReceiveResult", function (result) {
    var node = document.createElement("span");
    node.setAttribute("class", "output-message");
    node.textContent = "=> " + result;

    document.getElementById("output").append(node);
});

document.getElementById("input").addEventListener("keydown", function (event) {
    if (event.key == "Enter") {
        var input = document.getElementById("input").value;

        if (input == null || input == "") {

        } else {
            var node = document.createElement("span");
            node.setAttribute("class", "output-message");
            node.textContent = input;

            document.getElementById("output").append(node);

            connection.invoke("ProcessInput", input).catch(function (err) {
                return console.error(err.toString());
            });

            document.getElementById("input") = "";

            event.preventDefault();
        }
    }
});