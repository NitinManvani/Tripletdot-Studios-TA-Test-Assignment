using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;
	public HomeScreenView homeScreenView;
	public GameObject popupMessageParentGO;
	public GameObject popupMessagePrefab;
	public GameObject settingPrefab;
	public GameObject infoPrefab;
	public Transform targetTransform;
	public bool isOnHomeScreen = false;
	public float bottomBarTriggerHeight = 200f;

	private void Awake ()
	{
		if (Instance == null)
		{ Instance = this; }
	}

	private void Start ()
	{
		homeScreenView.SetCurrencyValue ();
	}

	private void Update ()
	{
		//Debug.Log ("Mouse Position: " + Input.mousePosition.y);
		if (Input.mousePosition.y < bottomBarTriggerHeight && isOnHomeScreen && homeScreenView != null)
		{
			Debug.Log ("----IF----");
			homeScreenView.ShowBottomBar ();
		}
		else
		{
			Debug.Log ("----ELSE----");
			homeScreenView.HideBottomBar ();
		}
	}

	public void PopupOpen (GameObject popupInstance)
	{
		isOnHomeScreen = false;
		Instantiate (popupInstance, targetTransform);
	}

	public void GeneratePopupMessage ()
	{
		GameObject gameObject = Instantiate (popupMessagePrefab, popupMessageParentGO.transform);
	}

	public void DismissPopup (GameObject popupInstance)
	{
		Animator animator = popupInstance.GetComponent<Animator> ();
		animator.SetTrigger ("Dismiss");
		StartCoroutine (DestroyCoroutine (popupInstance));
	}

	private IEnumerator DestroyCoroutine (GameObject popupInstance)
	{
		isOnHomeScreen = true;
		yield return new WaitForSeconds (1f);
		Destroy (popupInstance);
	}

	public void AnimateCount (TextMeshProUGUI targetText, int start, int end, float duration)
	{
		StartCoroutine (CountRoutine ());

		IEnumerator CountRoutine ()
		{
			float elapsed = 0;
			while (elapsed < duration)
			{
				elapsed += Time.deltaTime;
				int val = Mathf.RoundToInt (Mathf.Lerp (start, end, elapsed / duration));
				targetText.text = val.ToString ();
				yield return null;
			}
			targetText.text = end.ToString ();
		}
	}

}