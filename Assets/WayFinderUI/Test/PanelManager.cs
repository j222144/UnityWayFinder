using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UIElements;
using UnityEngine.Assertions;

public class PanelManager : MonoBehaviour
{
    public UIDocument StartupMenu;
    public List<UIDocument> SearchMenu;
    Button viewMapButton;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var item in SearchMenu)
        {
            item.rootVisualElement.style.display = DisplayStyle.None;
            item.enabled = false;
        }
        var allElementsInHirarchy = new List<UnityEngine.UIElements.VisualElement>();
        getAllContainedElements(StartupMenu.rootVisualElement, ref allElementsInHirarchy);

        foreach (var element in allElementsInHirarchy)
        {
            if (element.name == ".button")
            {
                viewMapButton = (Button)element;
            }
        }

        viewMapButton.clicked += () =>
        {
            Debug.Log("ViewMapClicked");
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void getAllContainedElements(VisualElement parentElement, ref List<VisualElement> allElements)
    {
        allElements.Add(parentElement);

        for (int i = 0; i < parentElement.childCount; i++)
        {
            getAllContainedElements(parentElement[i], ref allElements);
        }
    }

    void SetEnabledState(UIDocument document, bool enable)
    {
        if (enable)
        {
            document.rootVisualElement.style.display = DisplayStyle.Flex;
        }
        else
        {
            document.rootVisualElement.style.display = DisplayStyle.None;
        }
        document.enabled = enable;
    }
}
