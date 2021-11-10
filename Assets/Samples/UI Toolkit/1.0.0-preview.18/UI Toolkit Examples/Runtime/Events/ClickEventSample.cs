using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Mapbox;
using Newtonsoft.Json;
using System.Linq;

    public class ClickEventSample : MonoBehaviour
    {
        private enum GameState
        {
            Waiting,
            Active
        }

        private const string ActiveClassName = "game-button--active";

        [SerializeField] private PanelSettings panelSettings = default;
        [SerializeField] private VisualTreeAsset sourceAsset = default;
        [SerializeField] private StyleSheet styleSheet = default;

        private Button ViewMapButton;
        public UIDocument StartupMenu;
        public List<UIDocument> SearchMenu;
        //public SpawnOnMap spawnOnMap;

        public void SetPanelSettings(PanelSettings newPanelSettings)
        {
            panelSettings = newPanelSettings;
            StartupMenu.panelSettings = panelSettings;
        }

        void Awake()
        {
            StartupMenu.panelSettings = panelSettings;
            StartupMenu.visualTreeAsset = sourceAsset;
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
            StartupMenu.rootVisualElement.style.display = DisplayStyle.None;
            SearchMenu[0].rootVisualElement.style.display = DisplayStyle.Flex;
        }

        void Update()
        {

        }
    }
