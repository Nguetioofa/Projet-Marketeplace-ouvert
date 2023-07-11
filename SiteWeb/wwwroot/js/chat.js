var connection = new signalR.HubConnectionBuilder()
    .withUrl("/chatHub")
    .withAutomaticReconnect([0, 1000, 5000, null])
    .build();

document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (idExpediteur, idDestinataire, contenu, dateM) {
    var messageList = document.getElementById("message-list");
    var li = document.createElement("li");
    var messageClass = idExpediteur === idUser ? 'sent' : 'received';
    li.innerHTML = `
        <div class="message ${messageClass}">
            <small>${dateM}</small><br>
            ${contenu}
        </div>
    `;
    messageList.appendChild(li);
});

connection.on("ReceiveConversationMessages", function (messages) {
    var messageList = document.getElementById("message-list");
    messageList.innerHTML = "";
    messages.forEach(function (message) {
        var li = document.createElement("li");
        var messageClass = message.idExpediteur === idUser ? 'sent' : 'received';
        li.innerHTML = `
            <div class="message ${messageClass}">
                <small>${message.dateM}</small><br>
                ${message.contenu}
            </div>
        `;

        messageList.appendChild(li);
    });
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;

    document.getElementById("sendButton").addEventListener("click", function (event) {
        var messageInput = document.getElementById("messageInput");
        console.log(messageInput.value);
        console.log(contactId);
        console.log(idUser);

        connection.invoke("SendMessage",idUser, contactId, messageInput.value);
        messageInput.value = "";
        event.preventDefault();
    });

    contactList.addEventListener('click', (event) => {
        contactId = parseInt(event.target.getAttribute('data-id'));
        const contactName = event.target.textContent.trim();
        const conversationTitle = document.querySelector('.card-header');
        conversationTitle.textContent = `Conversation avec ${contactName}`;
        connection.invoke("GetConversationMessages", idUser, contactId);
        //        connection.invoke("GetConversationMessages", idUser, contactId);

        for (const contactItem of contactList.children) {
            if (contactItem === event.target) {
                contactItem.classList.add('active');
            } else {
                contactItem.classList.remove('active');
            }
        }
    });
});

//connection.start().then(function () {
//    document.getElementById("sendButton").disabled = false;
//}).catch(function (err) {
//    return console.error(err.toString());
//});

//document.getElementById("sendButton").addEventListener("click", function (event) {
//    const messageInput = document.getElementById("messageInput");
//    const messageText = messageInput.value.trim();


//    connection.invoke("SendMessage", idUser, contactId, messageText).catch(function (err) {
//        return console.error(err.toString());

//    });
//    event.preventDefault();
//});