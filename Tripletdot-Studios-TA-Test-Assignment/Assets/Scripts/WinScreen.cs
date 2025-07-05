using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    public void OnCollect (GameObject popupInstance)
    {
        UIManager.Instance.PopupOpen (popupInstance);
    }

    public void DestroyWinScreen()
	{
        Destroy (this.gameObject,.5f);
	}
}
