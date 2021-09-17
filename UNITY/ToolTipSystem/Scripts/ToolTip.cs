using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//[ExecuteInEditMode()]
public class ToolTip : MonoBehaviour
{

    public TextMeshProUGUI headerField;

    public TextMeshProUGUI contentField;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

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

        Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0f, -0.3f);
        //Vector2 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector3 position = Input.mousePosition;

         // envoie le tooltip a perpette
        transform.position = position;
        //Debug.Log(position);
    }

    
}
