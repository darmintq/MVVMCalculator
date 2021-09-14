using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class Model
{
    private const string Empty = "";
    private const string DefaultValue = "0";

    private string FirstValue;
    private string SecondValue;
    private string CurrentOperation;

    public delegate void DataHandler(string newFirstValue, string newCurrentOperation, string newSecondValue);

    public event DataHandler OnDataChangedEvent;

    public void InitializeModel()
    {
        LoadModel();
    }

    private void LoadModel()
    {
        if (PlayerPrefs.HasKey("FirstValue") && PlayerPrefs.HasKey("SecondValue") && PlayerPrefs.HasKey("CurrentOperation"))
        {
            FirstValue = PlayerPrefs.GetString("FirstValue");
            SecondValue = PlayerPrefs.GetString("SecondValue");
            CurrentOperation = PlayerPrefs.GetString("CurrentOperation");
            DataChange();
        }
        else
            Reset();

    }

    private void SaveModel()
    {
        PlayerPrefs.SetString("FirstValue", FirstValue);
        PlayerPrefs.SetString("SecondValue", SecondValue);
        PlayerPrefs.SetString("CurrentOperation", CurrentOperation);
        PlayerPrefs.Save();
    }

    private void DataChange()
    {
        SaveModel();

        this.OnDataChangedEvent?.Invoke(FirstValue, CurrentOperation, SecondValue);
    }

    public void Reset()
    {
        FirstValue = DefaultValue;
        SecondValue = Empty;
        CurrentOperation = Empty;
        DataChange();
    }
        
    public void AddValue(string value)
    {
        if (IsDefaultValue(FirstValue) && IsEmpty(CurrentOperation))
            FirstValue = value;
        else if (IsEmpty(CurrentOperation) && CheckMaxInt(FirstValue + value))
            FirstValue += value;
        
        if (!IsEmpty(CurrentOperation))
            if (IsEmpty(SecondValue) )
                SecondValue = value;
            else if (CheckMaxInt(SecondValue + value))
                SecondValue += value;

        DataChange();
    }

    public bool CheckMaxInt(string s)
    {
        return Convert.ToInt64(s) < int.MaxValue;
    }

    public void AddOperation(string operation)
    {
        if (IsEmpty(CurrentOperation) && !IsEmpty(FirstValue))
        {
            CurrentOperation = operation;
        }
        DataChange();
    }


    public void Result()
    {
        if (!IsEmpty(SecondValue))
        {
            try
            {
                long Part1 = Convert.ToInt32(FirstValue);
                long Part2 = Convert.ToInt32(SecondValue);
                long result = 0;
                switch (CurrentOperation)
                {
                    case "+":
                        result = Part1 + Part2;
                        break;
                    case "-":
                        result = Part1 - Part2;
                        break;
                    case "*":
                        result = Part1 * Part2;
                        break;
                    case "/":
                        result = Part1 / Part2;
                        break;
                    default:
                        result = Part1;
                        break;
                }
                FirstValue = result.ToString();
                SecondValue = Empty;
                CurrentOperation = Empty;
                DataChange();
            }
            catch(OverflowException e)
            {
                ResetWithMessage("too big number");
            }
            catch(DivideByZeroException e)
            {
                ResetWithMessage("Divide By Zero");
            }
        }
    }

    private void ResetWithMessage(string message)
    {
        Reset();
        this.OnDataChangedEvent?.Invoke(message, "", "");
    }


    private bool IsEmpty(string value)
    {
        return value == Empty;
    }

    private bool IsDefaultValue(string value)
    {
        return value == DefaultValue;
    }
}
