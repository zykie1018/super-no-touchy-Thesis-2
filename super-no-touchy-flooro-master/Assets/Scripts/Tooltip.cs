using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public LayoutElement layoutElement;
    public int characterWrapLimit;
    
    // Dynamically anchor the tooltip relative to the position of the mouse
    public RectTransform rectTransform;

    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void SetText(string content, string header = "")
    {   
        //Some tooltips dont need a title so i want to be able to control whether or not a header is shown and hide the header if argument is null or empty
        if(string.IsNullOrEmpty(header))
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;
    }
    private void Update()
    {
        FollowCursor();
    }
    
    private void FollowCursor()
    {
        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        //Without using ternary operator. set the layoutElement.enabled to the condition to be true (since if the conditions are true, then it sets to true and the opposite happens too) 
        layoutElement.enabled = (headerLength > characterWrapLimit || contentLength > characterWrapLimit);
        

        //Assign mouse position on vector2
        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height +0.3f;

        rectTransform.pivot = new Vector2(pivotX, pivotY);

        //set position of the tooltip based on the mouse location
        transform.position = position;
    }

}
