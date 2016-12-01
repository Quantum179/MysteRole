using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ListScrollable : MonoBehaviour{
	private RectTransform scrollRectTransform;
	private RectTransform contentPanel;
	private RectTransform selectedRectTransform;
	public GameObject lastSelected;

	void Start(){
		scrollRectTransform = GetComponent<RectTransform> ();
		contentPanel = GetComponent<ScrollRect> ().content;
	}

	void Update (){
		// Get the currently selected UI element from the event system.
		GameObject selected = EventSystem.current.currentSelectedGameObject;

		if (selected == null) {
			return;
		}
		if (selected.transform.parent != contentPanel.transform) {
			return;
		}
		if (selected == lastSelected) {

		}
		selectedRectTransform = selected.GetComponent<RectTransform> ();
		float selectedPositionY = Mathf.Abs (selectedRectTransform.anchoredPosition.y) + selectedRectTransform.rect.height;
		float scrollViewMinY = contentPanel.anchoredPosition.y;
		float scrollViewMaxY = contentPanel.anchoredPosition.y + scrollRectTransform.rect.height;
		if (selectedPositionY > scrollViewMaxY) {
			float newY = selectedPositionY - scrollRectTransform.rect.height;
			contentPanel.anchoredPosition = new Vector2 (contentPanel.anchoredPosition.x, newY);
		} else if (Mathf.Abs (selectedRectTransform.anchoredPosition.y) < scrollViewMinY) {
			contentPanel.anchoredPosition = new Vector2 (contentPanel.anchoredPosition.x, Mathf.Abs (selectedRectTransform.anchoredPosition.y));
		}
		lastSelected = selected;
	}
}