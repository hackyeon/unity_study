using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class UI_Button : UI_Popup
{
    
    enum Buttons
    {
        PointButton
    }
    enum Texts
    {
        PointText,
        ScoreText
    }

    enum GameObjects
    {
        TestObject
    }

    enum Images
    {
        // ItemIcon
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        // GameObject itemIcon = GetImage((int)Images.ItemIcon).gameObject;
        // AddUIEvent(itemIcon, data => { itemIcon.transform.position = data.position; }, Define.UIEvent.Drag );
        
        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);
    }


    private int _score = 0;
    public void OnButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"점수 : {_score}점";
    }
}
