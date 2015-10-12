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

using System;

namespace MarcelJoachimKloubert.DWAD.WADs.Lumps.Things
{
    /// <summary>
    /// List of DOOM flags for an <see cref="IDOOMThing" /> (<see href="http://doom.wikia.com/wiki/Thing" />).
    /// </summary>
    [Flags]
    public enum DOOMThingFlags : short
    {
        /// <summary>
        /// Thing is on skill levels 1 and 2
        /// </summary>
        Skill_1_and_2 = 0x0001,

        /// <summary>
        /// Thing is on skill levels 3
        /// </summary>
        Skill_3 = 0x0002,

        /// <summary>
        /// Thing is on skill levels 4 and 5
        /// </summary>
        Skill_4_and_5 = 0x0004,

        /// <summary>
        /// Thing is deaf.
        /// </summary>
        Deaf = 0x0008,

        /// <summary>
        /// Thing is not in single player
        /// </summary>
        NotInSinglePlayer = 0x0010,

        /// <summary>
        /// Thing is not in deathmatch (Boom)
        /// </summary>
        NotInDeathmatch = 0x0020,

        /// <summary>
        /// Thing is not in coop (Boom)
        /// </summary>
        NotInCoop = 0x0040,

        /// <summary>
        /// Friendly monster (MBF)
        /// </summary>
        FriendlyMonster = 0x0080,
    }
}