using FMOD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Actions
{
    public class AFlipMidrow : CardAction
    {

        public int pivot_world_x;

        public override void Begin(G g, State s, Combat c)
        {
            //play sound bit
            Audio.Play(FSPRO.Event.Story_BooksTeleport);
            //play flashy FX
            for (int i = -20; i < 20; i++)
            {
                var pos = FxPositions.Drone(pivot_world_x + i);
                c.fx.Add(new GlowFX()
                {
                    age = 0.25,
                    color = Colors.energy,
                    pos = pos,
                    size = new Vec(12, 12)
                });
            }
            //move all midrow objects around
            var copy = c.stuff.ToArray();
            c.stuff.Clear();
            foreach (var entry in copy)
            {
                var distance = entry.Value.x - pivot_world_x;
                var new_pos = -distance + pivot_world_x;
                entry.Value.x = new_pos;
                c.stuff.Add(new_pos, entry.Value);
            }

            timer = 0.75;
        }


    }
}
