using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public interface ITextCaller
{
    void DisplayComplete();
}

public enum DialogueState
{
    INACTIVE,
    INIT,
    SCROLLING,
    LINE_WAIT,
    COMPLETE
}

public class UI_DialogueDisplay : MonoBehaviour
{

    ITextCaller _caller;
    int _lineIndex;
    int _charIndex;
    string[] _lines;
    float _scrollSpeed;
    float _endlLineWait;
    public GameObject underlay;
    public Text textOutput;
    public DialogueState _state = DialogueState.INACTIVE;
    bool lineWaitCalled = false;
    bool endLineWaitElapsed = false;
    //public AudioClip textboxOpen;
    //public AudioClip textboxClose;
    //public AudioClip textboxType;
    //public AudioClip textboxSkip;

    public string DisplayStr
    {
        set => textOutput.text = value;
        get => textOutput.text;
    }

    private void OnEnable() => ResetState(false);

    public void SetInstanceUp(ITextCaller caller, string[] lines, float scrollSpeed, float endLineSpeed)
    {
        //Set data. 
        _state = DialogueState.INIT;
        _lines = lines;
        _caller = caller;
        _scrollSpeed = scrollSpeed;
        _endlLineWait = endLineSpeed;

        //Reset values. 
        ResetState(true);

        //Begin print.
        StartCoroutine(PrintLine(_lines[_lineIndex]));
    }

    private void ResetState(bool displayState)
    {
        underlay.SetActive(displayState);
        _lineIndex = 0;
        textOutput.text = "";
        textOutput.enabled = displayState;
    }

    void Update()
    {
        if (_state == DialogueState.INACTIVE)
            return;

        if (endLineWaitElapsed && _state == DialogueState.LINE_WAIT)
        {
            _lineIndex++;
            endLineWaitElapsed = false;

            if (_lineIndex < _lines.Length)
                StartCoroutine(PrintLine(_lines[_lineIndex]));
            else
            {
                _state = DialogueState.INACTIVE;
                _caller.DisplayComplete();
                ResetState(false);
            }
        }
    }


    private IEnumerator EndLineWait()
    {
        yield return new WaitForSeconds(_endlLineWait);
        endLineWaitElapsed = true;
        lineWaitCalled = false;
    }

    private IEnumerator PrintLine(string text)
    {
        _state = DialogueState.SCROLLING;
        DisplayStr = string.Empty;
        _charIndex = 0;

        while (DisplayStr.Length < text.Length)
        {
            DisplayStr += text[_charIndex];
            yield return new WaitForSeconds(_scrollSpeed);
            _charIndex++;
        }

        if (DisplayStr.Length == text.Length && lineWaitCalled == false)
            StartCoroutine(EndLineWait());

        _state = DialogueState.LINE_WAIT;
    }
}
