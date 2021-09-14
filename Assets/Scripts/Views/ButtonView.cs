using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{
    [SerializeField] private CalculatorView CalculatorView;
    [SerializeField] private string ButtonId;
	[SerializeField] private Button Button;

	private void OnButtonClicked()
	{
        CalculatorView.ViewModel.OnButtonClicked(ButtonId);
	}

    private void OnEnable()
    {
        Button.onClick.AddListener(() => OnButtonClicked());
    }
    private void OnDestroy()
    {
        Button.onClick.RemoveAllListeners();
    }
}
