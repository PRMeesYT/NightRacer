using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons;
    public int selectedLevel;

    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 1); 

        for (int i = 0; i < lvlButtons.Length; i++)
        {
            if (i + 1 > levelAt)
                lvlButtons[i].interactable = false;
        }
    }

}