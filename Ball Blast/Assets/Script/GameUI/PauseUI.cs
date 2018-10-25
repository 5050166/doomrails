// PauseUI
using UnityEngine;

public class PauseUI : MonoBehaviour
{
	public void ResumeButton()
	{
		Game_Controller.isPaused = false;
	}
}
