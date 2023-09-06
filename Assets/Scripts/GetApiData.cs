using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetApiData : MonoBehaviour
{
    public string url;
    public InputField id;
    public GameObject playerStatsPanel;
    
    public void GetData()
    {
        StartCoroutine(FetchData());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public IEnumerator FetchData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url + id.text))
        {
            yield return request.SendWebRequest();
            
            if(request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(request.error);
            }
            else
            {
                PlayerStats playerStats = new PlayerStats();
                playerStats = JsonUtility.FromJson<PlayerStats>(request.downloadHandler.text);

                playerStatsPanel.transform.GetChild(0).GetComponent<Text>().text = playerStats.playerName;
                playerStatsPanel.transform.GetChild(1).GetComponent<Text>().text = "Attack: " +  playerStats.attack.ToString();
                playerStatsPanel.transform.GetChild(2).GetComponent<Text>().text = "Defend: " +  playerStats.defend.ToString();
                playerStatsPanel.transform.GetChild(3).GetComponent<Text>().text = "HP: " +  playerStats.hp.ToString();
                playerStatsPanel.transform.GetChild(4).GetComponent<Text>().text = "MP: " +  playerStats.mp.ToString();
            }
        }
    }
}
