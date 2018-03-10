using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class ShopController : MonoBehaviour {

    public Text moneyText;

	void Start ()
    {
        UpdateMoneyText();
	}

    public void UpdateMoneyText()
    {
        moneyText.text = "POINTS: " + PlayerPrefs.GetInt("money", 0).ToString();
    }

    public void NotEnoughMoney()
    {
        StartCoroutine(ChangeTextColor(Color.red));
    }

    public void AlreadyBought()
    {
        StartCoroutine(ChangeTextColor(Color.green));
    }

    IEnumerator ChangeTextColor(Color color)
    {
        moneyText.color = color;
        yield return new WaitForSecondsRealtime(1);
        moneyText.color = Color.white;
    }
}
