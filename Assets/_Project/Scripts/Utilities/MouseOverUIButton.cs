using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOverUIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Play hover sound when the mouse enters the button
        AudioManager.Instance.PlaySound("UI_Hover");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // You can add additional logic for when the mouse exits the button, if needed
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Play click sound when the button is clicked
        AudioManager.Instance.PlaySound("UI_Click");
    }
}
