using System;
using ERP.Domain.Entities;
using Xunit;
using Xunit.Categories;

[UnitTest]
public class UserTests
{
    [Fact]
    public void AddToken_SetsTokenProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var email = "test@example.com";
        var username = "testuser";
        var password = "password";
        var profileId = new Guid();

        var user = new User(email, username, password, profileId);
        var token = "sampleToken";
        var refreshToken = "sampleRefreshToken";
        var expiration = DateTime.UtcNow.AddHours(1);

        // Act
        user.AddToken(token, refreshToken, expiration);

        // Assert
        Assert.Equal(token, user.Token);
        Assert.Equal(refreshToken, user.TokenRefresh);
        Assert.Equal(expiration.ToUniversalTime(), user.RevokeIn);
    }

    [Fact]
    public void RevokeToken_RevokesToken()
    {
        // Arrange
        var id = Guid.NewGuid();
        var email = "test@example.com";
        var username = "testuser";
        var password = "password";
        var profileId = new Guid();
        var user = new User(email, username, password, profileId);
        user.Token = "sampleToken";
        user.TokenRefresh = "sampleRefreshToken";
        user.RevokeIn = DateTime.UtcNow.AddHours(1);

        // Act
        user.RevokeToken();

        // Assert
        Assert.Null(user.Token);
        Assert.Equal(string.Empty, user.TokenRefresh);
        Assert.Equal(DateTime.MinValue, user.RevokeIn);
    }

    [Fact]
    public void IsTokenRevoked_ReturnsTrueIfTokenRevoked()
    {
        // Arrange
        var email = "test@example.com";
        var username = "testuser";
        var password = "password";
        var profileId = new Guid();
        var user = new User(email, username, password, profileId);
        user.Token = null;
        user.RevokeIn = DateTime.UtcNow.AddHours(-1);

        // Act
        bool isRevoked = user.IsTokenRevoked();

        // Assert
        Assert.True(isRevoked);
    }

    [Fact]
    public void IsTokenRevoked_ReturnsFalseIfTokenNotRevoked()
    {
        // Arrange
        var email = "test@example.com";
        var username = "testuser";
        var password = "password";
        var profileId = new Guid();
        var user = new User(email, username, password, profileId);
        user.Token = "sampleToken";
        user.RevokeIn = DateTime.UtcNow.AddHours(1);

        // Act
        bool isRevoked = user.IsTokenRevoked();

        // Assert
        Assert.False(isRevoked);
    }

    [Fact]
    public void UpdateToken_UpdatesTokenProperties()
    {
        // Arrange
        var email = "test@example.com";
        var username = "testuser";
        var password = "password";
        var profileId = new Guid();

        var user = new User(email, username, password, profileId);
        var newToken = "newToken";
        var newRefreshToken = "newRefreshToken";
        var newExpiration = DateTime.UtcNow.AddHours(2);

        // Act
        user.UpdateToken(newToken, newRefreshToken, newExpiration);

        // Assert
        Assert.Equal(newToken, user.Token);
        Assert.Equal(newRefreshToken, user.TokenRefresh);
        Assert.Equal(newExpiration, user.RevokeIn);
    }
}