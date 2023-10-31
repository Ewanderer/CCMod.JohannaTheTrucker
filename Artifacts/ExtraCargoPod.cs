﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JohannaTheTrucker.Artifacts
{
    [ArtifactMeta(unremovable = true)]
    public class ExtraCargoPod : Artifact
    {
        public override int ModifyCardRewardCount(State state, bool inCombat)
        {
            if (state.route is not Combat c)
                return 0;
            if (!c.EitherShipIsDead(state))
                return 0;
            if (c.turn <= 0)
                return 0;
            if (state.map.markers[state.map.currentLocation].contents is not MapBattle contents)
                return 0;
            if (contents.battleType != BattleType.Boss)
                return 1;
            return 0;
        }
    }
}
