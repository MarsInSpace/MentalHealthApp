using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AppManager : MonoBehaviour
{
    [SerializeField] GameObject WelcomeScreen;
    [SerializeField] GameObject RewardInfoPanel;
    [SerializeField] GameObject RewardsTextField;

    [SerializeField] GameObject TaskInfoPanel;

    [SerializeField] GameObject InputPanel;
    [SerializeField] TMP_InputField Field;
    [SerializeField] TMP_Text InputHeader;
    [SerializeField] TMP_Text InputText;
    List<string> InputRewards = new List<string>();
    List<string> InputTasks = new List<string>();

    static bool IsInRewardMode = false;
    static bool IsInTaskMode = false;

    [SerializeField] TMP_Dropdown TaskOne;
    [SerializeField] TMP_Dropdown TaskTwo;
    [SerializeField] TMP_Dropdown TaskThree;
    [SerializeField] TMP_Dropdown TaskFour;
    [SerializeField] TMP_Dropdown TaskFive;

    static bool IsIngame = false;

    [SerializeField] GameObject Buttons;
    [SerializeField] GameObject Plant;
    [SerializeField] TMP_Text ButtonTextOne;
    [SerializeField] TMP_Text ButtonTextTwo;
    [SerializeField] TMP_Text ButtonTextThree;
    [SerializeField] TMP_Text ButtonTextFour;
    [SerializeField] TMP_Text ButtonTextFive;

    [SerializeField] GameObject RewardPanel;
    [SerializeField] GameObject RewardPanelTwo;
    [SerializeField] TMP_Text RewardTextOne;
    [SerializeField] TMP_Text RewardTextTwo;
    [SerializeField] TMP_Text RewardTextThree;

    [SerializeField] TMP_Text RewardTextShown;
    static string ChosenReward;

    [SerializeField] GameObject PanicPanel;

    void Start()
    {
        WelcomeScreen.SetActive(true);
        RewardInfoPanel.SetActive(false);
        TaskInfoPanel.SetActive(false);
        InputPanel.SetActive(false);
        Buttons.SetActive(false);
        Plant.SetActive(false);
        RewardPanel.SetActive(false);
        RewardPanelTwo.SetActive(false);
        PanicPanel.SetActive(false);
    }

    void Update()
    {
        RewardPanelRunning(IsInRewardMode);
        TaskPanelRunning(IsInTaskMode);

        if (!IsIngame)
            return;

        TriggerRewardPanel();

        RewardTextShown.text = ChosenReward;
    }

    //pre game panels
    public void TriggerRewardsInfoPanel()
    {
        WelcomeScreen.SetActive(false);
        RewardInfoPanel.SetActive(true);
        IsInRewardMode = true;
    }

    public void TriggerTaskInfoPanel()
    {
        RewardInfoPanel.SetActive(false);
        TaskInfoPanel.SetActive(true);
        IsInRewardMode = false;
        IsInTaskMode = true;
    }

    public void OpenInputPanel()
    {
        Field.Select();
        Field.text = "";

        InputPanel.SetActive(true);

        if (IsInRewardMode)
        {
            InputHeader.text = "Enter Reward:";
        }
        else if (IsInTaskMode)
        {
            InputHeader.text = "Enter your own:";
        }
        else
        {
            InputHeader.text = "Enter here:";
        }
    }

    public void EnterInput()
    {
        if (IsInRewardMode)
        {
            string newReward = InputText.text;
            InputRewards.Add(newReward);

            InputPanel.SetActive(false);
        }
        else if (IsInTaskMode)
        {
            string newTask = InputText.text;
            InputTasks.Add(newTask);

            InputPanel.SetActive(false);
        }
    }

    void RewardPanelRunning(bool isRunning)
    {
        if (!isRunning)
            return;

        string rewardsString = "";

        if(InputRewards != null)
        {
            foreach(string listElement in InputRewards)
            {
                rewardsString += listElement + "\n";
            }

            RewardsTextField.GetComponent<TMP_Text>().text = rewardsString;
        }
    }

    void TaskPanelRunning(bool isRunning)
    {
        if (!isRunning)
            return;

        if (InputTasks != null)
        {
            TaskOne.AddOptions(InputTasks);
            TaskTwo.AddOptions(InputTasks);
            TaskThree.AddOptions(InputTasks);
            TaskFour.AddOptions(InputTasks);
            TaskFive.AddOptions(InputTasks);
        }
    }

    public void ExitSetupMode()
    {
        TaskInfoPanel.SetActive(false);
        IsIngame = true;
        InitializeMainScreen();
    }

    //ingame panels

    void InitializeMainScreen()
    {
        Buttons.SetActive(true);
        Plant.SetActive(true);

        ButtonTextOne.text = TaskOne.options[TaskOne.value].text;
        ButtonTextTwo.text = TaskTwo.options[TaskTwo.value].text;
        ButtonTextThree.text = TaskThree.options[TaskThree.value].text;
        ButtonTextFour.text = TaskFour.options[TaskFour.value].text;
        ButtonTextFive.text = TaskFive.options[TaskFive.value].text;
    }

    //reward panels
    void TriggerRewardPanel()
    {
        if (TaskButtons.IsTaskComplete)
        {
            RewardPanel.SetActive(true);
            RandomReward(RewardTextOne);
            RandomReward(RewardTextTwo);
            RandomReward(RewardTextThree);
            
            TaskButtons.IsTaskComplete = false;
        }
    }

    void RandomReward(TMP_Text rewardText){
        int index = Random.Range(0, InputRewards.Count);
        rewardText.text = InputRewards[index].ToString();
    }

    public void ChooseRewardOne(){
        ChosenReward = RewardTextOne.text;
    }
    public void ChooseRewardTwo(){
        ChosenReward = RewardTextTwo.text;
    }
    public void ChooseRewardThree(){
        ChosenReward = RewardTextThree.text;
    }

    public void TriggerReward()
    {
        RewardPanel.SetActive(false);
        RewardPanelTwo.SetActive(true);
    }

    public void CloseRewards()
    {
        RewardPanelTwo.SetActive(false);
    }

    //Panic Button
    public void OpenPanicPanel()
    {
        PanicPanel.SetActive(true);
    }

    public void ClosePanicPanel()
    {
        PanicPanel.SetActive(false);
    }
}
