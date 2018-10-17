using UnityEngine;
using UnityEngine.UI;
public class test : MonoBehaviour, INotification
{
    public Dropdown dropdown;
    public Text textCom;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        // PlayerPrefs.DeleteAll();
        LocalizationMgr.Instance.Init();
    }
    private void Start()
    {
        Refresh();
        LocalizationMgr.Instance.RefreshHander += Refresh;
        dropdown.onValueChanged.AddListener(ChangeLanguage);
        // textCom.text = LocalizationMgr.Instance.GetWordByDirect(1);
    }
    public void ChangeLanguage(int language)
    {
        // Debug.Log("ChangeLanguage:" + language);
        // var temp=language;
        // temp=language==0?10:10;
        // temp=language==1?40:10;
        // temp=language==2?22:10;
        if (language == 0)
        {
            language = 10;
        }
        else if (language == 1) { language = 40; }
        else if (language == 2) { language = 22; }
        Debug.Log("ChangeLanguage after:" + language);
        LocalizationMgr.Instance.ChangeLanguage((SystemLanguage)language);
    }

    public void Refresh()
    {
        textCom.text = LocalizationMgr.Instance.GetWordByReflection(1);
    }
}