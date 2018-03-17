﻿using System;

namespace POGOLib.Official.Util.Encryption.PokeHash
{
    /// <summary>
    ///     This is the PCrypt used by POGOLib. It should always match the used PokeHash version.
    /// 
    ///     Android version: 0.61.0
    ///     IOS version: 1.31.0
    /// </summary>
    internal static class PCryptPokeHash
    {
        private static readonly byte[] KEY = {
            0x4F,  0xEB,  0x1C,  0xA5,  0xF6,  0x1A,  0x67,  0xCE,
            0x43,  0xF3,  0xF0,  0x0C,  0xB1,  0x23,  0x88,  0x35,
            0xE9,  0x8B,  0xE8,  0x39,  0xD8,  0x89,  0x8F,  0x5A,
            0x3B,  0x51,  0x2E,  0xA9,  0x47,  0x38,  0xC4,  0x14
        };

        private static byte[] xbox = {
            0x01,
            0x00,
            0x83, 0x57, 0x47, 0x28, 0x1c, 0x84, 0x5c, 0xf0, 0x25, 0xcc, 0x14, 0xd1, 0xe4, 0xe0, 0x4b, 0x4c,
            0x68, 0x20, 0x72, 0x37, 0x34, 0x7b, 0x23, 0xf3, 0x7d, 0x62, 0x8c, 0xa7, 0xe2, 0xa8, 0x88, 0x6e,
            0x27, 0x74, 0x3e, 0x94, 0x2a, 0x6d, 0x3b, 0xa5, 0x7a, 0x41, 0xa3, 0x13, 0x8b, 0x31, 0x42, 0x09,
            0xb4, 0x16, 0x2f, 0xb7, 0x06, 0x04, 0x75, 0x39, 0x67, 0xc0, 0x30, 0xde, 0xa4, 0xf8, 0xd8, 0x19,
            0xf7, 0xf9, 0x2d, 0xae, 0xc2, 0xe9, 0xcb, 0xc1, 0x1b, 0x5e, 0xc3, 0x08, 0xaa, 0x4f, 0xd4, 0xbf,
            0x35, 0x63, 0x2e, 0x8f, 0x9f, 0x0f, 0x8a, 0x97, 0xb8, 0x3a, 0xa6, 0x48, 0x98, 0x11, 0x71, 0x89,
            0x6c, 0x9b, 0x0a, 0x61, 0xa9, 0x86, 0x22, 0xe3, 0x03, 0x7f, 0x4a, 0x99, 0x00, 0xab, 0xed, 0xf2,
            0x9a, 0xba, 0x52, 0x29, 0x1e, 0xbe, 0xfc, 0xa0, 0x65, 0x6a, 0x78, 0xca, 0x69, 0xd0, 0x21, 0x49,
            0xbd, 0x4d, 0x2c, 0x7e, 0x53, 0xb5, 0xe6, 0xdc, 0x60, 0x8e, 0xfd, 0x17, 0x82, 0x0e, 0x9c, 0x4e,
            0xaf, 0xc5, 0xc4, 0x5d, 0x81, 0xf4, 0x02, 0x5b, 0x0b, 0x50, 0xac, 0x45, 0x95, 0x5f, 0x38, 0xd3,
            0x76, 0xc7, 0x07, 0x90, 0x92, 0x79, 0x15, 0x77, 0xdb, 0x12, 0x3d, 0xbc, 0x10, 0x1a, 0x51, 0xb9,
            0x32, 0xbb, 0x26, 0x56, 0xdd, 0xd9, 0xe5, 0x7c, 0xe8, 0xe7, 0xad, 0xd2, 0xf6, 0xee, 0xcf, 0xfe,
            0x87, 0x66, 0x64, 0xf5, 0xcd, 0xe1, 0xc9, 0xfa, 0x0c, 0x01, 0x6b, 0x3f, 0x0d, 0xda, 0x96, 0x40,
            0xa2, 0x1f, 0x5a, 0x24, 0xeb, 0x59, 0xec, 0x44, 0x43, 0x91, 0xb0, 0xb2, 0xd7, 0x54, 0x2b, 0xce,
            0x33, 0xff, 0x58, 0x18, 0x93, 0x46, 0xc8, 0xdf, 0x3c, 0xfb, 0x8d, 0xb1, 0x55, 0xd5, 0x6f, 0x70,
            0xef, 0x9d, 0xa1, 0x9e, 0xb6, 0xea, 0xc6, 0xf1, 0x80, 0x1d, 0x05, 0x73, 0xd6, 0xb3, 0x36, 0x85
        };

        private static void Encrypt_cipher(byte[] src, int size)
        {
            var newxbox = new byte[xbox.Length];
            xbox.CopyTo(newxbox, 0);
            int a4 = size - 1;
            int srci = 0;
            byte v4 = newxbox[0];
            byte v5 = newxbox[1];
            for (; a4 != 0; v4 = (byte)(v4 + 1))
            {
                --a4;
                byte v7 = newxbox[2 + v4];
                v5 = (byte)(v5 + v7);
                byte v9 = newxbox[2 + v5];
                newxbox[2 + v4] = v9;
                newxbox[2 + v5] = v7;
                byte v10 = (byte)(v9 + v7);
                src[srci++] ^= newxbox[2 + v10];
            }
            newxbox[0] = v4;
            newxbox[1] = v5;
        }

        /**
			* Encrypts the given signature
			*
			* @param input input data
			* @param msSinceStart time since start
			* @return encrypted signature
			*/

        public static byte[] Encrypt(byte[] uncryptedSignature, uint msSinceStart)
        {
            try
            {
                Rand rand = new Rand(msSinceStart);

                object[] key = TwoFish.MakeKey(KEY);

                var xor_byte = new byte[TwoFish.BLOCK_SIZE];

                for (int i = 0; i < TwoFish.BLOCK_SIZE; ++i)
                    xor_byte[i] = (byte)rand.Next();

                int block_count = (uncryptedSignature.Length + 256) / 256;
                int output_size = 4 + (block_count * 256) + 1;
                var output = new byte[output_size];
                output[0] = (byte)(msSinceStart >> 24);
                output[1] = (byte)(msSinceStart >> 16);
                output[2] = (byte)(msSinceStart >> 8);
                output[3] = (byte)msSinceStart;


                Array.Copy(uncryptedSignature, 0, output, 4, uncryptedSignature.Length);

                output[output_size - 2] = (byte)(256 - uncryptedSignature.Length % 256);

                for (int offset = 0; offset < block_count * 256; offset += TwoFish.BLOCK_SIZE)
                {
                    for (int i = 0; i < TwoFish.BLOCK_SIZE; i++)
                        output[4 + offset + i] ^= xor_byte[i];

                    byte[] block = TwoFish.blockEncrypt(output, offset + 4, key);
                    Array.Copy(block, 0, output, offset + 4, block.Length);
                    Array.Copy(output, 4 + offset, xor_byte, 0, TwoFish.BLOCK_SIZE);

                }
                output[output_size - 1] = 0x23;
                Encrypt_cipher(output, output_size);

                return output;
            }

            catch (Exception)
            {
                return null;
            }
        }

        public class Rand
        {
            private long state;

            public Rand(long state)
            {
                this.state = state;
            }

            public byte Next()
            {
                state = (state * 0x41C64E6D) + 0x3039;
                return (byte)((state >> 16) & 0xFF);
            }
        }
    }
}