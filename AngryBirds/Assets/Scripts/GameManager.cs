using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int MaxNumberOfShots = 3;
    [SerializeField] private float secondsUntilDeathCheck = 2.5f;
    [SerializeField] private GameObject restartScreenObject;

    private int usedNumberOfShots;

    private IconHandler iconHandler;

    private List<Piggy> piggyList = new List<Piggy>();

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        iconHandler = FindObjectOfType<IconHandler>();

        Piggy[] piggies = FindObjectsOfType<Piggy>();
        for (int i = 0; i < piggies.Length; i++)
        {
            piggyList.Add(piggies[i]);
        }
    }

    public void UseShot()
    {
        usedNumberOfShots++;
        iconHandler.UseShot(usedNumberOfShots);

        CheckForLastShot();
    }

    public bool HasEnoughShots()
    {
        if (usedNumberOfShots < MaxNumberOfShots)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CheckForLastShot()
    {
        if (usedNumberOfShots == MaxNumberOfShots)
        {
            StartCoroutine(CheckAfterWaitTime());
        }
    }

    private IEnumerator CheckAfterWaitTime()
    {
        yield return new WaitForSeconds(secondsUntilDeathCheck);

        //check if all piggies have been killed
        if (piggyList.Count == 0)
        {
            //win
            WinGame();
        }
        else
        {
            //lose
            LoseGame();
        }
    }

    public void RemovePiggy(Piggy piggy)
    {
        piggyList.Remove(piggy);

        CheckForAllDeadPiggies();
    }

    private void CheckForAllDeadPiggies()
    {
        if(piggyList.Count == 0)
        {
            //win
            WinGame();
        }
    }

    #region Win/Lose Game
    private void WinGame()
    {
        restartScreenObject.SetActive(true);
    }

    private void LoseGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion
}
