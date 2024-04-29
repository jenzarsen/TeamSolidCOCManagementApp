using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UIController : Singleton<UIController>
{
    [Sirenix.OdinInspector.ShowInInspector, ReadOnly]
    static List<BaseUI> baseUIs = new List<BaseUI>();

    protected override void Awake()
    {
       base.Awake();
       baseUIs = GetComponentsInChildren<BaseUI>(true).ToList();

       HideAllUI();
    }

    private void Start()
    {
        GetUI<MainUI>().Show();
    }

    public void HideAllUI()
    {
        foreach(var ui in baseUIs)
        {
            if(ui is not MainUI)
            {
                ui.Hide();
            }
        }
    }

    public static T GetUI<T>() where T : BaseUI
    {
        return (T)baseUIs.FirstOrDefault(x => x is T);
    }
}
