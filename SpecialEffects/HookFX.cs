using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.SpecialEffects
{
    public class HookFX : FX
    {

        public double target_duration = 0.3;
        public int world_x_pos;

        public override void Update(G g)
        {
            if (age == 0)
                Start(g);
            age += g.dt / (target_duration);// + (target_duration / 5));
        }

        public override void Render(G g, Vec v)
        {
            var pos = v + FxPositions.Drone(world_x_pos);

            var sel = (int)(age * 5);

            var spr = (Spr)(Manifest.HookFxSprites?.ElementAt(sel).Id ?? throw new Exception());

            Draw.Sprite(spr, pos.x - 10, pos.y - 20, false, false, 0, color: Colors.textMain);
        }

    }
}
