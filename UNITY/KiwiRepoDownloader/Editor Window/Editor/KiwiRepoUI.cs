#region

using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

#endregion

public class KiwiRepoUI : EditorWindow
{
    [SerializeField] private VisualTreeAsset m_VisualTreeAsset;
    private readonly List<string> finderName = new List<string>();

    public void CreateGUI()
    {
        string data = AutoRepo.Request("https://api.github.com/repos/alex1600/kiwi-dev/contents/UNITY");
        List<JsonParse.Root> directdl = JsonConvert.DeserializeObject<List<JsonParse.Root>>(data);

        if (directdl != null)
        {
            foreach (JsonParse.Root uri in directdl)
            {
                finderName.Add(uri.name);
            }
        }
        VisualElement root = rootVisualElement;

        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate("Asset/Editor Window/Editor/KiwiRepoUI.uxml");
        foreach (string uri in finderName)
        {
            labelFromUXML.Add(new Toggle
                { text = uri, name = uri });
        }
        labelFromUXML.Add(new Button
            { text = "Download", name = "Download" });
        root.Add(labelFromUXML);
        SetupButtonHandler();
        SetupToggleHandler();
    }


    [MenuItem("KiwiRepo/KiwiRepoUI")]
    public static void ShowExample()
    {
        var wnd = GetWindow<KiwiRepoUI>();
        wnd.titleContent = new("KiwiRepoUI");
    }

    private void SetupButtonHandler()
    {
        UQueryBuilder<Button> buttons = rootVisualElement.Query<Button>();
        buttons.ForEach(RegisterHandler);
    }
    private void RegisterHandler(Button button)
    {
        button.RegisterCallback<ClickEvent>(DownlaodButton);
    }

    private void SetupToggleHandler()
    {
        foreach (string tooglename in finderName)
        {
            UQueryBuilder<Toggle> toggle = rootVisualElement.Query<Toggle>(tooglename);
            toggle.ForEach(RegisterHandler);
        }

    }
    
    private void RegisterHandler(Toggle toggle)
    {
        toggle.RegisterCallback<ChangeEvent<Toggle>>(DefautToggle);
    }

    private void DownlaodButton(ClickEvent evt)
    {
        foreach (string tooglename in finderName)
        {
            var toggle = rootVisualElement.Q<Toggle>(tooglename);
            if (toggle.value)
            {
                AutoRepo.JsonParsing(AutoRepo.Request($"https://api.github.com/repos/alex1600/kiwi-dev/contents/UNITY/{toggle.text}"), toggle.text);
            }
        }
        Close();
    }

    private void DefautToggle(ChangeEvent<Toggle> cet) { }
}
