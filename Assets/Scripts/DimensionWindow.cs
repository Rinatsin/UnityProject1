using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DimensionWindow : MonoBehaviour
{
    [SerializeField] GameObject DimensionsText;
    private bool _textIsVisible = false;

    void Update() => OpenClosePanel();

    public void ChangePanelVisibility()
    {
        if (_textIsVisible)
        {
            _textIsVisible = false;
        }
        else
        {
            _textIsVisible = true;
        }
    }

    private void OpenClosePanel()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangePanelVisibility();
            DimensionsText.SetActive(_textIsVisible);
        }
    }
}
