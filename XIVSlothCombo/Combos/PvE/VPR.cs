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
            HindsbaneFang = 34613,
            FlankstingStrike = 34610,
            FlanksbaneFang = 34611;

        public static class Buffs
        {
            public const ushort
                FellhuntersVenom = 3659,
                FellskinsVenom = 3660,
                FlanksbaneVenom = 3646,
                FlankstungVenom = 3645,
                HindstungVenom = 3647,
                HindsbaneVenom = 3648,
                GrimhuntersVenom = 3649,
                GrimskinsVenom = 3650,
                HuntersVenom = 3657,
                SwiftskinsVenom = 3658;
        }

        public static class Debuffs
        {
            public const ushort
                NoxiousGnash = 3667;
        }

        public static class Config
        {

        }

        internal class VPR_ST_AdvancedMode : CustomCombo
        {
            protected internal override CustomComboPreset Preset { get; } = CustomComboPreset.VPR_ST_AdvancedMode;
            // internal static VPROpenerLogic VPROpener = new();

            protected override uint Invoke(uint actionID, uint lastComboMove, float comboTime, byte level)
            {
                //  VPRGauge? gauge = GetJobGauge<VPRGauge>();
                bool trueNorthReady = TargetNeedsPositionals() && HasCharges(All.TrueNorth) && !HasEffect(All.Buffs.TrueNorth);

                if (actionID is SteelFangs)
                {

                    if (IsEnabled(CustomComboPreset.VPR_ST_RangedUptime) &&
                        LevelChecked(WrithingSnap) && !InMeleeRange() && HasBattleTarget())
                        return WrithingSnap;

                    //1-2-3 Combo
                    if (CanWeave(actionID) &&
                        (WasLastAction(HindstingStrike) ||
                        WasLastAction(HindsbaneFang) ||
                        WasLastAction(FlankstingStrike) ||
                        WasLastAction(FlanksbaneFang)))
                        return OriginalHook(SerpentsTail1);

                    if (comboTime > 0)
                    {
                        if (lastComboMove is DreadFangs && LevelChecked(SwiftskinSting))
                            return OriginalHook(SwiftskinSting);

                        if (lastComboMove is SwiftskinSting && LevelChecked(HindstingStrike))
                        {
                            if (HasEffect(Buffs.HindstungVenom) || (!HasEffect(Buffs.HindsbaneVenom) && !HasEffect(Buffs.HindstungVenom)))
                            {
                                // If we are not on the rear
                                if (IsEnabled(CustomComboPreset.VPR_TrueNorthDynamic) &&
                                    trueNorthReady && !IsMoving && CanDelayedWeave(actionID) &&
                                    !OnTargetsRear())
                                    return All.TrueNorth;

                                return OriginalHook(HindstingStrike);
                            }

                            if (HasEffect(Buffs.HindsbaneVenom))
                            {
                                // If we are not on the rear
                                if (IsEnabled(CustomComboPreset.VPR_TrueNorthDynamic) &&
                                    trueNorthReady && CanDelayedWeave(actionID) && !IsMoving &&
                                    !OnTargetsRear())
                                    return All.TrueNorth;

                                return OriginalHook(HindsbaneFang);
                            }
                        }
                    }
                    return OriginalHook(DreadFangs);
                }

                return actionID;
            }
        }
    }
}
