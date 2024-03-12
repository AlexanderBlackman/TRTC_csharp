using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TRTC_csharp.Xunit
{
   internal static class Initializer
   {
      [ModuleInitializer]
      public static void SetDefaults()
      {
         AssertionOptions.AssertEquivalencyUsing(
             options =>
             {
                //options.Using<Double>(ctx => ctx.Subject.Should().BeApproximately(ctx.Expectation, 0.0001));
                //options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, TimeSpan.FromSeconds(1))).WhenTypeIs<DateTime>();
                //options.Using<DateTimeOffset>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation, TimeSpan.FromSeconds(1))).WhenTypeIs<DateTimeOffset>();
                return options;
             });
      }
   }
}
//Some common adjustments I use.