using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace Samples.Runtime.Events
{
    public class ClickEventSample : MonoBehaviour
    {
        private enum GameState
        {
            Waiting,
            Active
        }

        private const string ActiveClassName = "game-button--active";

        [SerializeField] private PanelSettings panelSettings = default;
        [SerializeField] private StyleSheet styleSheet = default;

        private Button ViewMapButton;
        public UIDocument StartupMenu;
        public List<UIDocument> SearchMenu;
        public SpawnOnMap spawnOnMap;

        public void SetPanelSettings(PanelSettings newPanelSettings)
        {
            panelSettings = newPanelSettings;
            StartupMenu.panelSettings = panelSettings;
        }

        void Awake()
        {
            StartupMenu.panelSettings = panelSettings;
        }

        void OnEnable()
        {
            InitializeVisualTree(StartupMenu);
        }

        private void InitializeVisualTree(UIDocument doc)
        {
            var root = doc.rootVisualElement;

            ViewMapButton = root.Q<Button>("ViewMapButton");
            Button SearchBuildingbutton = root.Q<Button>("SearchBuildingButton");
            ViewMapButton.clicked += ClickViewMapButton;
            SearchBuildingbutton.clicked += ClickSearchButton;
            root.styleSheets.Add(styleSheet);

            foreach (var menu in SearchMenu)
            {
                menu.rootVisualElement.style.display = DisplayStyle.None;
            }
        }

        private void ClickViewMapButton()
        {
            StartupMenu.rootVisualElement.style.display = DisplayStyle.None;
        }

        private void ClickSearchButton()
        {
            var root = SearchMenu[0].rootVisualElement;
            StartupMenu.rootVisualElement.style.display = DisplayStyle.None;
            root.style.display = DisplayStyle.Flex;
            root.Query<Button>().ForEach((button) =>
            {
                button.clickable.clickedWithEventInfo += ClickBuildingButton;
            });
        }
        private void ClickBuildingButton(EventBase info)
        {
            Button button = info.target as Button;
            switch (button.name)
            {
                case "Building6Button":
                    SearchMenu[0].rootVisualElement.style.display = DisplayStyle.None;
                    SearchMenu[1].rootVisualElement.style.display = DisplayStyle.Flex;
                    break;
                    
                default:
                    break;
            }
            var root = SearchMenu[1].rootVisualElement;
            root.Query<Button>().ForEach((button) =>
            {
                button.clickable.clickedWithEventInfo += ClickStartingRoom;
            });
        }
        private void ClickStartingRoom(EventBase info)
        {
            Button button = info.target as Button;
            spawnOnMap.From = button.name;
            SearchMenu[1].rootVisualElement.style.display = DisplayStyle.None;
            SearchMenu[2].rootVisualElement.style.display = DisplayStyle.Flex;
            var root = SearchMenu[2].rootVisualElement;
            root.Query<Button>().ForEach((button) =>
            {
                button.clickable.clickedWithEventInfo += ClickEndRoom;
            });
        }
        private void ClickEndRoom(EventBase info)
        {
            Button button = info.target as Button;
            spawnOnMap.To = button.name;
            SearchMenu[2].rootVisualElement.style.display = DisplayStyle.None;
        }

        void Update()
        {

        }
    }
}
