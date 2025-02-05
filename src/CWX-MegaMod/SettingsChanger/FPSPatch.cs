// using SPT.Reflection.CodeWrapper;
// using SPT.Reflection.Patching;
// using SPT.Reflection.Utils;
// using EFT;
// using HarmonyLib;
// using System.Collections.Generic;
// using System.Linq;
// using System.Reflection;
// using System.Reflection.Emit;
//
// namespace CWX_MegaMod.SettingsChanger
// {
//     public class FPSPatch : ModulePatch
//     {
//         protected override MethodBase GetTargetMethod()
//         {
//             return AccessTools.Method(typeof(TarkovApplication.Struct366), nameof(TarkovApplication.Struct366.MoveNext));
//         }
//
//         [PatchTranspiler]
//         public static IEnumerable<CodeInstruction> PatchTranspile(ILGenerator generator, IEnumerable<CodeInstruction> instructions)
//         {
//             var codes = new List<CodeInstruction>(instructions);
//             
//             // search for the code where the FPS settings are used
//             var searchCode = new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(GameGraphicsClass), nameof(GameGraphicsClass.SetFramerateLimits)));
//             var searchIndex = -1;
//             
//             for (int i = 0; i < codes.Count; i++)
//             {
//                 if (codes[i].opcode == searchCode.opcode && codes[i].operand == searchCode.operand)
//                 {
//                     searchIndex = i;
//                     break;
//                 }
//             }
//             
//             if (searchIndex == -1)
//             {
//                 Logger.LogError($"Patch {MethodBase.GetCurrentMethod().Name} Failed! Could not find reference code!");
//                 return instructions;
//             }
//             
//             // Move back by 3. this is the start of the IL chain we are interested in
//             // also make a copy of the operand and opcode for this Var as we'll need it later
//             searchIndex -= 3;
//             var opcodeToUse = codes[searchIndex].opcode;
//             var operandToUse = codes[searchIndex].operand;
//             
//             var newCodes = CodeGenerator.GenerateInstructions(new List<Code>()
//             {
//                 new Code(OpCodes.Ldfld, typeof(GClass1365), nameof(GClass1365.ClientSettings)), // load field clientsettings
//                 new Code(OpCodes.Ldfld, typeof(GClass1464), nameof(GClass1464.FramerateLimit)), // load field framratelimit
//                 new Code(OpCodes.Ldc_I4, 240), // load int 240
//                 // new Code(OpCodes.Stfld, typeof(GClass1464.GClass1466), nameof(GClass1464.GClass1466.MaxFramerateGameLimit)), // store value into maxframerategamelimit
//                 // new Code(opcodeToUse, operandToUse), // use previously found opcode and operand
//                 // new Code(OpCodes.Ldfld, typeof(GClass1365), "ClientSettings"), // load field clientsettings
//                 // new Code(OpCodes.Ldfld, typeof(GClass1464), "FramerateLimit"), // load field frameratelimit
//                 // new Code(OpCodes.Ldc_I4, 240), // load int 240
//                 // new Code(OpCodes.Stfld, typeof(GClass1464.GClass1466), "MaxFramerateLobbyLimit"), // store value into maxframeratelobbylimit
//                 // new Code(opcodeToUse, operandToUse), // use previously found opcode and operand
//             });
//
//             for (int i = 0; i < newCodes.Count; i++)
//             {
//                 Logger.LogError($"new codes: {newCodes[i].opcode}, operand: {newCodes[i].operand ?? ""}");
//             }
//             
//             codes.InsertRange(searchIndex, newCodes);
//             
//             return codes.AsEnumerable();
//         }
//     }
// }