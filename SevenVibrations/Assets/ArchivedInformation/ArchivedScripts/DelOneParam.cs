using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelOneParam : MonoBehaviour
{
    public delegate void EventParam(float _amount);

    EventParam myEvent;

    public void BindToEvent(EventParam _func)
    {
        myEvent += _func;
    }

    public void UnbindEvent(EventParam _func)
    {
        myEvent -= _func;
    }

    public void CallEvent(float _param)
    {
        if(null == myEvent)
        {
            return;
        }
        myEvent.Invoke(_param);
    }

}
