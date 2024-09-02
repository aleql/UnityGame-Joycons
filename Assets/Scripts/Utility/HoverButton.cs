using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button button;
    private int _pointersCount;
    void Start()
    {
        _pointersCount = 0;
        button = GetComponent<Button>();
    }
    /*
    // Method to simulate pointer enter (hover)
    public void SimulatePointerEnter()
    {
        if (button.interactable)
        {
            // Optionally, add your custom hover effect logic here
            ExecuteEvents.Execute<IPointerEnterHandler>(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerEnterHandler);
        }
    }

    // Method to simulate pointer exit
    public void SimulatePointerExit()
    {
        if (button.interactable)
        {
            // Optionally, add your custom exit effect logic here
            ExecuteEvents.Execute<IPointerExitHandler>(gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerExitHandler);
        }
    }
    */

    // Implementing hover effects
    public void OnPointerEnter(PointerEventData eventData)
    {
        button.OnPointerEnter(null);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        button.OnPointerExit(null);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pointer")
        {
            button.OnPointerEnter(null);
            _pointersCount += 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Pointer")
        {
            _pointersCount -= 1;
            if (_pointersCount <= 0)
            {
                button.OnPointerExit(null);
            }
        }
    }
}