﻿using System;
using Managers;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor
{
#if UNITY_EDITOR
    /// <summary>
    /// Static class that loads the game manager into the scene.
    /// </summary>
    [InitializeOnLoad]
    static class GameManagerLoader
    {
        public static Action OnCreation;

        /// <summary>
        /// Static constructor which is called when the class is loaded.
        /// </summary>
        static GameManagerLoader()
        {
            EditorApplication.playModeStateChanged -= InitializeGameManager;
            EditorApplication.playModeStateChanged += InitializeGameManager;
        }

        /// <summary>
        /// Invoked when the play mode state is changed in the editor.
        /// </summary>
        private static void InitializeGameManager(PlayModeStateChange evt)
        {
            if (evt != PlayModeStateChange.EnteredPlayMode) return;

            // Check if a GameManager already exists in the scene.
            if (Object.FindObjectOfType<GameManager>() != null) return;

            // Call creation event.
            OnCreation?.Invoke();

            // Add the game manager to the scene.
            var prefab = Resources.Load<GameObject>("Prefabs/Managers/GameManager");
            if (prefab == null) throw new Exception("Missing GameManager prefab!");

            var instance = Object.Instantiate(prefab);
            if (instance == null) throw new Exception("Failed to instantiate GameManager prefab!");

            instance.name = "Managers.GameManager (MonoSingleton)";
        }
    }
#endif
}