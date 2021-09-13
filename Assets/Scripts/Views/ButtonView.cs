using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonView : MonoBehaviour
{

	[SerializeField] private ViewModel ViewModel;
	[SerializeField] private string ButtonId;
	[SerializeField] private Button Button;

	private void OnButtonClicked()
	{
		ViewModel.OnButtonClicked(ButtonId);
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
