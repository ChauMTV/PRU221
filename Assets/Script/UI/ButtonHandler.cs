using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Button handler.
/// </summary>
public class ButtonHandler : MonoBehaviour
{
	public AudioClip audioClip;

	/// <summary>
	/// Buttons pressed.
	/// </summary>
	/// <param name="buttonName">Button name.</param>
	public void ButtonPressed(string buttonName)
	{
		StartCoroutine(PressedCoroutine(buttonName));
	}

	/// <summary>
	/// Presseds the coroutine.
	/// </summary>
	/// <returns>The coroutine.</returns>
	/// <param name="buttonName">Button name.</param>
	private IEnumerator PressedCoroutine(string buttonName)
	{
		
			Button button = GetComponent<Button>();
			button.interactable = false;
			// Wayt for sound effect end
			yield return new WaitForSecondsRealtime(1);
			button.interactable = true;
		
		// Send global event about button preesing
		EventManager.TriggerEvent("ButtonPressed", gameObject, buttonName);
	}

	/// <summary>
	/// Raises the destroy event.
	/// </summary>
	void OnDestroy()
	{
		StopAllCoroutines();
	}
}
