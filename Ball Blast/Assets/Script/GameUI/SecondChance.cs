// SecondChance
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SecondChance : MonoBehaviour
{
    public Text timmerText;

    private int timer = 3;

    public Button button;

    private void OnEnable()
    {
        timer = 3;
        timmerText.text = timer.ToString();
        button.interactable = true;
        StartCoroutine(Timercount());
        StartCoroutine(DisableUI());
    }

    public void SecondChanceButton()
    {
        Game_Controller.secondChance = false;
        //	if (AdvHelper.Instance.showGift(delegate(int state)
        //	{
        
        //如果广告载入成功并且点击了
        if (true)
        {
            ContinueGame();
        }
        else
        {
        //    MainMenuUI.instance.OpenEndUI();
        }
        //	}) != 0)
        //	{
        MainMenuUI.Instance.OpenEndUI();
        //}
    }

    public void ContinueGame()
    {
        Game_Controller.isPaused = false;
    }

    private IEnumerator Timercount()
    {
        if (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
            timmerText.text = timer.ToString();
            StartCoroutine(Timercount());
            yield break;
        }
        button.interactable = false;
        yield break;
    }

    private IEnumerator DisableUI()
    {
        yield return new WaitForSeconds(3.1f);
        MainMenuUI.Instance.OpenEndUI();
        gameObject.SetActive(false);
        yield break;
    }
}
