using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using UnityEngine;
using Verse;

namespace Explosive_Implant;

[HarmonyPatch(typeof(GizmoGridDrawer))]
[HarmonyPatch("DrawGizmoGrid")]
internal class Patch_AddGizmos
{
    [HarmonyPrefix]
    private static bool Prefix_AddGizmos(ref IEnumerable<Gizmo> gizmos)
    {
        if (GlobalVariables.list_obj.Count == 0 || GlobalVariables.pawnIsSelected.Count == 0)
        {
            return true;
        }

        HediffWithComps_Explosion objToRemove = null;
        var i = 0;
        foreach (var obj in GlobalVariables.list_obj)
        {
            if (GlobalVariables.pawnIsSelected[i])
            {
                if (!obj.host.Dead)
                {
                    var command = new Command_Action
                    {
                        action = obj.Explode,
                        defaultLabel = "ExIm.Detonate".Translate(),
                        defaultDesc = "ExIm.DetonateDesc".Translate(),
                        icon = ContentFinder<Texture2D>.Get("UI/Buttons/Detonate")
                    };

                    var l = new List<Gizmo>(gizmos) { command };
                    var gizmos_updated = from gizmo in l select gizmo;
                    gizmos = gizmos_updated;
                }
            }

            if (obj.host.Dead)
            {
                obj.host.health.RemoveHediff(obj);
                objToRemove = obj;
            }

            i++;
        }

        GlobalVariables.pawnIsSelected.Clear();
        if (objToRemove == null)
        {
            return true;
        }

        GlobalVariables.list_obj.Remove(objToRemove);

        return true;
    }
}