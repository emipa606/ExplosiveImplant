using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RimWorld;
using Verse;

namespace Explosive_Implant
{
    class ThoughtWorker_Hediff_ExplosiveImplant : ThoughtWorker_Hediff
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            ThoughtState t = base.CurrentStateInternal(p);
            /*
            if(!t.Equals(ThoughtState.Inactive))
            {
                if(p.IsPrisoner)
                {
                    return t;
                }
                else
                {
                    return ThoughtState.Inactive;
                }
            }
            */
            return t;
        }
    }
}
