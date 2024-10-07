using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utility;

enum ButtonState
{
    Normal,
    Highlighted,
    Pressed,
    Selected,
    Disabled
}

[Serializable]
struct StateEditor
{
    public ButtonState State;
    public bool UseState;
    
    public bool AlterTextColor;
    public Color TextColor;

    public ButtonAnimationBase AnimationToDo;
}

public class ButtonController : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    ISelectHandler, IDeselectHandler,
    IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI text;
    private ButtonState _buttonState;
    private bool _mouseIsHovering;

    [SerializeField] private List<StateEditor> ButtonStates;
    public RectTransform InitialTransform { get; private set; }
    
    private StateEditor[] _stateEditors;
   
    private void Awake()
    {
        InitialTransform = TransformUtil.CreateStandaloneRectTransform(gameObject.GetComponent<RectTransform>());
        if(button == null) button = GetComponent<Button>();
        if(text == null) text = GetComponentInChildren<TextMeshProUGUI>();
        _buttonState = ButtonState.Normal;
        _mouseIsHovering = false;
    }

    void Start()
    {
        button.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        Debug.Log("Button clicked!");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _mouseIsHovering = true;
        if (_buttonState == ButtonState.Selected) return;
        _buttonState = ButtonState.Highlighted;
        DoChanges();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _mouseIsHovering = false;

        if (_buttonState == ButtonState.Selected) return;
        _buttonState = ButtonState.Normal;
        DoChanges();
    }

    public void OnSelect(BaseEventData eventData)
    {
        _buttonState = ButtonState.Selected;
        DoChanges();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        _buttonState = ButtonState.Normal;
        if (_mouseIsHovering) _buttonState = ButtonState.Highlighted;
        DoChanges();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _buttonState = ButtonState.Pressed;
        DoChanges();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _buttonState = ButtonState.Normal;
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            _buttonState = ButtonState.Selected;
        }
        else if (_mouseIsHovering)
        {
            _buttonState = ButtonState.Highlighted;
        }

        DoChanges();
    }

    private void DoChanges()
    {
        StopAndResetTween();
        var editorState = GetEditor(_buttonState);
        var animationExists = editorState.AnimationToDo != null;
        
        if (!editorState.UseState || (editorState.AlterTextColor == false && !animationExists))  return;

        if (animationExists) editorState.AnimationToDo.CallAnimation(this);
        
        if (editorState.AlterTextColor && text != null) text.color = editorState.TextColor;
    }


    private StateEditor GetEditor(ButtonState state)
    {
        foreach (var stateEditor in ButtonStates.Where(stateEditor => stateEditor.State == state))
        {
            return stateEditor;
        }

        return new StateEditor();
    }
    
    public void ScaleUpAndDown(bool loop)
    {
        // Scale up the object
        transform.DOScale(1.5f, 1f)
            .SetEase(Ease.InOutSine)  // You can change the ease type to another if desired
            .OnComplete(() =>
            {
                // Scale back down after scaling up is complete
                transform.DOScale(1f, 1f)
                    .SetEase(Ease.InOutSine)
                    .OnComplete(() =>
                    {
                        if (loop) ScaleUpAndDown(loop); 
                    });
            });
    }
    
    public void ScaleUp(float increaseBy)
    {
        Vector3 targetScale = button.transform.localScale + new Vector3(increaseBy,increaseBy,increaseBy);
        transform.DOScale(targetScale, 1f).SetEase(Ease.InOutSine); 
    }

    private void StopAndResetTween()
    {
        transform.DOKill();
        TransformUtil.CopyTransformData(InitialTransform,transform);
    }
}