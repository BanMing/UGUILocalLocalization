using UnityEngine;
using UnityEngine.UI;
public class TextNotification : MonoBehaviour, INotification
{
    public int textId;
    private Text textCom;
    void Start()
    {
        textCom=gameObject.GetComponent<Text>();
    }
    public void Refresh()
    {

    }
}