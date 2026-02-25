using System.Collections.Generic;
using UnityEngine;

namespace LyUtils
{
    public class ObjectPoolScriptable : MonoBehaviour, IObjectPool
    {
        GameObject prefab;
        Queue<GameObject> objects = new Queue<GameObject>(); // 오브젝트 풀 구현을 위한 스택

        public void SetObject(GameObject prefab, int count = 5)
        {
            this.prefab = prefab;
            // 오브젝트 풀에 미리 오브젝트를 저장함
            for (int i = 0; i < count; i++)
            {
                GameObject obj = Instantiate(this.prefab);
                obj.SetActive(false);
                objects.Enqueue(obj);
            }
        }
        
        public void PushObject(GameObject obj)
        { 
            obj.SetActive(false);
            objects.Enqueue(obj); 
        }

        public GameObject GetObject() { 
            GameObject obj;
            if (objects.Count > 0) {
                // 오브젝트 풀에 오브젝트가 존재하는 경우 Pop
                obj = objects.Dequeue();
            } else {
                // 없는 경우 생성
                obj = Instantiate(prefab);
            }
            obj.SetActive(true);
            return obj;
        }

        /// <summary>
        /// ObjectPool에 지정된 GameObject를 다른 GameObject로 변경합니다.
        /// </summary>
        /// <param name="prefab">ObjectPool에서 관리할 GameObject입니다.</param>
        /// <param name="count">ObjectPool에서 처음 생성할 GameObject 개수입니다. 기본값은 5입니다.</param>
        public void ChangeObject(GameObject prefab, int count = 5)
        {
            while (objects.Count > 0) { Destroy(objects.Dequeue()); }
            SetObject(prefab, count);
        }

        public int GetCount() { return objects.Count; }
    }
}