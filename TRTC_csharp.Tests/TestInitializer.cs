using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRTC_csharp.Tests
{
    [TestClass]
    public static class TestInitializer
    {
        [AssemblyInitialize]
        public static void SetDefaults(TestContext context)
        {
            AssertionOptions.AssertEquivalencyUsing(
                options => { options.Using<Double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, 0.0001));

                    return options;
                });
        }
    }
}

/* Examples I've found
 *               options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, TimeSpan.FromSeconds(1))).WhenTypeIs<DateTime>();
                options.Using<DateTimeOffset>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, TimeSpan.FromSeconds(1))).WhenTypeIs<DateTimeOffset>(); */