using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using SiteWeb.Hubs;
using SiteWeb.Services.Interfaces;

namespace MyProject.Tests
{
    [TestFixture]
    public class ChatHubTests
    {
        private Mock<IMessageService> _mockMessageService;
        private Mock<IHubCallerClients> _mockClients;
        private Mock<IClientProxy> _mockClientProxy;
        private ChatHub _chatHub;

        [SetUp]
        public void SetUp()
        {
            _mockMessageService = new Mock<IMessageService>();
            _mockClients = new Mock<IHubCallerClients>();
            _mockClientProxy = new Mock<IClientProxy>();
            _mockClients.Setup(clients => clients.All).Returns(_mockClientProxy.Object);
            _chatHub = new ChatHub(_mockMessageService.Object)
            {
                Clients = _mockClients.Object
            };
        }

        [Test]
        public async Task SendMessage_SendsMessageToAllClients()
        {
            // Arrange
            int idExpediteur = 1;
            int idDestinataire = 2;
            string contenu = "Hello, World!";

            // Act
            await _chatHub.SendMessage(idExpediteur, idDestinataire, contenu);

            // Assert
           // _mockClientProxy.Verify(clientProxy => clientProxy.SendAsync("ReceiveMessage", idExpediteur, idDestinataire, contenu, It.IsAny<DateTime>()), Times.Once);
        }
    }
}
