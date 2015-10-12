/**********************************************************************************************************************
 * dwad-net (https://github.com/mkloubert/dwad-net)                                                                   *
 *                                                                                                                    *
 * Copyright (c) 2015, Marcel Joachim Kloubert <marcel.kloubert@gmx.net>                                              *
 * All rights reserved.                                                                                               *
 *                                                                                                                    *
 * Redistribution and use in source and binary forms, with or without modification, are permitted provided that the   *
 * following conditions are met:                                                                                      *
 *                                                                                                                    *
 * 1. Redistributions of source code must retain the above copyright notice, this list of conditions and the          *
 *    following disclaimer.                                                                                           *
 *                                                                                                                    *
 * 2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the       *
 *    following disclaimer in the documentation and/or other materials provided with the distribution.                *
 *                                                                                                                    *
 * 3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote    *
 *    products derived from this software without specific prior written permission.                                  *
 *                                                                                                                    *
 *                                                                                                                    *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, *
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE  *
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, *
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR    *
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,  *
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE   *
 * USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.                                           *
 *                                                                                                                    *
 **********************************************************************************************************************/

namespace MarcelJoachimKloubert.DWAD.WADs.Lumps.Things
{
    /// <summary>
    /// List of known DOOM things (<see href="http://doom.wikia.com/wiki/Thing_types" />).
    /// </summary>
    public enum DOOMThingType : short
    {
        /// <summary>
        /// Unknown
        /// </summary>
        UNKNOWN = 0x0000,

        #region Ammunition

        /// <summary>
        /// Ammo clip
        /// </summary>
        AmmoClip = 0x07D7,

        /// <summary>
        /// Box of ammo
        /// </summary>
        BoxOfAmmo = 0x0800,

        /// <summary>
        /// Box of rockets
        /// </summary>
        BoxOfRockets = 0x07FE,

        /// <summary>
        /// Box of shells
        /// </summary>
        BoxOfShells = 0x0801,

        /// <summary>
        /// Cell charge
        /// </summary>
        CellCharge = 0x07FF,

        /// <summary>
        /// Cell charge pack
        /// </summary>
        CellChargePack = 0x0011,

        /// <summary>
        /// Rocket
        /// </summary>
        Rocket = 0x07DA,

        /// <summary>
        /// Shotgun shells
        /// </summary>
        ShotgunShells = 0x07D8,

        #endregion Ammunition

        #region Artifacts

        /// <summary>
        /// Berserk
        /// </summary>
        Berserk = 0x07E7,

        /// <summary>
        /// Computer map
        /// </summary>
        ComputerMap = 0x07EA,

        /// <summary>
        /// Health potion
        /// </summary>
        HealthPoition = 0x07DE,

        /// <summary>
        /// Invisibility
        /// </summary>
        Invisibility = 0x07E8,

        /// <summary>
        /// Invulnerability
        /// </summary>
        Invulnerability = 0x07E6,

        /// <summary>
        /// Light amplification visor
        /// </summary>
        LightAmplicationVisor = 0x07FD,

        /// <summary>
        /// Megasphere
        /// </summary>
        Megasphere = 0x0053,

        /// <summary>
        /// Soul sphere
        /// </summary>
        SoulSphere = 0x07DD,

        /// <summary>
        /// Spiritual armor
        /// </summary>
        SpiritualArmor = 0x07DF,

        #endregion Artifacts

        #region Decorations

        /// <summary>
        /// Bloody mess #1
        /// </summary>
        BloodyMess1 = 0x000A,

        /// <summary>
        /// Bloody mess #2
        /// </summary>
        BloodyMess2 = 0x000C,

        /// <summary>
        /// Candle
        /// </summary>
        Candle = 0x0022,

        /// <summary>
        /// Dead cacodemon
        /// </summary>
        DeadCacodemon = 0x0016,

        /// <summary>
        /// Dead demon
        /// </summary>
        DeadDemon = 0x0015,

