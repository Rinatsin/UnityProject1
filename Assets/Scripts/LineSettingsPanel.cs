using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSettingsPanel : MonoBehaviour
{
    [SerializeField] GameObject SettingsPanel;
    private bool _panelIsOpened = false;

    void Update() => OpenClosePanel();

    public void ChangePanelVisibility()
    {
        if (_panelIsOpened)
        {
            _panelIsOpened = false;
        }
        else
        {
            _panelIsOpened = true;
        }
    }

    private void OpenClosePanel()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangePanelVisibility();
            SettingsPanel.SetActive(_panelIsOpened);
        }
    }
}
