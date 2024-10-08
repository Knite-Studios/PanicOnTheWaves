﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Attributes;
using Extensions;
using Interfaces;
using Scriptable;
using UnityEngine;

namespace Managers
{
    [InitializeSingleton]
    public class PrefabManager : MonoSingleton<PrefabManager>
    {
        [SerializeField] private List<Prefabs> allPrefabs;

        private readonly Dictionary<PrefabType, Prefab> _prefabs = new();
        private readonly Dictionary<PrefabType, Queue<GameObject>> _pools = new();

        /// <summary>
        /// Static shortcut method for creating a prefab.
        /// </summary>
        /// <param name="prefab">The type of prefab to create.</param>
        /// <param name="position">The position where the new object should be created.</param>
        /// <param name="parent">The parent transform.</param>
        /// <param name="active">The active state of the prefab.</param>
        public static GameObject Create(PrefabType prefab, Vector3 position = default, Transform parent = null, bool active = true)
            => position == default ?
                Instance.Instantiate(prefab, parent, active) :
                Instance.Instantiate(prefab, position, parent, active);

        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// Overload for creating a prefab and returning a component.
        /// </summary>
        /// <param name="prefab">The type of prefab to create.</param>
        /// <param name="position">The position where the new object should be created.</param>
        /// <param name="parent">The parent transform.</param>
        /// <param name="active">The active state of the prefab.</param>
        /// <typeparam name="T">The type of component to return.</typeparam>
        /// <returns>The component of the prefab.</returns>
        public static T Create<T>(PrefabType prefab, Vector3 position = default, 
            Transform parent = null, bool active = true) where T : Component
        {
            var newObject = position == default ?
                Instance.Instantiate(prefab, parent, active) :
                Instance.Instantiate(prefab, position, parent, active);
            var component = newObject.GetComponent<T>();
            if (component == null)
                Debug.LogError($"Prefab {prefab} does not have component {typeof(T)}");

            return component;
        }

        /// <summary>
        /// Special singleton initializer method.
        /// </summary>
        public new static void Initialize()
        {
            var prefab = Resources.Load<GameObject>("Prefabs/Managers/PrefabManager");
            if (prefab == null) throw new Exception("Missing PrefabManager prefab!");

            var instance = Instantiate(prefab);
            if (instance == null) throw new Exception("Failed to instantiate PrefabManager prefab!");

            instance.name = "Managers.PrefabManager (MonoSingleton)";
        }

        protected override void Awake()
        {
            DontDestroyOnLoad(this);

            foreach (var prefab in allPrefabs.SelectMany(prefabList => prefabList.prefabs))
            {
                _prefabs.Add(prefab.type, prefab);

                // If the prefab should be pooled, create a pool for it.
                if (!prefab.shouldPool) continue;
                var root = GameObject.Find(prefab.root);

                _pools.Add(prefab.type, new Queue<GameObject>());
                for (var i = 0; i < prefab.initialPoolSize; i++)
                {
                    var newObject = Instantiate(prefab.prefab, root ? root.transform : transform);
                    newObject.SetActive(false);
                    _pools[prefab.type].Enqueue(newObject);
                }
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        /// <summary>
        /// Creates a new instance of the prefab.
        /// </summary>
        /// <param name="prefab">The prefab type</param>
        /// <param name="parent">The parent transform.</param>
        /// <param name="active">The active state.</param>
        /// <returns>The created object.</returns>
        private GameObject Instantiate(PrefabType prefab, Transform parent, bool active)
        {
            var prefabData = _prefabs[prefab];

            GameObject newObject;
            if (prefabData.shouldPool)
            {
                var pool = _pools[prefab];

                if (pool.Count > 0 && !pool.Peek().activeSelf)
                {
                    // Use the object from the pool.
                    newObject = pool.Dequeue();
                    newObject.SetActive(true);

                    // Reset the transform.
                    newObject.transform.SetParent(parent, false);
                    newObject.transform.Reset(true, true);

                    // Call reset.
                    var poolObject = newObject.GetComponent<IPoolObject>();
                    poolObject?.Reset();
                }
                else
                {
                    // Create a new object.
                    newObject = Instantiate(prefabData.prefab, parent);
                }

                // Re-add the object to the pool.
                pool.Enqueue(newObject);
            }
            else
            {
                newObject = Instantiate(prefabData.prefab, parent);
            }

            newObject.SetActive(active);

            if (prefabData.root != null)
            {
                var root = GameObject.Find(prefabData.root);
                if (root)
                {
                    newObject.transform.SetParent(root.transform, false);
                }
            }

            return newObject;
        }

        private GameObject Instantiate(PrefabType prefab, Vector3 position, Transform parent, bool active)
        {
            var newObject = Instantiate(prefab, parent, active);
            newObject.transform.position = position;
            return newObject;
        }

        /// <summary>
        /// Returns an object to its pool, or destroys it.
        /// </summary>
        /// <param name="prefabType">The type of prefab.</param>
        /// <param name="obj">The object to return.</param>
        public static void Destroy(PrefabType prefabType, GameObject obj)
        {
            if (!Instance._pools.ContainsKey(prefabType))
            {
                Destroy(obj);
                return;
            }

            obj.SetActive(false);
            Instance._pools[prefabType].Enqueue(obj);

            // Return the object to its pool.
            var prefab = Instance._prefabs[prefabType];
            obj.transform.SetParent(prefab.shouldPool ?
                GameObject.Find(prefab.root).transform :
                null);
        }
    }
}