        /// <summary>
        /// Dead former human
        /// </summary>
        DeadFormerHuman = 0x0012,

        /// <summary>
        /// Dead former sergeant
        /// </summary>
        DeadFormerSergeant = 0x0013,

        /// <summary>
        /// Dead imp
        /// </summary>
        DeadImp = 0x0014,

        /// <summary>
        /// Dead player
        /// </summary>
        DeadPlayer = 0x000F,

        /// <summary>
        /// Hanging leg #1
        /// </summary>
        HangingLeg1 = 0x003E,

        /// <summary>
        /// Hanging pair of legs #1
        /// </summary>
        HangingPairOfLegs1 = 0x003C,

        /// <summary>
        /// Hanging victim, arms out #1
        /// </summary>
        HangingVictimArmsOut1 = 0x003B,

        /// <summary>
        /// Hanging victim, one-legged #1
        /// </summary>
        HangingVictimOneLegged1 = 0x003D,

        /// <summary>
        /// Hanging victim, twitching #1
        /// </summary>
        HangingVictimTwitching1 = 0x003F,

        /// <summary>
        /// Dead lost soul (invisible)
        /// </summary>
        InvisibleDeadLostSoul = 0x0017,

        /// <summary>
        /// Pool of blood #1
        /// </summary>
        PoolOfBlood1 = 0x004F,

        /// <summary>
        /// Pool of blood #2
        /// </summary>
        PoolOfBlood2 = 0x0050,

        /// <summary>
        /// Pool of blood and flesh
        /// </summary>
        PoolOfBloodAndFlesh = 0x0018,

        /// <summary>
        /// Pool of brains
        /// </summary>
        PoolOfBrains = 0x0051,

        #endregion Decorations

        #region Keys

        /// <summary>
        /// Blue keycard
        /// </summary>
        BlueKeycard = 0x0005,

        /// <summary>
        /// Blue skull key
        /// </summary>
        BlueSkullKey = 0x0028,

        /// <summary>
        /// Red keycard
        /// </summary>
        RedKeycard = 0x000D,

        /// <summary>
        /// Red skull key
        /// </summary>
        RedSkullKey = 0x0026,

        /// <summary>
        /// Yellow keycard
        /// </summary>
        YellowKeycard = 0x0006,

        /// <summary>
        /// Yellow skull key
        /// </summary>
        YellowSkullKey = 0x0027,

        #endregion Keys

        #region Monsters

        /// <summary>
        /// Arachnotron
        /// </summary>
        Arachnotron = 0x0044,

        /// <summary>
        /// Arch-Vile
        /// </summary>
        ArchVile = 0x0040,

        /// <summary>
        /// Baron of Hell
        /// </summary>
        BaronOfHell = 0x0BBB,

        /// <summary>
        /// Cacodemon
        /// </summary>
        Cacodemon = 0x0BBD,

        /// <summary>
        /// Chaingunner
        /// </summary>
        Chaingunner = 0x0041,

        /// <summary>
        /// Commander Keen
        /// </summary>
        CommanderKeen = 0x0048,

        /// <summary>
        /// Cyberdemon
        /// </summary>
        Cyberdemon = 0x0010,

        /// <summary>
        /// Demon
        /// </summary>
        Demon = 0x0BBA,

        /// <summary>
        /// FormerHumanTrooper
        /// </summary>
        FormerHumanTrooper = 0x0BBC,

        /// <summary>
        /// Former Human Sergeant
        /// </summary>
        FormerHumanSergeant = 0x0009,

        /// <summary>
        /// Hell Knight
        /// </summary>
        HellKnight = 0x0045,

        /// <summary>
        /// Imp
        /// </summary>
        Imp = 0x0BB9,

        /// <summary>
        /// Lost Soul
        /// </summary>
        LostSoul = 0x0BBE,

        /// <summary>
        /// Mancubus
        /// </summary>
        Mancubus = 0x0043,

