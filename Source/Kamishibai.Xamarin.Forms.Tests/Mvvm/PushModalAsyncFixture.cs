﻿using Kamishibai.Xamarin.Forms.Mvvm;
using Moq;
using Xamarin.Forms;
using Xunit;

namespace Kamishibai.Xamarin.Forms.Tests.Mvvm
{
    public class PushModalAsyncFixture
    {
        [Fact]
        public void Navigate()
        {
            var pushModalAsync = new PushModalAsync<ContentPage>();
            var navigatorMock = new Mock<INavigator>();
            pushModalAsync.Navigator = navigatorMock.Object;

            var parameter = "Hello, Parameter!";
            pushModalAsync.Navigate(parameter);
			
            navigatorMock.Verify(m => m.PushModalAsync(It.IsAny<ContentPage>(), parameter, true), Times.Once);
        }

        [Fact]
        public void NavigateWithNavigationPage()
        {
            var pushModalAsync = new PushModalAsync<ContentPage>{ WithNavigationPage = true };
            var navigatorMock = new Mock<INavigator>();
            pushModalAsync.Navigator = navigatorMock.Object;

            var parameter = "Hello, Parameter!";

            NavigationPage navigationPage = null;
            navigatorMock
                .Setup(m => m.PushModalAsync(It.IsAny<NavigationPage>(), parameter, true))
                .Callback<Page, string, bool>((page, param, animated) =>
                {
                    navigationPage = page as NavigationPage;
                });
            
            pushModalAsync.Navigate(parameter);
			
            navigatorMock.Verify(m => m.PushModalAsync(It.IsAny<NavigationPage>(), parameter, true), Times.Once);
            Assert.NotNull(navigationPage);
            Assert.IsType<ContentPage>(navigationPage.CurrentPage);
        }
    }
}