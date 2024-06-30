using Dalamud.Game.ClientState.JobGauge.Enums;
using Dalamud.Game.ClientState.JobGauge.Types;
using ECommons.DalamudServices;
using System;
using System.Runtime.InteropServices;
using XIVSlothCombo.Services;

namespace XIVSlothCombo.Data;

public unsafe class TmpSCHGauge
{
    public byte Aetherflow => Struct->Aetherflow;

    public byte FairyGauge => Struct->FairyGauge;

    public short SeraphTimer => Struct->SeraphTimer;

    public DismissedFairy DismissedFairy => (DismissedFairy)Struct->DismissedFairy;

    private protected TmpScholarGauge* Struct;

    public TmpSCHGauge()
    {
        Struct = (TmpScholarGauge*)Service.JobGauges.Get<SCHGauge>().Address;
    }
}

public unsafe class TmpPCTGauge
{
    public byte PalleteGauge => Struct->PalleteGauge;

    public byte Paint => Struct->Paint;

    public bool CreatureMotifDrawn => Struct->CreatureMotifDrawn;

    public bool WeaponMotifDrawn => Struct->WeaponMotifDrawn;

    public bool LandscapeMotifDrawn => Struct->LandscapeMotifDrawn;

    public bool MooglePortraitReady => Struct->MooglePortraitReady;

    private protected PictoGauge* Struct;

    public byte GetOffset(int offset)
    {
        var val = IntPtr.Add(Address, offset);
        return Marshal.ReadByte(val);
    }

    private nint Address;
    public TmpPCTGauge()
    {
        Address = Svc.SigScanner.GetStaticAddressFromSig("48 8B 3D ?? ?? ?? ?? 33 ED") + 0x8;
        Struct = (PictoGauge*)Address;
    }
}

public unsafe class TmpVPRGauge
{
    public byte RattlingCoilStacks => Struct->RattlingCoilStacks;

    public byte SerpentsOfferings => Struct->SerpentsOfferings;

    private protected ViperGauge* Struct;

    public byte GetOffset(int offset)
    {
        var val = IntPtr.Add(Address, offset);
        return Marshal.ReadByte(val);
    }

    private nint Address;
    public TmpVPRGauge()
    {
        Address = Svc.SigScanner.GetStaticAddressFromSig("48 8B 3D ?? ?? ?? ?? 33 ED") + 0x8;
        Struct = (ViperGauge*)Address;
    }
}

[StructLayout(LayoutKind.Explicit, Size = 0x10)]
public struct TmpScholarGauge
{
    [FieldOffset(0x08)] public byte Aetherflow;
    [FieldOffset(0x09)] public byte FairyGauge;
    [FieldOffset(0x0A)] public short SeraphTimer;
    [FieldOffset(0x0C)] public byte DismissedFairy;
}

[StructLayout(LayoutKind.Explicit, Size = 0x10)]
public struct PictoGauge
{
    [FieldOffset(0x08)] public byte PalleteGauge;
    [FieldOffset(0x0A)] public byte Paint;
    [FieldOffset(0x0B)] public CanvasFlags CanvasFlags;
    [FieldOffset(0x0C)] public CreatureFlags CreatureFlags;

    public bool CreatureMotifDrawn => CanvasFlags.HasFlag(CanvasFlags.Pom) || CanvasFlags.HasFlag(CanvasFlags.Wing); //TODO Update at level 96
    public bool WeaponMotifDrawn => CanvasFlags.HasFlag(CanvasFlags.Weapon);
    public bool LandscapeMotifDrawn => CanvasFlags.HasFlag(CanvasFlags.Landscape);
    public bool MooglePortraitReady => CreatureFlags.HasFlag(CreatureFlags.Wings);
}

[StructLayout(LayoutKind.Explicit, Size = 0x10)]
public struct ViperGauge
{
    [FieldOffset(0x08)] public byte RattlingCoilStacks;
    [FieldOffset(0x0A)] public byte SerpentsOfferings;

}

[Flags]
public enum CanvasFlags : byte
{
    Pom = 1,
    Wing = 2,
    Weapon = 16,
    Landscape = 32,
}

[Flags]
public enum CreatureFlags : byte
{
    Pom = 1,
    Wings = 16
}