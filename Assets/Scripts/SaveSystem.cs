using UnityEngine;

public class SaveSystem
{
    public int GetPlayerScore()
    {
        return PlayerPrefs.GetInt("PlayerScore", 0);
    }

    public void SetPlayerScore(int newScore)
    {
        PlayerPrefs.SetInt("PlayerScore", newScore);
    }
}