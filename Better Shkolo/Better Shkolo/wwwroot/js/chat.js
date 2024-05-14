
"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/Team/chatHub").build();
// Disable the send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("RecieveMessage", function (user, message, teamId) {
    var tId = document.getElementById("teamId").value;
    if (teamId == tId) {
        if (user == document.getElementById("userName").value) {
            var li = document.createElement("li")
            li.classList.add("clearfix");

            var div = document.createElement("div")
            div.classList.add("message-data");
            div.classList.add("text-right");
            div.style.textAlign = "right";

            var span = document.createElement("span")
            span.classList.add("message-data-time");
            span.textContent = `${user}`;
            div.appendChild(span);

            var img = document.createElement("img")
            img.src = "https://bootdey.com/img/Content/avatar/avatar7.png";
            div.appendChild(img);
            li.appendChild(div);

            var secondDiv = document.createElement("div");
            secondDiv.classList.add("message");
            secondDiv.classList.add("other-message");
            secondDiv.classList.add("float-right");
            secondDiv.textContent = message;

            li.appendChild(secondDiv);

            document.getElementById("messageList").appendChild(li);
        }
        else {
            var li = document.createElement("li")

            var div = document.createElement("div")
            div.classList.add("message-data");

            var span = document.createElement("span")
            span.classList.add("message-data-time");
            span.textContent = `10:12 AM, Today`;
            div.appendChild(span);
            
            var secondDiv = document.createElement("div")
            secondDiv.classList.add("message");
            secondDiv.classList.add("my-message");
            secondDiv.textContent = message;

            li.appendChild(div);
            li.appendChild(secondDiv);

            document.getElementById("messageList").appendChild(li);
        }
    }
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
})

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userName").value;
    var message = document.getElementById("messageInput").value;
    var teamId = document.getElementById("teamId").value;

    connection.invoke("SendMessage", user, message, teamId).catch(function (error) {
        return console.error(error.toString());
    })

    event.preventDefault();
})