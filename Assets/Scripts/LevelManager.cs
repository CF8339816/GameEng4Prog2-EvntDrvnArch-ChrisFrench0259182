using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public GameObject Level01;
    public GameObject Level02;
    public GameObject Level03;
  
    public GameObject currentActiveLevel;
   
    public GameObject levelToLoad;
    

    public void Start()
    {
        currentActiveLevel = Level01;// sets default starting stage
        
    }
    public void CloseAllScreens() //closes all levels
    {
      
        Level01.SetActive(false);
        Level02.SetActive(false);
        Level03.SetActive(false);
      
    }
    public void levelChange(GameObject levelToLoad ) // processes level change 
    {
        CloseAllScreens();

        currentActiveLevel.SetActive(false);
        levelToLoad.SetActive(true);
        currentActiveLevel = levelToLoad;

      
    }

    public void LoadNextChronologicalLevel()  //  loads stages in next chronological order
    {
         if (currentActiveLevel == Level01)
        {
            levelChange(Level02);
        }
        else if (currentActiveLevel == Level02)
        {
            levelChange(Level03);
        }
        

    }
}
