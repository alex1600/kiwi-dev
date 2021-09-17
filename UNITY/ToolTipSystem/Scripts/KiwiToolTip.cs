using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KiwiToolTip : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI headerField;

    [SerializeField] private TextMeshProUGUI contentField;

    [SerializeField] private LayoutElement layoutElement;

    private KiwiToolTipSystem kttSystem;

    private Vector2 pos;
    private int characterWrapLimit;

    private void Awake()
    {
        kttSystem = GetComponentInParent<KiwiToolTipSystem>();
        characterWrapLimit = kttSystem.characterWrapLimit;
    }

    public void SetText(string content, string header = "")
    {
        if (string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;


        int headerLenght = headerField.text.Length;
        int contentLenght = contentField.text.Length;

        layoutElement.enabled = (headerLenght > characterWrapLimit || contentLenght > characterWrapLimit) ? true : false;
    }

    private void Update()
    {
        if (Application.isEditor)
        {
            int headerLenght = headerField.text.Length;
            int contentLenght = contentField.text.Length;

            layoutElement.enabled = (headerLenght > characterWrapLimit || contentLenght > characterWrapLimit) ? true : false;
        }

        if (kttSystem.orientation == KiwiToolTipSystem.Orientation.Top)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f, kttSystem.distanceToPointer);
        }
        else if (kttSystem.orientation == KiwiToolTipSystem.Orientation.Down)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f, - kttSystem.distanceToPointer);
        }
        else if (kttSystem.orientation == KiwiToolTipSystem.Orientation.Left)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(-kttSystem.distanceToPointer * 2, 0f);
        }
        else if (kttSystem.orientation == KiwiToolTipSystem.Orientation.Right)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(kttSystem.distanceToPointer * 2, 0f);
        }

        transform.position = pos;
    }
}
