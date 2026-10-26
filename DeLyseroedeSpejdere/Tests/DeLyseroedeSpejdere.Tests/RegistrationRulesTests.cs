using DeLyseroedeSpejdere.Models;
using DeLyseroedeSpejdere.Services;
using Xunit;

namespace DeLyseroedeSpejdere.Tests;

public class RegistrationRulesTests
{
    [Fact]
    public void Scout_WithCorrectAgeAndBadge_IsEligible()
    {
        var scout = new User { Role = "Scout", Age = 13, Badges = "Bål, Førstehjælp" };
        var activity = new Activity { MinAge = 12, RequiredBadge = "førstehjælp" };
        Assert.True(RegistrationRules.CheckEligibility(scout, activity).Success);
    }

    [Fact]
    public void UnderageScout_IsRejected()
    {
        var scout = new User { Role = "Scout", Age = 10 };
        var activity = new Activity { MinAge = 12 };
        Assert.False(RegistrationRules.CheckEligibility(scout, activity).Success);
    }

    [Fact]
    public void ScoutWithoutRequiredBadge_IsRejected()
    {
        var scout = new User { Role = "Scout", Age = 15, Badges = "Bål" };
        var activity = new Activity { MinAge = 12, RequiredBadge = "Førstehjælp" };
        Assert.False(RegistrationRules.CheckEligibility(scout, activity).Success);
    }

    [Fact]
    public void LeaderCannotRegisterAsScout()
    {
        var leader = new User { Role = "Leader", Age = 35 };
        var activity = new Activity { MinAge = 0 };
        Assert.False(RegistrationRules.CheckEligibility(leader, activity).Success);
    }
}
