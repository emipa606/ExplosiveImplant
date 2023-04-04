/*
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

using System.Linq;
using RimWorld;
using Verse;
using Verse.Sound;

namespace Explosive_Implant;

internal class HediffWithComps_Explosion : HediffWithComps
{
    private bool HediffWithComps_toDoAtBeginning;

    public override void Tick()
    {
        base.Tick();

        if (HediffWithComps_toDoAtBeginning)
        {
            return;
        }

        GlobalVariables.list_obj.Add(this);
        HediffWithComps_toDoAtBeginning = true;
    }

    public void Explode()
    {
        if (ExplosiveImplant_Def.damageDef == DamageDefOf.Stun)
        {
            FleckMaker.ThrowMicroSparks(host.Position.ToVector3(), host.Map);
            FleckMaker.ThrowLightningGlow(host.Position.ToVector3(), host.Map, host.BodySize);
            SoundDefOf.EnergyShield_AbsorbDamage.PlayOneShot(SoundInfo.InMap(host));
            HealthUtility.TryAnesthetize(host);
            return;
        }

        GenExplosion.DoExplosion(host.Position, host.Map, ExplosiveImplant_Def.explosionRadius,
            ExplosiveImplant_Def.damageDef, host);

        var dInfo = new DamageInfo(ExplosiveImplant_Def.damageDef, 100.0f);
        host.Kill(dInfo);
        if (host.def.race.body.GetPartsWithDef(BodyPartDefOf.Head).Any())
        {
            host.health.AddHediff(HediffDefOf.MissingBodyPart,
                host.def.race.body.GetPartsWithDef(BodyPartDefOf.Head).First());
        }
    }

    #region Properties

    public HediffDefs_ExplosiveImplant ExplosiveImplant_Def => def as HediffDefs_ExplosiveImplant;

    public Pawn host => pawn;

    #endregion Properties
}