using UnityEngine;
using UnityEngine.EventSystems;

// Button not unselecting fix
public class ButtonDeselect : MonoBehaviour, IPointerUpHandler
{
    // unselects objects
    public void OnPointerUp(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
