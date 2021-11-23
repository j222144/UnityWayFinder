using UnityEngine;
using UnityEngine.UIElements;

public class testScript : MonoBehaviour
{

    // Start is called before the first frame update
    void OnEnable()
    {
        var startMenu = GetComponent<UIDocument>().rootVisualElement;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
