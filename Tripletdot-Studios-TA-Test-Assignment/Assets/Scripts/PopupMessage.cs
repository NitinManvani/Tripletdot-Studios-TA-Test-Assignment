using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopupMessage : MonoBehaviour
{
	public string infoMessage = "No Info Available!";
	public TextMeshProUGUI infoText;

	private void Start ()
	{
		SetInfo (infoMessage);
	}

	public void SetInfo (string infoMessage)
	{
		if (infoText != null)
		{
			infoText.text = infoMessage;
		}

		else if (infoMessage.Length == 0)
		{
			infoMessage = "No Info Available!";
			infoText.text = infoMessage;
		}

		else
		{
			Debug.LogWarning ("InfoText is not assigned in the InfoPopup script.");
		}
	}
	
	public void DestroyPopupMessage ()
	{
		Destroy (this.gameObject, 5f);
	}
}
