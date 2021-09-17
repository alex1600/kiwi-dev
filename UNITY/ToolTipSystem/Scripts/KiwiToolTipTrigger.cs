using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KiwiToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string header;

    [Multiline()]
    public string content;

    public void OnPointerEnter(PointerEventData eventData)
    {
        KiwiToolTipSystem.Show(content, header);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        KiwiToolTipSystem.Hide();
    }
}
