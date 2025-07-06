using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HomeScreenView : MonoBehaviour//, IPointerEnterHandler, IPointerExitHandler
{
	public TopBarView topBarView;
	public BottomBarView bottomBarView;
	private Animator animator;

	private void Awake ()
	{
		animator = GetComponent<Animator> ();
	}

	public void ShowBottomBar ()
	{
		animator.SetTrigger ("BottomBarIn");
	}

	public void HideBottomBar ()
	{
		animator.SetTrigger ("BottomBarOut");
	}

	// Start is called before the first frame update
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{

	}


	public void SetCurrencyValue ()
	{
		topBarView.coinCountText.text = topBarView.coinCount.ToString ();
		topBarView.lifeCountText.text = topBarView.lifeCount.ToString ();
		topBarView.starCountText.text = topBarView.starCount.ToString ();
	}
}

