using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputView : MonoBehaviour
{
    [SerializeField] private Text output;
    [SerializeField] private ViewModel ViewModel;


    private void OnEnable()
    {
        ViewModel.OnOutputValueChangedEvent += OnOutputValueChanged;
    }

    private void OnDestroy()
    {
        ViewModel.OnOutputValueChangedEvent -= OnOutputValueChanged;
    }

    private void OnOutputValueChanged(string newOutputValue)
    {
        output.text = newOutputValue;
    }

}
