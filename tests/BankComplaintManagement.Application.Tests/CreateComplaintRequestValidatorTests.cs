using BankComplaintManagement.Application.DTOs.Complaints;
using BankComplaintManagement.Application.Validators.Complaints;
using BankComplaintManagement.Domain.Enums;

namespace BankComplaintManagement.Application.Tests;

public class CreateComplaintRequestValidatorTests
{
    [Fact]
    public void Should_Pass_When_Complaint_Data_Is_Valid()
    {
        // Arrange
        var request = new CreateComplaintRequest
        {
            Category = ComplaintCategory.carte,
            SubCategory = "Carte bloquée",
            RelatedBankAccountId = Guid.NewGuid(),
            Subject = "Ma carte est bloquée",
            Description = "Ma carte bancaire ne fonctionne plus.",
            IncidentDate = DateTime.UtcNow,
            IncidentTime = new TimeSpan(10, 30, 0),
            Location = "Tunis",
            Channel = ComplaintChannel.web,
            Priority = ComplaintPriority.normale,
            Attachments = new()
        };

        var validator = new CreateComplaintRequestValidator();

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.True(result.IsValid);
    }

    [Fact]
    public void Should_Fail_When_IncidentDate_Is_In_The_Future()
    {
        // Arrange
        var request = new CreateComplaintRequest
        {
            Category = ComplaintCategory.carte,
            SubCategory = "Carte bloquée",
            RelatedBankAccountId = Guid.NewGuid(),
            Subject = "Ma carte est bloquée",
            Description = "Ma carte bancaire ne fonctionne plus.",
            IncidentDate = DateTime.UtcNow.AddDays(1),
            IncidentTime = new TimeSpan(10, 30, 0),
            Location = "Tunis",
            Channel = ComplaintChannel.web,
            Priority = ComplaintPriority.normale,
            Attachments = new()
        };

        var validator = new CreateComplaintRequestValidator();

        // Act
        var result = validator.Validate(request);

        // Assert
        Assert.False(result.IsValid);
    }
}