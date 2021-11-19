using System;
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
        public GameObject Building6Rooms;
        public Func<VisualElement> makeItem { get; set; }
        public Action<VisualElement, int> bindItem { get; set; }

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
                    // Creating a bunch of things to add to listview
                    const int itemCount = 1000;
                    var items = new List<string>(itemCount);
                    for (int i = 1; i <= itemCount; i++)
                        items.Add(i.ToString());
                    // copy pasted code from https://docs.unity3d.com/Packages/com.unity.ui@1.0/api/UnityEngine.UIElements.ListView.html
                    Func<VisualElement> makeItem = () => new Label();
                    Action<VisualElement, int> bindItem = (e, i) => (e as Label).text = items[i];
                    var listView = new ListView(items, 16, makeItem, bindItem);
                    listView.selectionType = SelectionType.Multiple;

                    listView.onItemsChosen += objects => Debug.Log(objects);
                    listView.onSelectionChange += objects => Debug.Log(objects);

                    listView.style.flexGrow = 1.0f;
                    // added listview and buttons show but cant find then when inspecting code
                    SearchMenu[1].rootVisualElement.Add(listView);
                    SearchMenu[1].rootVisualElement.Add(button);
                    SearchMenu[1].rootVisualElement.Add(button);
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
