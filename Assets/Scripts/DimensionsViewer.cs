using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DimensionsViewer : MonoBehaviour
{
    private int numberOfDimension;
    private Text _dimensionText;

    void Start()
    {
        _dimensionText = GetComponent<Text>();
        Dimensions.ResetDimensions();
    }

    void Update()
    {
        if (_dimensionText.isActiveAndEnabled)
        {
            numberOfDimension = Dimensions.Counter;
            _dimensionText.text = Dimensions.LoadDimensionsList(numberOfDimension);
            if (Input.GetKeyDown(KeyCode.C))
            {
                ClearUIDimensionsList();
            }
        }
    }

    private void ClearUIDimensionsList()
    {
        Dimensions.ResetDimensions();
        _dimensionText.text = "";
    }
}
