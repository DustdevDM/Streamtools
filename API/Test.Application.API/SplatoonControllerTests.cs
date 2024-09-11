using System.Globalization;
using Application.API.Controller;
using Core.BusinessLogic.Builder;
using Core.BusinessLogic.DTOs.Settings;
using Core.BusinessLogic.Services;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;

namespace Test.Application.API;

public class SplatoonControllerTests
{

  [Fact]
  public async void WlStats_Returns_SplatoonStatsResponse_With_Correct_Calculated_Win_Percentage()
  {
    // Arrange
    IStatInkService fakeStatInkService = A.Fake<IStatInkService>();
    StatInkSettingsDto statInkSettings = new (){ StatisticsUsername = "fakeUsername" };

    Random random = new ();
    int randomWonMatches =  random.Next(0, 100);
    int randomTotalMatches =  random.Next(randomWonMatches, 101);
    string calculatedRandomResult = Math.Round((float)randomWonMatches / randomTotalMatches * 100, 2).ToString(CultureInfo.InvariantCulture);
    (int, int) fakeStatInkServiceResult = (randomTotalMatches, randomWonMatches);

    A.CallTo(fakeStatInkService).WithReturnType<Task<(int, int)>>().Returns(Task.FromResult(fakeStatInkServiceResult));

    SplatoonController controller = new (fakeStatInkService, statInkSettings);
    StatInkQueryBuilder cleanStatInkQueryBuilder = new ();

    // Act
    var actionResult = await controller.WlStats(cleanStatInkQueryBuilder);

    // Assert
    Assert.NotNull(actionResult.Value);
    Assert.Equal(actionResult.Value?.WinPercentage, calculatedRandomResult);
  }

  [Fact]
  public async void WlStats_Returns_204_If_There_Are_No_Recorded_Matches()
  {
    // Arrange
    IStatInkService fakeStatInkService = A.Fake<IStatInkService>();
    StatInkSettingsDto statInkSettings = new (){ StatisticsUsername = "fakeUsername" };
    (int, int) fakeStatInkServiceResult = (0, 0);

    A.CallTo(fakeStatInkService).WithReturnType<Task<(int, int)>>().Returns(Task.FromResult(fakeStatInkServiceResult));

    SplatoonController controller = new (fakeStatInkService, statInkSettings);
    StatInkQueryBuilder cleanStatInkQueryBuilder = new ();

    // Act
    var actionResult = await controller.WlStats(cleanStatInkQueryBuilder);

    // Assert
    Assert.IsType<NoContentResult>(actionResult.Result);
  }
}
