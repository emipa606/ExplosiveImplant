using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace Explosive_Implant;

[HarmonyPatch(typeof(TerrorUtility), nameof(TerrorUtility.GetTerrorLevel))]
public static class TerrorUtility_GetTerrorLevel
{
    public static void Postfix(Pawn pawn, ref float __result)
    {
        if (__result >= 1f)
        {
            return;
        }

        if (!pawn.IsSlave)
        {
            return;
        }

        var hediffs = pawn.health?.hediffSet?.hediffs;
        if (hediffs == null || !hediffs.Any())
        {
            return;
        }

        if (hediffs.Where(t => Main.ExplosiveDefs.Contains(t.def.defName)).All(t => t.def.defName == "KnockoutImplant"))
        {
            return;
        }

        __result = 1f;
    }
}