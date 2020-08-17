using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private ARPlacementInteractableFromUI placement;
    
    [SerializeField] private Button[] buttons;

    [SerializeField] private Button toggleUIButton;

    [SerializeField] private Button switchPlacementsButton;

    [SerializeField] private RectTransform background;

    [SerializeField] private RectTransform EnvironmentList;
    
    [SerializeField] private RectTransform CharactersList;

    private Text switchText;

    private void Awake()
    { 
        switchText = switchPlacementsButton.GetComponentInChildren<Text>();
        // deleteButton.onClick.AddListener((SetSelectedObjectInactive));
        buttons[0].onClick.AddListener(() => ChangePrefab("Barrel"));
        buttons[1].onClick.AddListener(() => ChangePrefab("Big_torch"));
        buttons[2].onClick.AddListener(() => ChangePrefab("Crypt"));
        buttons[3].onClick.AddListener(() => ChangePrefab("Column_2"));
        buttons[4].onClick.AddListener(() => ChangePrefab("Door"));
        buttons[5].onClick.AddListener(() => ChangePrefab("Lever"));
        buttons[6].onClick.AddListener(() => ChangePrefab("Stairs"));
        buttons[7].onClick.AddListener(() => ChangePrefab("Wall_Column_Big"));
        buttons[8].onClick.AddListener(() => ChangePrefab("Wall_Column_Middle"));
        buttons[9].onClick.AddListener(() => ChangePrefab("Ranger"));
        buttons[10].onClick.AddListener(() => ChangePrefab("Wizard"));
        buttons[11].onClick.AddListener(() => ChangePrefab("Fighter"));
        buttons[12].onClick.AddListener(() => ChangePrefab("Rogue"));
        buttons[13].onClick.AddListener(() => ChangePrefab("Dragon"));
    }

    void ChangePrefab(string prefabName)
    {
        placement.placementPrefab = Resources.Load<GameObject>($"Prefabs/{prefabName}");
    }

    public void ToggleUI()
    {
        background.gameObject.SetActive(!background.gameObject.activeSelf);
    }

    public void SwitchPlacements()
    {
        if (EnvironmentList.gameObject.activeSelf)
        {
            switchText.text = "Environment";
            EnvironmentList.gameObject.SetActive(false);
            CharactersList.gameObject.SetActive(true);
        }
        else if (CharactersList.gameObject.activeSelf)
        {
            switchText.text = "Characters";
            CharactersList.gameObject.SetActive(false);
            EnvironmentList.gameObject.SetActive(true);
        }
    }
}
