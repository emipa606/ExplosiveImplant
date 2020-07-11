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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using HarmonyLib;
using RimWorld;
using Verse;
using UnityEngine;

namespace Explosive_Implant
{
    [StaticConstructorOnStartup]
    class main
    {
        static main()
        {
            var harmony = new Harmony("com.ma1ta.Explosive.Implant");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(GizmoGridDrawer))]
    [HarmonyPatch("DrawGizmoGrid")]
    class Patch_AddGizmos
    {
        [HarmonyPrefix]
        static bool Prefix_AddGizmos(ref IEnumerable<Gizmo> gizmos)
        {
            if (GlobalVariables.list_obj.Count != 0 && GlobalVariables.pawnIsSelected.Count != 0)
            {
                HediffWithComps_Explosion objToRemove = null;
                int i = 0;
                foreach (HediffWithComps_Explosion obj in GlobalVariables.list_obj)
                {
                    if (GlobalVariables.pawnIsSelected[i])
                    {
                        if (!obj.host.Dead)
                        {
                            Command_Action command = new Command_Action();
                            command.action = obj.Explode;
                            command.defaultLabel = "Detonate";
                            command.defaultDesc = "Make the explosive implant explode.";
                            command.icon = ContentFinder<Texture2D>.Get("UI/Buttons/Detonate", true);

                            List<Gizmo> l = new List<Gizmo>(gizmos);
                            l.Add(command);
                            IEnumerable<Gizmo> gizmos_updated = from gizmo in l select gizmo;
                            gizmos = gizmos_updated;
                        }
                    }
                    if(obj.host.Dead)
                    {
                        obj.host.health.RemoveHediff(obj);
                        objToRemove = obj;
                    }
                    i++;
                }
                GlobalVariables.pawnIsSelected.Clear();
                if (objToRemove != null)
                {
                    GlobalVariables.list_obj.Remove(objToRemove);
                    objToRemove = null;
                }
            }
            return true;
        }
    }

    [HarmonyPatch(typeof(Selector))]
    [HarmonyPatch("SelectorOnGUI")]
    class Patch_GetSelector
    {
        [HarmonyPostfix]
        static void Postfix_GetSelector(Selector __instance)
        {
            if (GlobalVariables.list_obj.Count != 0)
            {
                foreach (HediffWithComps_Explosion obj in GlobalVariables.list_obj)
                {
                    GlobalVariables.pawnIsSelected.Add(__instance.IsSelected(obj.host));
                }
            }
        }
    }
}
