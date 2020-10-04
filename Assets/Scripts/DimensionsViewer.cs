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
        }
    }
}
