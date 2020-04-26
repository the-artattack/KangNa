using UnityEngine;
using UnityEngine.SceneManagement;

/** Handle 3 events - Rent Land (to notify user that will cost money)
                    - Plant method (giving 2 choices for planting)
                    - Harvest method (giving 2 choices for planting) */

public class MethodManager : MonoBehaviour
{
    public CardDisplay cardDisplay;
    public Card plantingCard;
    public Card harvestCard;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        if(scene.name == "10Simulation")
        {
            PlantingMethods();
        }
    }

    public void PlantingMethods()
    {
        Debug.Log("planting method showed");
        cardDisplay.OpenCardWindow(plantingCard);
    }

    public void HarvestMethods()
    {
        Debug.Log("planting method showed");
        cardDisplay.OpenCardWindow(harvestCard);
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
