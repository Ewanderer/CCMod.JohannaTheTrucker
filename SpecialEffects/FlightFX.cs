using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.SpecialEffects
{
    public class FlightFX : FX
    {

        public Texture2D? texture;

        public int start_x;

        public enum YRow
        {
            player,
            midrow,
            enemy
        }


        public YRow start_y;

        public int target_x;

        public YRow target_y;

        /// <summary>
        /// If the target pos would result in a miss.
        /// </summary>
        public bool miss;

        /// <summary>
        /// Duration for this FX to complete
        /// </summary>
        public double target_flight_time = 0.25;

        public override void Update(G g)
        {
            age += g.dt / target_flight_time;
        }

        public override void Render(G g, Vec v)
        {
            if (age > 1 || age < 0)
                return;
            //get start pos in pixel

            Vec start_pos;
            
            switch (start_y)
            {
                case YRow.player:
                    start_pos = FxPositions.Hull(start_x, true);
                    break;
                case YRow.midrow:
                    start_pos = FxPositions.Drone(start_x);
                    break;
                case YRow.enemy:
                    start_pos = FxPositions.Hull(start_x, false);
                    break;
                default:
                    //don't render if misconfigured.
                    return;
            }

            //get end pos in pixel
            Vec end_pos;

            switch (target_y)
            {
                case YRow.player:
                    end_pos = FxPositions.Hull(target_x, true);
                    break;
                case YRow.midrow:
                    end_pos = FxPositions.Drone(target_x);
                    break;
                case YRow.enemy:
                    end_pos = FxPositions.Hull(target_x, false);
                    break;
                default:
                    //don't render if misconfigured.
                    return;
            }

            //create vector
            Vec path = end_pos - start_pos;
            //calc render pos
            Vec render_pos;
            if (miss)
            {
                render_pos = start_pos + path * 2 * age;
            }
            else
            {
                render_pos = start_pos + path * age;
            }

            //get rotation
            var angle = 0;// Math.Atan2(1 - path.y, -path.x);

            var color = new Color(1, 1, 1, 1);

            if (miss)
            {
                var f = Math.Clamp(1.25 - age, 0, 1);
                //fadeout
                color = new Color(f, f, f, f);
            }
            //render
            if (texture != null)
                Draw.Sprite(texture, v.x + render_pos.x, v.y + render_pos.y, false, false, angle, color: color);
        }

    }
}
