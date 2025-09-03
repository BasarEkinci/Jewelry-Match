using System;
using _GameFolders.Scripts.Abstracts;

namespace _GameFolders.Scripts.Data.ValueObjects
{
    [Serializable]
    public struct PowerUpToSpawnData
    {
        public CollectableObject powerUp;
        public int spawnCount;
    }
}