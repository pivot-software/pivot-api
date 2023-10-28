using System;
using ERP.Domain.Entities;
using Xunit;
using Xunit.Categories;

[UnitTest]
public class WorkspaceTests
{
    [Fact]
    public void Workspace_CanBeCreated()
    {
        // Arrange
        var businessName = "My Business";
        var businessLogo = "logo.png";
        var businessColor = "blue";
        var templateMode = "T";
        var adminId = new Guid("a83c6ecb-9ef7-4d61-869a-62dd1ea82d7d");

        // Act
        var workspace = new Workspace(businessName, businessLogo, businessColor, templateMode, adminId);

        // Assert
        Assert.Equal(businessName, workspace.BusinessName);
        Assert.Equal(businessLogo, workspace.BusinessLogo);
        Assert.Equal(businessColor, workspace.BusinessColor);
        Assert.Equal('T', workspace.TemplateMode);
        Assert.Equal(adminId, workspace.AdminId);
    }
}
