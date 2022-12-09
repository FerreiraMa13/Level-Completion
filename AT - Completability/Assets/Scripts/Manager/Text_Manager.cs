using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Text_Manager : MonoBehaviour
{
    public Canvas canvas;
    public TMP_Text text_to_update;
    public string error_message_apend = "Could not reach: ";
    public Color error_color = Color.red;
    public bool valid = true;

    [Space(1)]
    public string default_tag = "DefaultUI";
    public string failed_tag = "FailedUI";

    private string failed_name = "";
    private List<GameObject> default_ui = new();
    private List<GameObject> failed_ui = new();
    private void Awake()
    {
        if(!text_to_update)
        {
            valid = false;
        }
        PopulateList(default_ui, default_tag);
        PopulateList(failed_ui, failed_tag);
        ToggleList(failed_ui, false);
    }
    public void UpdateName(string new_name)
    {
        failed_name = new_name;
    }
    public void ToggleDisplay()
    {
        ToggleList(default_ui, false);
        UpdateElements();
        ToggleList(failed_ui, true);
    }
    public void EndToggle()
    {
        ToggleList(default_ui, false);
    }
    private bool PopulateList(List<GameObject> list, string tag)
    {
        var temp_list = GameObject.FindGameObjectsWithTag(tag);
        if(temp_list == null)
        {
            Debug.Log("Failed to load tag: " + tag);
            return false;
        }
        foreach(var child in temp_list)
        {
            if(!list.Contains(child))
            {
                list.Add(child);
            }
        }
        return true;
    }
    private void ToggleList(List<GameObject> list, bool value)
    {
        foreach(var element in list)
        {
            element.SetActive(value);
        }
    }
    private void UpdateElements()
    {
        text_to_update.text = error_message_apend + failed_name;
    }
    public void ErrorMessageCNR(string name)
    {
        UpdateName(name);
        ToggleDisplay();
    }
}
