using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRTC_csharp.Maths;

namespace TRTC_csharp.Play
{
    public class Environment
    {
        public Environment(mVector gravity, mVector wind)
        {
            Gravity = gravity;
            Wind = wind;
        }
        public mVector Gravity;
        public mVector Wind;

    }
}
