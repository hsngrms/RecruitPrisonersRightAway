using HarmonyLib;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;

namespace RecruitPrisonersRightAway
    {
    public class RPRASubModule : MBSubModuleBase
        {
        protected override void OnSubModuleLoad()
            {
            base.OnSubModuleLoad();
            new Harmony("hsngrms.recruit.prisoners.rightaway").PatchAll();
            }
        protected override void OnBeforeInitialModuleScreenSetAsRoot()
            {
            base.OnBeforeInitialModuleScreenSetAsRoot();
            InformationManager.DisplayMessage(new InformationMessage("Recruit Prisoners Right Away mod enabled"));
            }
        }

    [HarmonyPatch(typeof(DefaultPrisonerRecruitmentCalculationModel), "GetConformityNeededToRecruitPrisoner")]
    internal class ConformityNeededToRecruitPrisonerModification
        {
        public static void Postfix(ref int __result)
            {
            try
                {
                __result = 0;
                }
            catch (System.Exception)
                {
                InformationManager.DisplayMessage(new InformationMessage(" Oops... Something went terribly wrong because of ConformityNeededToRecruitPrisonerModification ! "));
                }
            }
        }

    [HarmonyPatch(typeof(RecruitPrisonersCampaignBehavior), "GetRecruitableNumber")]
    internal class RecruitablePrisonerNumberModification
        {
        public static void Postfix(CharacterObject character, ref int __result)
            {
            try
                {
                if (!character.IsHero)
                    {
                    int prisonercount = MobileParty.MainParty.PrisonRoster.GetTroopCount(character);
                    __result = prisonercount;
                    }
                return;
                }
            catch (System.Exception)
                {
                InformationManager.DisplayMessage(new InformationMessage(" Oops... Something went terribly wrong because of RecruitablePrisonerNumberModification ! "));
                }
            }
        }

    }