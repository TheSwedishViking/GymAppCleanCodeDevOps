using GymSwipe.UserTests.FixtureFolder;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymSwipe.UserTests.Collectins
{
    [CollectionDefinition("api")]
    public class APICollectionTests:ICollectionFixture<APIExerciseFixture>
    {
    }
}
