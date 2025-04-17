
using Bunit;
using Xunit;
using Moq; // Import Moq for mocking
using Microsoft.Extensions.DependencyInjection;
using travlr.Admin.Pages; // Make sure this matches your Blazor project
using travlr.Backend.Service; // Correct namespace for AuthService


namespace travlr
{
    public class NavbarComponentTests : TestContext
    {
        private readonly string fakeToken = "test-token";

        [Fact]
        public void Navbar_ShouldRender_Correctly_When_User_Is_LoggedIn()
        {
            // Arrange: Mock AuthenticationService
            var authService = new Mock<AuthService>();
            authService.Setup(a => a.IsLoggedIn(fakeToken)).Returns(true);
            Services.AddSingleton(authService.Object);

            // Act: Render component
            var component = RenderComponent<Navbar>();

            // Assert: Check if Logout link is visible
            Assert.Contains("Logout", component.Markup);
        }

        [Fact]
        public void Navbar_ShouldRender_Correctly_When_User_Is_LoggedOut()
        {
            // Arrange: Mock AuthenticationService
            var authService = new Mock<AuthService>();
            authService.Setup(a => a.IsLoggedIn(fakeToken)).Returns(false);
            Services.AddSingleton(authService.Object);

            // Act: Render component
            var component = RenderComponent<Navbar>();

            // Assert: Check if Login link is visible
            Assert.Contains("Login", component.Markup);
        }
    }
}
