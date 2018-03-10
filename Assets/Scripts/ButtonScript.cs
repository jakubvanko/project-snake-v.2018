using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour {

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void ResumeGame()
    {
        GameObject.Find("Game Manager").GetComponent<GameController>().ResumeGame();
    }

    public void BuyUpgrade(string data)
    {
        string[] input = data.Split(' ');
        //UpgradeNumbers
        //1 - BirdsEye 2 - SpeedManager 3 - ProjectileShooter  
        string upgradeNumber = input[0];
        int price = int.Parse(input[1]);
        if (PlayerPrefs.GetInt("upgrade" + upgradeNumber, 0) == 1)
        {
            GameObject.Find("ShopManager").GetComponent<ShopController>().AlreadyBought();
            return;
        }
        if (price <= PlayerPrefs.GetInt("money", 0))
        {
            PlayerPrefs.SetInt("upgrade" + upgradeNumber, 1);
            PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money", 0) - price);
            GameObject.Find("ShopManager").GetComponent<ShopController>().UpdateMoneyText();
        }
        else
        {
            GameObject.Find("ShopManager").GetComponent<ShopController>().NotEnoughMoney();
        }
    }


}
