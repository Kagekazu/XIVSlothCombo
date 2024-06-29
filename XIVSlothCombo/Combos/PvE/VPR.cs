using XIVSlothCombo.Combos.JobHelpers;
using XIVSlothCombo.CustomComboNS;

namespace XIVSlothCombo.Combos.PvE
{
    internal class VPR
    {
        public const byte JobID = 41;

        public const uint
            DreadFangs = 34607,
            DreadMaw = 34615,
            Dreadwinder = 34620,
            HuntersCoil = 34621,
            HuntersDen = 34624,
            HuntersSnap = 39166,
            PitofDread = 34623,
            RattlingCoil = 39189,
            Reawaken = 34626,
            SerpentsIre = 34647,
            SerpentsTail1 = 35920,
            SerpentsTail2 = 39183,
            Slither1 = 34646,
            Slither2 = 39184,
            SnakeScales = 39185,
            SteelFangs = 34606,
            SteelMaw = 34614,
            SwiftskinsCoil = 34622,
            SwiftskinsDen = 34625,
            Twinblood = 35922,
            Twinfang = 35921,
            UncoiledFury1 = 34633,
            UncoiledFury2 = 39168,
            Worldswallower = 39190,
            WrithingSnap = 34632,
            SwiftskinSting = 34609,
            TwinfangBite = 34636,
            TwinbloodBite = 34637,
            UncoiledTwinfang = 34644,
            UncoiledTwinblood = 34645,
            HindstingStrike = 34612,
            DeathRattle = 34634,
            HuntersSting = 34608,
            ;

        public static class Buffs
        {

        }

        public static class Debuffs
        {

        }

        public static class Config
        {

        }

        internal class VPR_ST_AdvancedMode : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.VPR_ST_AdvancedMode;
            internal static VPROpenerLogic VPROpener = new();

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                if (actionID is SteelFangs)
                {
                    //1-2-3 Combo
                    if (HasEffect(Buffs.SharperFangAndClaw))
                    {
                        // If we are not on the flank, but need to use Fangs, pop true north if not already up
                        if (IsEnabled(CustomComboPreset.DRG_TrueNorthDynamic) &&
                            trueNorthReady && allowedToTN && CanDelayedWeave(actionID) &&
                            !OnTargetsFlank() && !HasEffect(Buffs.RightEye))
                            return All.TrueNorth;

                        return OriginalHook(FangAndClaw);
                    }

                    if (HasEffect(Buffs.EnhancedWheelingThrust))
                    {
                        // If we are not on the rear, but need to use Wheeling, pop true north if not already up
                        if (IsEnabled(CustomComboPreset.DRG_TrueNorthDynamic) &&
                            trueNorthReady && allowedToTN && CanDelayedWeave(actionID) &&
                            !OnTargetsRear() && !HasEffect(Buffs.RightEye))
                            return All.TrueNorth;

                        return OriginalHook(WheelingThrust);
                    }

                    if (comboTime > 0)
                    {
                        if (lastComboMove is SteelFangs && LevelChecked(HuntersSting))
                            return OriginalHook(HuntersSting);

                        if (lastComboMove is HuntersSting && !HasEffect(Buffs.Equals)
                            return OriginalHook(DreadFangs);
                    }

                    return OriginalHook(DreadFangs);
                }

                return actionID;
            }
        }
    }
}
