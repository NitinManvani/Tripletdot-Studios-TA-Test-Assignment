using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BottomBarView : MonoBehaviour
{
	[System.Serializable]
	public class BottomBarButton
	{
		public Button button;
		public Image iconImage;
		public bool isLocked = false;
		public string iconName;
		public Vector2 iconOffset = Vector2.zero;
		public bool isDefaultSelected = false;
	}

	public BottomBarButton[] buttons;
	public Sprite lockSprite;
	public Sprite[] unlockedIcons;

	public RectTransform selectionBG;
	public TextMeshProUGUI iconNameText;

	public float animationDuration = 0.3f;
	public Vector3 selectedScale = new Vector3 (1.2f, 1.2f, 1);
	public Vector3 unselectedScale = Vector3.one;

	private int currentButtonIndex = -1;
	private Coroutine selectionCoroutine;

	void Start ()
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			int index = i;
			buttons[i].button.onClick.AddListener (() => OnButtonClicked (index));
			UpdateButtonVisual (index);
			ResetIconOffset (index);
		}

		if (selectionBG != null)
			selectionBG.gameObject.SetActive (false);

		if (iconNameText != null)
			iconNameText.text = "";

		SelectDefaultOrFirstUnlockedButton ();
	}

	void SelectDefaultOrFirstUnlockedButton ()
	{
		int defaultIndex = -1;

		for (int i = 0; i < buttons.Length; i++)
		{
			if (buttons[i].isDefaultSelected && !buttons[i].isLocked)
			{
				defaultIndex = i;
				break;
			}
		}

		if (defaultIndex == -1)
		{
			for (int i = 0; i < buttons.Length; i++)
			{
				if (!buttons[i].isLocked)
				{
					defaultIndex = i;
					break;
				}
			}
		}

		if (defaultIndex != -1)
		{
			currentButtonIndex = defaultIndex;

			if (selectionCoroutine != null)
				StopCoroutine (selectionCoroutine);

			selectionCoroutine = StartCoroutine (AnimateSelection (-1, defaultIndex));
		}
	}

	void OnButtonClicked (int index)
	{
		var btn = buttons[index];

		if (btn.isLocked)
		{
			UIManager.Instance.GeneratePopupMessage ();
			return;
		}

		if (index == currentButtonIndex)
			return;

		int previousIndex = currentButtonIndex;
		currentButtonIndex = index;

		if (selectionCoroutine != null)
			StopCoroutine (selectionCoroutine);

		selectionCoroutine = StartCoroutine (AnimateSelection (previousIndex, currentButtonIndex));
	}

	IEnumerator AnimateSelection (int fromIndex, int toIndex)
	{
		if (selectionBG != null)
		{
			selectionBG.gameObject.SetActive (true);

			RectTransform fromRect = (fromIndex >= 0)
				? buttons[fromIndex].button.GetComponent<RectTransform> ()
				: null;

			RectTransform toRect = buttons[toIndex].button.GetComponent<RectTransform> ();

			Vector3 startPos = fromRect != null ? fromRect.position : toRect.position;
			Vector2 startSize = fromRect != null ? fromRect.sizeDelta : toRect.sizeDelta;

			Vector3 endPos = toRect.position;
			Vector2 endSize = toRect.sizeDelta;

			if (fromIndex == -1)
			{
				selectionBG.position = endPos;
				selectionBG.sizeDelta = endSize;
			}
			else
			{
				float elapsed = 0f;

				while (elapsed < animationDuration)
				{
					float t = elapsed / animationDuration;

					selectionBG.position = Vector3.Lerp (startPos, endPos, t);
					selectionBG.sizeDelta = Vector2.Lerp (startSize, endSize, t);

					elapsed += Time.deltaTime;
					yield return null;
				}

				selectionBG.position = endPos;
				selectionBG.sizeDelta = endSize;
			}
		}

		if (iconNameText != null)
			iconNameText.text = buttons[toIndex].iconName;

		AnimateButtonScales ();
		UpdateButtonOffsets ();
	}

	void AnimateButtonScales ()
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			var targetScale = (i == currentButtonIndex) ? selectedScale : unselectedScale;
			StartCoroutine (ScaleButton (buttons[i].button.transform, targetScale, animationDuration));
		}
	}

	IEnumerator ScaleButton (Transform target, Vector3 targetScale, float duration)
	{
		Vector3 initialScale = target.localScale;
		float elapsed = 0f;

		while (elapsed < duration)
		{
			float t = elapsed / duration;
			target.localScale = Vector3.Lerp (initialScale, targetScale, t);
			elapsed += Time.deltaTime;
			yield return null;
		}

		target.localScale = targetScale;
	}

	void UpdateButtonVisual (int index)
	{
		var btn = buttons[index];

		if (btn.isLocked)
			btn.iconImage.sprite = lockSprite;
		else
			btn.iconImage.sprite = unlockedIcons[index];
	}

	void UpdateButtonOffsets ()
	{
		for (int i = 0; i < buttons.Length; i++)
		{
			if (buttons[i].iconImage != null)
			{
				RectTransform iconRect = buttons[i].iconImage.GetComponent<RectTransform> ();
				if (iconRect != null)
				{
					if (i == currentButtonIndex)
						iconRect.anchoredPosition = buttons[i].iconOffset;
					else
						iconRect.anchoredPosition = Vector2.zero;
				}
			}
		}
	}

	void ResetIconOffset (int index)
	{
		var btn = buttons[index];
		if (btn.iconImage != null)
		{
			RectTransform iconRect = btn.iconImage.GetComponent<RectTransform> ();
			if (iconRect != null)
				iconRect.anchoredPosition = Vector2.zero;
		}
	}
}
