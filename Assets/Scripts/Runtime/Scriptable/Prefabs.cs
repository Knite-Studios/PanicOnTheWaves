﻿using System;
using System.Collections.Generic;
using Common.Attributes;
using JetBrains.Annotations;
using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(menuName = "POTW/Prefabs", fileName = "Prefab List")]
    public class Prefabs : ScriptableObject
    {
        public List<Prefab> prefabs = new();
    }
}

[Serializable]
public struct Prefab
{
    [TitleHeader("Prefab Info")]
    public PrefabType type;
    public GameObject prefab;

    [TitleHeader("Spawning Info")]
    [CanBeNull] public string root;

    [TitleHeader("Pooling Settings")]
    public bool shouldPool;
    public int initialPoolSize;
}

/// <summary>
/// Prefab types to be instantiated.
/// </summary>
public enum PrefabType
{
    None = 0,
    Hype = 1,
    HypeTrigger = 2,
    Tower = 3,
    BasicEnemy = 4,
    ConeheadZombie = 5,
    RushEnemy = 6,
    SneakyGhost = 7,
    TankEnemy = 8,
    Projectile = 9,
}