        /// <summary>
        /// Pain Elemental
        /// </summary>
        PainElemental = 0x0047,

        /// <summary>
        /// Revenant
        /// </summary>
        Revenant = 0x0042,

        /// <summary>
        /// Spectre
        /// </summary>
        Spectre = 0x003A,

        /// <summary>
        /// Spider Mastermind
        /// </summary>
        SpiderMastermind = 0x0007,

        /// <summary>
        /// Wolfenstein soldier
        /// </summary>
        WolfensteinSoldier = 0x0054,

        #endregion Monsters

        #region Obstacles

        /// <summary>
        /// Barrel
        /// </summary>
        Barrel = 0x07F3,

        /// <summary>
        /// Burning barrel
        /// </summary>
        BurningBarrel = 0x0046,

        /// <summary>
        /// Burnt tree
        /// </summary>
        BurntTree = 0x002B,

        /// <summary>
        /// Candelabra
        /// </summary>
        Candelabra = 0x0023,

        /// <summary>
        /// Evil eye
        /// </summary>
        EvilEye = 0x0029,

        /// <summary>
        /// Five skulls &quot;shish kebab&quot;
        /// </summary>
        FiveSkullsShishKebab = 0x001C,

        /// <summary>
        /// Floating skull
        /// </summary>
        FloatingSkull = 0x002A,

        /// <summary>
        /// Floor lamp
        /// </summary>
        FloorLamp = 0x07EC,

        /// <summary>
        /// Hanging leg #2
        /// </summary>
        HangingLeg2 = 0x0035,

        /// <summary>
        /// Hanging pair of legs #2
        /// </summary>
        HangingPairOfLegs2 = 0x0034,

        /// <summary>
        /// Hanging torso, brain removed
        /// </summary>
        HangingTorsoBrainRemoved = 0x004E,

        /// <summary>
        /// Hanging torso, looking down
        /// </summary>
        HangingTorsoLookingDown = 0x004B,

        /// <summary>
        /// Hanging torso, looking up
        /// </summary>
        HangingTorsoLookingUp = 0x004D,

        /// <summary>
        /// Hanging torso, open skull
        /// </summary>
        HangingTorsoOpenSkull = 0x004C,

        /// <summary>
        /// Hanging victim, arms out #2
        /// </summary>
        HangingVictimArmsOut = 0x0032,

        /// <summary>
        /// Hanging victim, guts and brain removed
        /// </summary>
        HangingVictimGutsAndBrainRemoved = 0x004A,

        /// <summary>
        /// Hanging victim, guts removed
        /// </summary>
        HangingVictimGutsRemoved = 0x0049,

        /// <summary>
        /// Hanging victim, one-legged #2
        /// </summary>
        HangingVictimOneLegged2 = 0x0033,

        /// <summary>
        /// Hanging victim, twitching #2
        /// </summary>
        HangingVictimTwitching2 = 0x0031,

        /// <summary>
        /// Impaled human
        /// </summary>
        ImpaledHuman = 0x0019,

        /// <summary>
        /// Large brown tree
        /// </summary>
        LargeBrownTree = 0x0036,

        /// <summary>
        /// Pile of skulls and candles
        /// </summary>
        PileOfSkullsAndCandles = 0x001D,

        /// <summary>
        /// Short blue firestick
        /// </summary>
        ShortBlueFirestick = 0x0037,

        /// <summary>
        /// Short green firestick
        /// </summary>
        ShortGreenFirestick = 0x0038,

        /// <summary>
        /// Short green pillar
        /// </summary>
        ShortGreenPillar = 0x001F,

        /// <summary>
        /// Short green pillar with beating heart
        /// </summary>
        ShortGreenPillarWithBeatingHeart = 0x0024,

        /// <summary>
        /// Short red firestick
        /// </summary>
        ShortRedFirestick = 0x0039,

