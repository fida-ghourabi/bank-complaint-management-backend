using BankComplaintManagement.Application.DTOs.Complaints;
using BankComplaintManagement.Application.Exceptions;
using BankComplaintManagement.Application.Interfaces.Services;
using BankComplaintManagement.Application.Services;
using BankComplaintManagement.Domain.Entities;
using BankComplaintManagement.Domain.Enums;
using BankComplaintManagement.Domain.Interfaces;
using BankComplaintManagement.Domain.Interfaces.Repositories;
using Moq;

namespace BankComplaintManagement.Application.Tests;

public class ComplaintServiceTests
{
    [Fact]
    public async Task Should_Create_Complaint_Successfully_When_Data_Is_Valid()
    {
        // Arrange
        var client = new Client(
            "Fida",
            "Ghourabi",
            "fida@example.com",
            "hashed-password",
            "12345678",
            "98765432"
        );

        var account = new BankAccount(
            "ACC001",
            "TN59000000000000000001",
            AccountType.Courant,
            1000m,
            client.Id
        );

        var request = new CreateComplaintRequest
        {
            Category = ComplaintCategory.carte,
            SubCategory = "Carte bloquée",
            RelatedBankAccountId = account.Id,
            Subject = "Ma carte est bloquée",
            Description = "Ma carte bancaire ne fonctionne plus.",
            IncidentDate = DateTime.UtcNow.AddDays(-1),
            IncidentTime = new TimeSpan(10, 30, 0),
            Location = "Tunis",
            Channel = ComplaintChannel.web,
            Priority = ComplaintPriority.normale,
            Attachments = new()
        };

        var complaintRepository = new Mock<IComplaintRepository>();
        var clientRepository = new Mock<IClientRepository>();
        var bankAccountRepository = new Mock<IBankAccountRepository>();
        var bankCardRepository = new Mock<IBankCardRepository>();
        var agentRepository = new Mock<IAgentRepository>();
        var attachmentRepository = new Mock<IAttachmentRepository>();
        var notificationService = new Mock<INotificationService>();
        var storage = new Mock<IFileStorageService>();
        var unitOfWork = new Mock<IUnitOfWork>();

        clientRepository
            .Setup(x => x.GetByIdAsync(client.Id))
            .ReturnsAsync(client);

        bankAccountRepository
            .Setup(x => x.GetByIdAsync(account.Id))
            .ReturnsAsync(account);

        complaintRepository
            .Setup(x => x.AddAsync(It.IsAny<Complaint>()))
            .Returns(Task.CompletedTask);

        notificationService
            .Setup(x => x.NotifyNewComplaintAsync(It.IsAny<Guid>()))
            .Returns(Task.CompletedTask);

        unitOfWork
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        var service = new ComplaintService(
            complaintRepository.Object,
            clientRepository.Object,
            bankAccountRepository.Object,
            bankCardRepository.Object,
            agentRepository.Object,
            attachmentRepository.Object,
            notificationService.Object,
            unitOfWork.Object,
            storage.Object
        );

        // Act
        var result = await service.CreateAsync(client.Id, request);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(request.Subject, result.Subject);
        Assert.Equal(request.Description, result.Description);
        Assert.Equal(client.Id, result.ClientId);

        complaintRepository.Verify(
            x => x.AddAsync(It.IsAny<Complaint>()),
            Times.Once);

        notificationService.Verify(
            x => x.NotifyNewComplaintAsync(It.IsAny<Guid>()),
            Times.Once);

        unitOfWork.Verify(
            x => x.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Should_Throw_NotFoundException_When_Client_Does_Not_Exist()
    {
        // Arrange
        var clientId = Guid.NewGuid();

        var request = new CreateComplaintRequest
        {
            Category = ComplaintCategory.carte,
            SubCategory = "Carte bloquée",
            RelatedBankAccountId = Guid.NewGuid(),
            Subject = "Ma carte est bloquée",
            Description = "Ma carte bancaire ne fonctionne plus.",
            IncidentDate = DateTime.UtcNow.AddDays(-1),
            IncidentTime = new TimeSpan(10, 30, 0),
            Location = "Tunis",
            Channel = ComplaintChannel.web,
            Priority = ComplaintPriority.normale,
            Attachments = new()
        };

        var complaintRepository = new Mock<IComplaintRepository>();
        var clientRepository = new Mock<IClientRepository>();
        var bankAccountRepository = new Mock<IBankAccountRepository>();
        var bankCardRepository = new Mock<IBankCardRepository>();
        var agentRepository = new Mock<IAgentRepository>();
        var attachmentRepository = new Mock<IAttachmentRepository>();
        var notificationService = new Mock<INotificationService>();
        var storage = new Mock<IFileStorageService>();
        var unitOfWork = new Mock<IUnitOfWork>();

        clientRepository
            .Setup(x => x.GetByIdAsync(clientId))
            .ReturnsAsync((Client?)null);

        var service = new ComplaintService(
            complaintRepository.Object,
            clientRepository.Object,
            bankAccountRepository.Object,
            bankCardRepository.Object,
            agentRepository.Object,
            attachmentRepository.Object,
            notificationService.Object,
            unitOfWork.Object,
            storage.Object
        );

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(
            () => service.CreateAsync(clientId, request));
    }
}