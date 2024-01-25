using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Static instance of the GameManager
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject go = new GameObject("GameManager");
                    _instance = go.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }
    
    //Keybinds
    public Dictionary<string, KeyCode> KeyBinds;
    
    private void Awake()
    {
        if (_instance == null)
        {
            // If this is the first instance, set it as the singleton instance
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
        }

        KeyBinds = new Dictionary<string, KeyCode>();
        DefaultKeybinds();
    }
    public void BindKey(string key, KeyCode keyBind)
    {
        Dictionary<string, KeyCode> currentDictionary = KeyBinds;

        if (!currentDictionary.ContainsKey(key))
        {
            currentDictionary.Add(key,keyBind);
            //Update text
        }
        else if (currentDictionary.ContainsValue(keyBind))
        {
            string myKey = currentDictionary.FirstOrDefault(x => x.Value == keyBind).Key;

            currentDictionary[myKey] = KeyCode.None;
            //Update text
        }
        
        //update Text
    }

    private void DefaultKeybinds()
    {
        BindKey("P1_Forward", KeyCode.W);
        BindKey("P1_Left", KeyCode.A);
        BindKey("P1_Back", KeyCode.S);
        BindKey("P1_Right", KeyCode.D);
        BindKey("P1_Reset", KeyCode.R);
        
        BindKey("P2_Forward", KeyCode.UpArrow);
        BindKey("P2_Left", KeyCode.LeftArrow);
        BindKey("P2_Back", KeyCode.DownArrow);
        BindKey("P2_Right", KeyCode.RightArrow);
        BindKey("P2_Reset", KeyCode.Slash);
    }

    public KeyCode GetKey(string key)
    {
        Dictionary<string, KeyCode> currentDictionary = KeyBinds;

        if (currentDictionary.ContainsKey(key))
        {
            return currentDictionary[key];
        }
        else
        {
            return KeyCode.Joystick2Button6;
        }
    }
    
    
}