        /// <summary>
        /// Short red pillar
        /// </summary>
        ShortRedPillar = 0x0021,

        /// <summary>
        /// Short red pillar with skull
        /// </summary>
        ShortRedPillarWithSkull = 0x0025,

        /// <summary>
        /// Short techno floor lamp
        /// </summary>
        ShortTechnoFloorLamp = 0x0056,

        /// <summary>
        /// Skull on a pole
        /// </summary>
        SkullOnAPole = 0x001B,

        /// <summary>
        /// Stalagmite
        /// </summary>
        Stalagmite = 0x002F,

        /// <summary>
        /// Tall blue firestick
        /// </summary>
        TallBlueFirestick = 0x002C,

        /// <summary>
        /// Tall green firestick
        /// </summary>
        TallGreenFirestick = 0x002D,

        /// <summary>
        /// Tall green pillar
        /// </summary>
        TallGreenPillar = 0x001E,

        /// <summary>
        /// Tall red firestick
        /// </summary>
        TallRedFirestick = 0x002E,

        /// <summary>
        /// Tall red pillar
        /// </summary>
        TallRedPillar = 0x0020,

        /// <summary>
        /// Tall techno floor lamp
        /// </summary>
        TallTechnoFloorLamp = 0x0055,

        /// <summary>
        /// Tall techno pillar
        /// </summary>
        TallTechnoPillar = 0x0030,

        /// <summary>
        /// Twitching impaled human
        /// </summary>
        TwitchingImpaledHuman = 0x001A,

        #endregion Obstacles

        #region Other

        /// <summary>
        /// Boss Brain
        /// </summary>
        BossBrain = 0x0058,

        /// <summary>
        /// Deathmatch start
        /// </summary>
        DeathmatchStart = 0x000B,

        /// <summary>
        /// Player 1 start
        /// </summary>
        Player1Start = 0x001,

        /// <summary>
        /// Player 2 start
        /// </summary>
        Player2Start = 0x002,

        /// <summary>
        /// Player 3 start
        /// </summary>
        Player3Start = 0x003,

        /// <summary>
        /// Player 4 start
        /// </summary>
        Player4Start = 0x004,

        /// <summary>
        /// Spawn shooter
        /// </summary>
        SpawnShooter = 0x0059,

        /// <summary>
        /// Spawn spot
        /// </summary>
        SpawnSpot = 0x0057,

        /// <summary>
        /// Teleport landing
        /// </summary>
        TeleportLanding = 0x000E,

        #endregion Other

        #region Powerups

        /// <summary>
        /// Backpack
        /// </summary>
        Backpack = 0x0008,

        /// <summary>
        /// Blue armor
        /// </summary>
        BlueArmor = 0x07E3,

        /// <summary>
        /// Green armor
        /// </summary>
        GreenArmor = 0x07E2,

        /// <summary>
        /// Medikit
        /// </summary>
        Medikit = 0x07DC,

        /// <summary>
        /// Radiation suit
        /// </summary>
        RadiationSuit = 0x07E9,

        /// <summary>
        /// Stimpack
        /// </summary>
        Stimpack = 0x07DB,

        #endregion Powerups

        #region Weapons

        /// <summary>
        /// BFG 9000
        /// </summary>
        BFG9000 = 0x07D6,

        /// <summary>
        /// Chaingun
        /// </summary>
        Chaingun = 0x07D2,

        /// <summary>
        /// Chainsaw
        /// </summary>
        Chainsaw = 0x07D5,

        /// <summary>
        /// Plasma rifle
        /// </summary>
        PlasmaRifle = 0x07D4,

        /// <summary>
        /// Rocket launcher
        /// </summary>
        RocketLauncher = 0x07D3,

        /// <summary>
        /// Shotgun
        /// </summary>
        Shotgun = 0x07D1,

        /// <summary>
        /// Super shotgun
        /// </summary>
        SuperShotgun = 0x0052,

        #endregion Weapons
    }
}