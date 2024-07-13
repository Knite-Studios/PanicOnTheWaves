using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using static System.Reflection.BindingFlags;

namespace Editor
{
    /// <summary>
    /// This static class registers the autosave methods when play mode state changes
    /// in the editor.
    /// </summary>
    [InitializeOnLoad]
    internal static class AutoSaveExtension
    {
        /// <summary>
        /// Property for the UnityEditor focus changed event.
        /// </summary>
        private static Action<bool> UnityEditorFocusChanged
        {
            get
            {
                var fieldInfo = typeof(EditorApplication)
                    .GetField("focusChanged", Static | NonPublic);
                return (Action<bool>)fieldInfo!.GetValue(null);
            }
            set
            {
                var fieldInfo = typeof(EditorApplication)
                    .GetField("focusChanged", Static | NonPublic);
                fieldInfo?.SetValue(null, value);
            }
        }

        /// <summary>
        /// Static constructor that gets called when Unity fires up or recompiles the scripts.
        /// (triggered by the InitializeOnLoad attribute above)
        /// </summary>
        static AutoSaveExtension()
        {
            // Normally I'm against defensive programming, and this is probably
            // not necessary. The intent is to make sure we don't accidentally
            // subscribe to the playModeStateChanged event more than once.
            EditorApplication.playModeStateChanged -= OnPlayStateChange;
            EditorApplication.playModeStateChanged += OnPlayStateChange;

            UnityEditorFocusChanged -= _ => Save();
            UnityEditorFocusChanged += _ => Save();
        }

        /// <summary>
        /// This method saves open scenes and other assets when entering playmode.
        /// </summary>
        /// <param name="newState">The enum that specifies how the play mode is changing in the editor.</param>
        private static void OnPlayStateChange(PlayModeStateChange newState)
        {
            // If we're exiting edit mode (entering play mode)
            if (newState != PlayModeStateChange.ExitingEditMode) return;

            Save();
        }

        /// <summary>
        /// Saves the open scenes and any assets.
        /// </summary>
        private static void Save()
        {
            if (EditorApplication.isPlaying) return;

            // Save the open scenes and any assets.
            EditorSceneManager.SaveOpenScenes();
            AssetDatabase.SaveAssets();
        }
    }
}
