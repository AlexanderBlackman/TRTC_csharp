using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TRTC_csharp.Maths;

//THIS IS UNFINISHED
namespace TRTC_csharp.Play
{
    internal class RocketGame
    {
        Projectile cannonBall;
        Environment atmosphere;


        public RocketGame()
        {

        }


        void Tick(Projectile ball, Environment space)
        {
            ball.Velocity = (ball.Velocity + space.Gravity + space.Wind);
            ball.Position = ball.Position + ball.Velocity;
            Console.WriteLine($"Tick: Ball is at {ball.Position.x},{ball.Position.y}");
            Console.WriteLine($"Velocity magnitude is now {ball.Velocity.Magnitude().ToString("0.00")}");
            Console.WriteLine($"Velocity is {ball.Velocity.ToString()}");


            void Play()
            {
                Environment sky = new Environment()
                {
                    Gravity = new mVector(0, -0.1, 0),
                    Wind = new mVector(-0.01, 0, 0)
                };

                Projectile ball = new Projectile()
                {
                    Velocity = (new mVector(1, 1, 0).Normalised()),
                    Position = new mPoint(0, 1, 0),
                };

                while (ball.Position.y > 0)
                {

                }
            }

        }
    }
}
