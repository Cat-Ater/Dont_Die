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
    SCROLL_INTERUPT,
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
    bool skipEnabled = false;
    public GameObject underlay;
    public Text textOutput;
    public DialogueState _state = DialogueState.INACTIVE;
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

    public void SetInstanceUp(ITextCaller caller, string[] lines, float scrollSpeed)
    {
        //Set data. 
        _state = DialogueState.INIT;
        _lines = lines;
        _caller = caller;
        _scrollSpeed = scrollSpeed;

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

        if (Input.anyKey && _state == DialogueState.SCROLLING && skipEnabled)
        {
            //UI_Manager.PlaySoundOS = textboxSkip;
            _state = DialogueState.SCROLL_INTERUPT;
            StartCoroutine(KeypressSpamPrevention());
        }
        if (Input.anyKey && _state == DialogueState.LINE_WAIT)
        {
            _lineIndex++;

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

    private IEnumerator KeypressSpamPrevention()
    {
        yield return new WaitForSeconds(0.5f);
        skipEnabled = true;
    }

    private IEnumerator PrintLine(string text)
    {
        _state = DialogueState.SCROLLING;
        float perCharSpeed = _scrollSpeed / text.Length;
        DisplayStr = string.Empty;
        _charIndex = 0;

        while (DisplayStr.Length < text.Length && _state != DialogueState.SCROLL_INTERUPT)
        {
            DisplayStr += text[_charIndex];
            yield return new WaitForSeconds(perCharSpeed);
            _charIndex++;
        }

        //Update the state. 
        if (_state == DialogueState.SCROLL_INTERUPT)
            DisplayStr = text;

        _state = DialogueState.LINE_WAIT;
    }
}
