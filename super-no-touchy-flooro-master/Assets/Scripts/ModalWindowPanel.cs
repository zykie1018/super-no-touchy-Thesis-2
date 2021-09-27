using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class ModalWindowPanel : MonoBehaviour
{
    [SerializeField]
    private Transform _ModalWindowBox;

    [Header("Header")]
    [SerializeField]
    private Transform _headerArea;
    [SerializeField]
    private TextMeshProUGUI _titleField;

    [Header("Content")]
    [SerializeField]
    private Transform _contentArea;
    [SerializeField]
    private Transform _horizontalLayoutArea;
    [SerializeField]
    private Transform _iconContainer;
    [SerializeField]
    private Image _iconImage;
    [SerializeField]
    private TextMeshProUGUI _iconText;
    [Space()]
    [SerializeField]
    private Transform _verticalLayoutArea;
    [SerializeField]
    private Image _heroImage;
    [SerializeField]
    private TextMeshProUGUI _heroText;

    [Header("Footer")]
    [SerializeField]
    private Transform _footerArea;
    [SerializeField]
    private Button _confirmButton;
    [SerializeField]
    private Button _declineButton;
    [SerializeField]
    private Button _alternativeButton;

    private Action onConfirmAction;
    private Action onDeclineAction;
    private Action onAlternativeAction;

    public void Confirm()
    {
        onConfirmAction?.Invoke();
        Close();
    }
    public void Decline()
    {
        onDeclineAction?.Invoke();
        Close();
    }
    public void Alternative()
    {
        onAlternativeAction?.Invoke();
        Close();
    }

    private void Close()
    {
        throw new NotImplementedException();
    }

    public void ShowAsHero(string title, Sprite imagetoShow, string message, Action confirmAction, Action declineAction = null, Action alternativeAction = null)
    {
        LeanTween.cancel(_ModalWindowBox.gameObject);

        _horizontalLayoutArea.gameObject.SetActive(false);
        _verticalLayoutArea.gameObject.SetActive(true);

        // Checker to see if title is empty
        // if it is, hide the header
        bool hasTitle = string.IsNullOrEmpty(title);
        _headerArea.gameObject.SetActive(hasTitle);
        _titleField.text = title;

        _heroImage.sprite = imagetoShow;
        _heroText.text = message;

        // onConfirmCallback = confirmAction;
        // onDeclineCallback = declineAction;

        // if additional callbacks are null
        // if true, just hide the buttons
        bool hasAlternative = (alternativeAction != null);
        _alternativeButton.gameObject.SetActive(hasAlternative);
        // onAlternativeCallback = alternativeAction;
    }

}
