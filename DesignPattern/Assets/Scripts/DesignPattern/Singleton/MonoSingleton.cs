using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPattern
{
    public class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        private static T m_Instance;
        public static T Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = FindObjectOfType<T>();
                    if (m_Instance == null)
                    {
                        var gameObject = new GameObject();
                        gameObject.name = typeof(T).Name;
                        m_Instance = gameObject.AddComponent<T>();
                    }
                }
                return m_Instance;
            }
        }

        private void Awake()
        {
            if (m_Instance != null)
            {
                Destroy(this);
                return;
            }

            m_Instance = this as T;
            Awake_Imp();
        }

        protected virtual void Awake_Imp()
        {
            //�������̵��ؼ� DontDestroyOnLoad �ϸ� ���� �Ŵ���, �ƴϸ� ���� �Ŵ���
        }
    }
}