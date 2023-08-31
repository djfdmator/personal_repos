using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern
{
    public interface IState
    {
        void Handle(MonoStateHandler _handler);
        Enum GetEnum();

        void Activate(bool isOn);
    }
}