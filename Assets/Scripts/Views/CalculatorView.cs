using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculatorView : MonoBehaviour
{
    public ViewModel ViewModel = new ViewModel();
    private void OnEnable()
    {
        ViewModel.OnEnable();
    }

    private void OnDestroy()
    {
        ViewModel.OnDestroy();
    }

    private void Start()
    {
        ViewModel.InitializeViewModel();
    }
}
