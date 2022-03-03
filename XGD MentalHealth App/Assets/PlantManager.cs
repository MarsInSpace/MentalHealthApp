using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public static bool IsMouseOver = false;

    [SerializeField] GameObject FirstPlant;
    [SerializeField] GameObject SecondPlant;
    [SerializeField] GameObject ThirdPlant;

    void Start()
    {
        TriggerNextSprite();
    }

    void Update()
    {
        TriggerNextSprite();
    }

    void TriggerNextSprite(){
        if(TaskButtons.GrowthCounter < 2){
            FirstPlant.SetActive(true);
            SecondPlant.SetActive(false);
            ThirdPlant.SetActive(false);
        }
        else if(TaskButtons.GrowthCounter == 2){
            FirstPlant.SetActive(false);
            SecondPlant.SetActive(true);
            ThirdPlant.SetActive(false);
        }
        else if(TaskButtons.GrowthCounter == 5){
            FirstPlant.SetActive(false);
            SecondPlant.SetActive(false);
            ThirdPlant.SetActive(true);
        }
    }

    private void OnMouseOver() {
        IsMouseOver = true;
    }

    private void OnMouseExit() {
        IsMouseOver = false;
    }
}
