using UnityEngine;
using UnityEngine.UI;

public class OneTimeButton : MonoBehaviour
{

    Button btn;

    void Start()
    {
        btn = GetComponent<Button>();
    }

    public void OnClick()
    {
        btn.interactable = false;
    }
}