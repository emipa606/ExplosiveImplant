using RimWorld;
using Verse;

namespace Explosive_Implant
{
    internal class ThoughtWorker_Hediff_ExplosiveImplant : ThoughtWorker_Hediff
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            var t = base.CurrentStateInternal(p);
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