using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    public TabGroup tabGroup;
    public Image background;
    public Button closeButton;

    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.onTabSelected(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.onTabEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.onTabExit(this);
    }

    private void Start()
    {
        background = GetComponent<Image>();
        tabGroup.Subscribe(this);
        Button close = closeButton.GetComponent<Button>();
        close.onClick.AddListener(ExitTab);
    }

    public void Select()
    {
        if (onTabSelected != null)
        {
            onTabSelected.Invoke();
        }
    }

    public void Deselect()
    {
        if (onTabDeselected != null)
        {
            onTabDeselected.Invoke();
        }
    }

    public void ExitTab()
    {
        tabGroup.ExitTab();
    }
}
