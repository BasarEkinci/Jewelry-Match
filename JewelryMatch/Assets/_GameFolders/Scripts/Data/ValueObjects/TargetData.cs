using System;
using _GameFolders.Scripts.Objects;

namespace _GameFolders.Scripts.Data.ValueObjects
{
    [Serializable]
    public struct TargetData
    {
        public Jewelry targetJewelry;
        public int requiredAmount;
    }
}