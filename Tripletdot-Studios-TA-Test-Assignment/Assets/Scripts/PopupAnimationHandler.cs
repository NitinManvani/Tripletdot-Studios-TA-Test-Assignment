using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupAnimationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }
	public void OnPopupOpen(GameObject popupInstacne)
	{
		UIManager.Instance.PopupOpen (popupInstacne);
	}

	public void OnPopupClose ()
	{
		UIManager.Instance.DismissPopup (this.GetComponentInParent<PopupAnimationHandler>().gameObject);
	}   
}
