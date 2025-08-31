using System;
using _GameFolders.Scripts.Objects;

namespace _GameFolders.Scripts.Data.ValueObjects
{
    [Serializable]
    public struct JewelryToSpawnData
    {
        public Jewelry targetJewelry;
        public int requiredMatchCount;
    }
}