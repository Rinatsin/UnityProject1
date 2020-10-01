using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DimensionsViewer : MonoBehaviour
{
    private string _startPoint;
    private string _endPoint;
    private string _distance;
    private string _previousDimensions = "";
    private Text _dimensionText;

    void Start()
    {
        _dimensionText = GetComponent<Text>();
        _previousDimensions = _dimensionText.text;
        Dimensions.ResetDimensions();
    }

    void Update()
    {
        if (_dimensionText.isActiveAndEnabled)
        {
            _startPoint = Dimensions.LoadDimension("StartPoint");
            _endPoint = Dimensions.LoadDimension("EndPoint");
            _distance = Dimensions.LoadDimension("Distance");
            _dimensionText.text = $"{_previousDimensions}\nStartPoint: {_startPoint}, EndPoint: {_endPoint}, Distance: {_distance} \n"; ;
        }
    }
}
