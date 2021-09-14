using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputView : MonoBehaviour
{
    [SerializeField] private Text output;
    [SerializeField] private CalculatorView CalculatorView;

    private void OnEnable()
    {
        CalculatorView.ViewModel.OnOutputValueChangedEvent += OnOutputValueChanged;
    }

    private void OnDestroy()
    {
        CalculatorView.ViewModel.OnOutputValueChangedEvent -= OnOutputValueChanged;
    }

    private void OnOutputValueChanged(string newOutputValue)
    {
        output.text = newOutputValue;
    }

}
