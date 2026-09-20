using System;
using TMPro;
using Unity.Jobs;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;
using UnityEngine.Android;

public class EventManager : MonoBehaviour
{

    [SerializeField] private TMP_Text itemCountPerLevel;
    [SerializeField] private TMP_Text InfoBox;
    [SerializeField] private Slider itemCollection;
    [SerializeField] private int MaxItems = 40;

    [SerializeField] public LevelManager levelManager;

    public TextMeshProUGUI textItemsCount;
    public TextMeshProUGUI textInfoBox;


    private int currentItems = 0;
    private int ItemPerLevelCount = 0;
    private int ItemsCount;
    private GameObject currentActiveLevel;
    public int MaxItemPerLevel;
   private GameObject activeLevel;
    void Start()
    {
        itemCollection.value = 0;
        itemCollection.minValue = 0;
        itemCollection.maxValue = 40;
        ItemPerLevelCount = 0;

        if (levelManager != null)
        {
            activeLevel = levelManager.currentActiveLevel;
            setItemsPerLevel();
            SetItemsValue();
        }

    




    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //checks for keypress to simulate item pickup
        {
            collectedItems(); //checks if max items reached for game  checks for max per level items for level change
            ItemsCount++;//adds 1 to max game count when picked up
            ItemPerLevelCount++;//adds 1 to level count when picked up
          
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) //checks for keypress to simulate level change manually
        {
            ManualLevelChange(levelManager.Level01);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) //checks for keypress to simulate level change manually
        {
            ManualLevelChange(levelManager.Level02);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) //checks for keypress to simulate level change manually
        {
            ManualLevelChange(levelManager.Level03);
        }
       
     
    }


    private void ManualLevelChange(GameObject targetLevel)//manual  level change   and  reset
    {
        activeLevel = targetLevel;
        ItemPerLevelCount = 0; // Reset level progress on manual skip
        setItemsPerLevel(); // sets  max ipl
        SetItemsValue();  // resets level collection counter
    }

    public void collectedItems()
    {
        currentItems = ItemsCount;  // sets slider value to collected value

        if (currentItems >= MaxItems)
        {
            textInfoBox.text = "you have collected all the items Needed  in the game  congrats you win";
            return; //  hard stop at 40 even though there are more cookies
        } 

        itemCollection.value = currentItems;// sets calery slider value to  currentcalories variable

        Debug.Log($"Collected: {currentItems}/{MaxItems}"); //verifies  the item slider addition whenitems are picked up

        if (currentItems < MaxItems)// if items are not at game max checks for  if at level max for level change
        {
            whenMaxPerLevelItems();
        }
    }

    public void whenMaxPerLevelItems() //Triggers stage change loads  next level and resets collection and sets max collection
    {
        if (ItemPerLevelCount == MaxItemPerLevel)
        {
            textInfoBox.text = "you have collected all the items in this stage!";
            levelManager.LoadNextChronologicalLevel();

            activeLevel = levelManager.currentActiveLevel;

            setItemsPerLevel(); // sets  max ipl

            SetItemsValue();  // resets level collection counter

        }
       
    }


    public void setItemsPerLevel()// sets the max collectable items per level to trrigger stage change
      {
       
        if (activeLevel == levelManager.Level01)
        {
           // ItemPerLevelCount = 0;
            MaxItemPerLevel = 10;
        }
        else if (activeLevel == levelManager.Level02)
        {
           // ItemPerLevelCount = 0;
            MaxItemPerLevel = 15;
        }
        else if (activeLevel == levelManager.Level03)
        {
            //ItemPerLevelCount = 0;
            MaxItemPerLevel = 20;
        }
        
      }
     public void SetItemsValue()  // sets the text output for the stage
     {
        
        textItemsCount.text = "Item Count: " + ItemPerLevelCount.ToString() + "/" + MaxItemPerLevel.ToString(); // sets count to output to string
           
     }

}



