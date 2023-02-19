using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
        GridPanel
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        // 임시 인벤토리 정보 입력
        for (int i = 0; i < 8; i++)
        {
            UI_Inven_Item item = Managers.UI.MakeSubItem<UI_Inven_Item>(parent: gridPanel.transform);
            item.SetInfo($"바인드{i}");
        }
        
    }
}
