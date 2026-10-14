using GymSwipe.UserTests.FixtureFolder;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace GymSwipe.UserTests.Collections
{
    [CollectionDefinition("api")]
    public class APICollectionTests:ICollectionFixture<APIExerciseFixture>
    {
    }
}
