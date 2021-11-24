using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
        
        
    }
    
    
    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;
    
    // References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    
    // Logic
    public int pesos;
    public int experience;

    // Floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
        
    }
    
    // Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // is the weapon max level?
        if (weaponPrices.Count <= weapon.weaponLevel)
            return false;
        if (pesos >= weaponPrices[weapon.weaponLevel])
        {
            pesos -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }

        return false;
    }
    
    // Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;
        
        while(experience >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count)
            {
                return r;
            }
        }

        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }
    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        experience += xp;
        if (currLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }
    
    public void OnLevelUp()
    {
        player.OnLevelUp();
    }
    // Save state
    
    // INT preferedSkin
    // INT pesos
    // INT experience
    // INT weaponLevel
    
    
    
    public void SaveState()
    {
        
        string s = "";

        s += "0" + "|";
        s += pesos.ToString() + "|";
        s += experience.ToString() + "|";
        s += weapon.weaponLevel.ToString();
        
        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        
        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }
        
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        //  "0|10|15|2"
        
        // Change player skin
        pesos = int.Parse(data[1]);
        
        // Experience
        experience = int.Parse(data[2]);
        player.SetLevel(GetCurrentLevel());
        
        // Change the weapon Level
        weapon.SetWeaponLevel(int.Parse(data[3]));
        


    }
}
