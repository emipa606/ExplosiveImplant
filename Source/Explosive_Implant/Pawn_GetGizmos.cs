using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace Explosive_Implant;

[HarmonyPatch]
public static class Pawn_GetGizmos
{
    public static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(Pawn), nameof(Pawn.GetGizmos));
        yield return AccessTools.Method(typeof(Corpse), nameof(Corpse.GetGizmos));
    }

    public static IEnumerable<Gizmo> Postfix(IEnumerable<Gizmo> values, object __instance)
    {
        if (values != null && values.Any())
        {
            foreach (var gizmo in values)
            {
                yield return gizmo;
            }
        }

        if (__instance is not Pawn pawn)
        {
            if (__instance is Corpse corpse)
            {
                pawn = corpse.InnerPawn;
            }
            else
            {
                yield break;
            }
        }

        var hediffs = pawn.health?.hediffSet?.hediffs;
        if (hediffs == null || !hediffs.Any())
        {
            yield break;
        }

        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < hediffs.Count; i++)
        {
            if (!Main.ExplosiveDefs.Contains(hediffs[i].def.defName))
            {
                continue;
            }

            if (hediffs[i] is not HediffWithComps_Explosion explosion)
            {
                continue;
            }

            var imageName = "Detonate";
            if (hediffs[i].def.defName != "ExplosiveImplant")
            {
                imageName = hediffs[i].def.defName;

                if (pawn.Dead && hediffs[i].def.defName == "KnockoutImplant")
                {
                    yield break;
                }
            }

            yield return new Command_Action
            {
                action = explosion.Explode,
                defaultLabel = "ExIm.DetonateSpecific".Translate(hediffs[i].Label),
                defaultDesc = "ExIm.DetonateDesc".Translate(),
                icon = ContentFinder<Texture2D>.Get($"UI/Buttons/{imageName}")
            };
        }
    }
}