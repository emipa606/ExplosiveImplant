﻿/*
Copyright © 07/27/2019, ma1ta

Permission is hereby granted, free of charge, to any person obtaining a copy of this software
and associated documentation files (the “Software”),to deal in the Software without restriction,
including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies
or substantial portions of the Software.

The Software is provided “as is”, without warranty of any kind, express or implied, including but not limited to
the warranties of merchantability, fitness for a particular purpose and noninfringement.
In no event shall the authors or copyright holders X be liable for any claim, damages or other liability,
whether in an action of contract, tort or otherwise, arising from, out of or in connection with the software
or the use or other dealings in the Software.
Except as contained in this notice, the name of the <copyright holders> shall not be used in advertising
or otherwise to promote the sale, use or other dealings in this Software without prior written authorization
from the <copyright holders>.
*/

using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Verse;

namespace Explosive_Implant
{
    class HediffWithComps_Explosion : HediffWithComps
    {
        private bool HediffWithComps_toDoAtBeginning = false;

        #region Properties
        public HediffDefs_ExplosiveImplant ExplosiveImplant_Def
        {
            get
            {
                return this.def as HediffDefs_ExplosiveImplant;
            }
        }

        public Pawn host
        {
            get
            {
                return this.pawn;
            }
        }
        #endregion Properties
        public override void Tick()
        {
            base.Tick();

            if (!HediffWithComps_toDoAtBeginning)
            {
                GlobalVariables.list_obj.Add(this);
                HediffWithComps_toDoAtBeginning = true;
            }
        }

        public void Explode()
        {
            GenExplosion.DoExplosion(host.Position, host.Map, ExplosiveImplant_Def.explosionRadius, ExplosiveImplant_Def.damageDef, host);

            DamageInfo dInfo = new DamageInfo(ExplosiveImplant_Def.damageDef, 100.0f);
            host.Kill(dInfo);
            if (host.def.race.body.GetPartsWithDef(BodyPartDefOf.Head).Count() > 0)
                host.health.AddHediff(HediffDefOf.MissingBodyPart, host.def.race.body.GetPartsWithDef(BodyPartDefOf.Head).First());
        }
    }
}