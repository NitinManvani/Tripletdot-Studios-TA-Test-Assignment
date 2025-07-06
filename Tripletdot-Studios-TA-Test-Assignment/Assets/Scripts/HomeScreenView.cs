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
	[SerializeField] private string bottomBarShowTriggger = "BottomBarIn";
	[SerializeField] private string bottomBarHideTrigger = "BottomBarOut";

	private void Awake ()
	{
		animator = GetComponent<Animator> ();
	}

	public void ShowBottomBar ()
	{
		animator.ResetTrigger (bottomBarShowTriggger);
		animator.ResetTrigger (bottomBarHideTrigger);
		animator.SetTrigger (bottomBarShowTriggger);
	}

	public void HideBottomBar ()
	{
		animator.ResetTrigger (bottomBarHideTrigger);
		animator.ResetTrigger (bottomBarShowTriggger);
		animator.SetTrigger (bottomBarHideTrigger);
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

