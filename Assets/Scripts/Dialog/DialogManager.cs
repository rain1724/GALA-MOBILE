using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] ChoiceBox choiceBox;
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPerSecond;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;


    public static DialogManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

   

    public bool IsShowing { get; private set; }

    public IEnumerator ShowDialogText (string text, bool waitForinput=true, bool autoClose=true)
    {
        OnShowDialog?.Invoke();
        IsShowing = true;
        dialogBox.SetActive(true);

        yield return TypeDialog(text);
        if (waitForinput)
        {
            yield return new WaitUntil(() => CrossPlatformInputManager.GetButtonDown("interact"));
        }
        
        if (autoClose)
        {
            CloseDialog();
        }

    }

    public void CloseDialog()
    {
        dialogBox.SetActive(false);
        IsShowing = false;
        OnCloseDialog?.Invoke();

    }

    //Showing Dialog when Triggred by Key
    public IEnumerator ShowDialog(Dialog dialog, List<string> choices=null, 
        Action<int>onchoiceSelected=null) 
    {
        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();
        IsShowing = true;
        dialogBox.SetActive(true);

        foreach (var line in dialog.Lines)
        {
            yield return TypeDialog(line);
            yield return new WaitUntil(() => CrossPlatformInputManager.GetButtonDown("interact"));
        }

        if (choices != null && choices.Count > 1)
        {
           yield return choiceBox.ShowChoices(choices, onchoiceSelected);

        }
        

        dialogBox.SetActive(false);
        IsShowing = false;
        OnCloseDialog?.Invoke();
    }

    public void HandleUpdate()
    {
       
    }
    //dialog animation
    //dialog shows letter by letter
    public IEnumerator TypeDialog(string line)
    {
        
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
     
    }
}
