using Common.Attributes;
using JetBrains.Annotations;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UI
{
    /// <summary>
    /// Base class for all menus.
    /// </summary>
    public abstract class Menu : MonoBehaviour
    {
        [TitleHeader("Menu Settings")]
        [CanBeNull] public GameObject firstSelected;
        
        private GameObject _lastSelected;
        
        /// <summary>
        /// Checks if the menu is open.
        /// </summary>
        public bool IsOpen => MenuManager.Instance.IsMenuOpen(this);

        protected virtual void OnEnable()
        {
            // Subscribe to the menu input.
            InputManager.Menu.started += OnMenuInput;

            // Keep track of the last selected game object.
            _lastSelected = EventSystem.current.currentSelectedGameObject;
            // Set the first selected game object if it's not null.
            if (firstSelected != null) EventSystem.current.SetSelectedGameObject(firstSelected);
        }

        protected virtual void OnDisable()
        {
            // Unsubscribe from the menu input.
            InputManager.Menu.started -= OnMenuInput;

            // Set the last selected game object if it's not null.
            if (_lastSelected != null) EventSystem.current.SetSelectedGameObject(_lastSelected);
            // Clear the last selected game object.
            _lastSelected = null;
        }

        public void Enable() => gameObject.SetActive(true);
        
        public void Disable() => gameObject.SetActive(false);
        
        /// <summary>
        /// Requests the MenuManager to open the menu.
        /// </summary>
        public void OpenMenu() => MenuManager.Instance.OpenMenu(this);
        
        /// <summary>
        /// Requests the MenuManager to close the menu.
        /// </summary>
        public void CloseMenu() => MenuManager.Instance.CloseMenu();
        
        /// <summary>
        /// Requests the MenuManager to close the menu and open another menu.
        /// </summary>
        private void OnMenuInput(InputAction.CallbackContext context)
        {
            if (IsOpen) CloseMenu();
        }
    }
}