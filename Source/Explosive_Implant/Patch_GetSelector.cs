using HarmonyLib;
using RimWorld;

namespace Explosive_Implant;

[HarmonyPatch(typeof(Selector))]
[HarmonyPatch("SelectorOnGUI")]
internal class Patch_GetSelector
{
    [HarmonyPostfix]
    private static void Postfix_GetSelector(Selector __instance)
    {
        if (GlobalVariables.list_obj.Count == 0)
        {
            return;
        }

        foreach (var obj in GlobalVariables.list_obj)
        {
            GlobalVariables.pawnIsSelected.Add(__instance.IsSelected(obj.host));
        }
    }
}