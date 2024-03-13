[System.Serializable]
public class PatternDialogue : PatternTimedEvent, ITextCaller
{
    public string[] dialogueStrings;
    public float scrollSpeed;
    public float endLineDelay;
    public float eventTime;

    public override void FireEvent() => DisplayText();

    private void DisplayText()
    {
        fired = true;
        UIManager.Instance.DisplayText(this, dialogueStrings, scrollSpeed, endLineDelay);
    }

    public void DisplayComplete()
    {
    }
}
