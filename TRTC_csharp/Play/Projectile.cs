using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRTC_csharp.Maths;

namespace TRTC_csharp.Play
{
    public class Projectile
    {
        public Projectile(mVector velocity, mPoint position)
        {
            Velocity = velocity;
            Position = position;
        }

        public mVector Velocity { get; set; }
        public mPoint Position { get; set; }


    }
}
