using OWML.ModHelper;
using HikersMod;
using HarmonyLib;
using UnityEngine;
using SmolHatchling;

namespace HikersModSHCompatibility
{
    public class HikersModSHAddonController : ModBehaviour
    {
        public void Awake()
        {
            Harmony.CreateAndPatchAll(typeof(HikersModSHAddonController));
        }

        public void Start()
        {
            ModHelper.Console.WriteLine("Hiker's Mod Compatibility for Smol Hatchling is ready to go!", OWML.Common.MessageType.Success);
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(HikersModController), nameof(HikersModController.UpdateAnimSpeed))]
        public static bool UpdateAnimSpeed(HikersModController __instance)
        {
            float multiplier = __instance._characterController._acceleration / __instance._groundAccel * SmolHatchlingController.Instance._animSpeed;
            if (__instance._moveSpeed == MoveSpeed.Walking) __instance._animController._animator.speed = Mathf.Max(__instance._walkSpeed / 6 * multiplier, multiplier);
            else if (__instance._moveSpeed == MoveSpeed.DreamLantern) __instance._animController._animator.speed = Mathf.Max(__instance._dreamLanternSpeed / 6 * multiplier, multiplier);
            else __instance._animController._animator.speed = Mathf.Max(__instance._characterController._runSpeed / 6 * multiplier, multiplier);
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(SmolHatchlingController), nameof(SmolHatchlingController.UpdatePlayerScale))]
        public static void OnSmolHatchlingSizeChanged(SmolHatchlingController __instance)
        {
            if (!__instance.IsCorrectScene() || !__instance._characterLoaded) return;
            HikersModController.Instance.UpdateAnimSpeed();
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(HikersModController), nameof(HikersModController.ChangeAttributes))]
        public static void SetStoryModeAttributes(HikersModController __instance)
        {
            if (StoryController.Instance._storyEnabledNow)
            {
                {
                    __instance._normalSpeed = 4;
                    __instance._walkSpeed = 2;
                    __instance._jumpPower = 5;
                    __instance._sprintSpeed = 8;
                }
            };
        }
    }
}