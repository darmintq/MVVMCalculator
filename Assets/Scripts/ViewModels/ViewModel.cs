using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewModel : MonoBehaviour
{
	[SerializeField] private Model Model;

    private string[] Operations = { "/", "*", "+", "-" };

    public void OnButtonClicked(string ButtonId)
    {
        if (Array.Exists(Operations, x => x==ButtonId))
            Model.AddOperation(ButtonId);
        else
        {
            switch (ButtonId)
            {
                case "Clear":
                    Model.Reset();

                    break;
                case "Result":
                    Model.Result();
                    break;
                default:
                    Model.AddValue(ButtonId);
                    break;
            }

        }
	}

    private void OnEnable()
    {
        Model.OnDataChangedEvent += OnDataChanged;
    }

    private void OnDestroy()
    {
        Model.OnDataChangedEvent -= OnDataChanged;
    }

    public delegate void OutputHandler(string newOutputValue);

    public event OutputHandler OnOutputValueChangedEvent;

    private void OnDataChanged(string newFirstValue, string newCurrentOperation, string newSecondValue)
    {
        string NewOutput = newFirstValue + newCurrentOperation + newSecondValue;

        this.OnOutputValueChangedEvent?.Invoke(NewOutput);
    }
}
