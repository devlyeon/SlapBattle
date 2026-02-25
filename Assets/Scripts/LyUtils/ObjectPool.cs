using System.Collections.Generic;
using UnityEngine;

namespace LyUtils
{
    public interface IObjectPool
    {
        /// <summary>
        /// GameObject를 ObjectPool에 반환합니다.
        /// </summary>
        /// <param name="obj">반환할 GameObject입니다.</param>
        public void PushObject(GameObject obj);
        /// <summary>
        /// GameObject를 ObjectPool에서 가져옵니다. 만일 ObjectPool에 남은 GameObject가 없는 경우 새로 생성됩니다.
        /// </summary>
        /// <returns>ObjectPool에서 가져온 GameObject입니다.</returns>
        public GameObject GetObject();
        /// <summary>
        /// ObjectPool에 지정된 GameObject의 개수를 가져옵니다.
        /// </summary>
        /// <returns>GameObject의 개수입니다.</returns>
        public int GetCount();
    }

    public class ObjectPool : IObjectPool
    {
        GameObject prefab;
        Queue<GameObject> objects = new Queue<GameObject>(); // 오브젝트 풀 구현을 위한 스택

        /// <summary>
        /// 지정된 GameObject를 관리하는 ObjectPool을 생성합니다.
        /// </summary>
        /// <param name="prefab">ObjectPool에서 관리할 GameObject입니다.</param>
        /// <param name="count">ObjectPool에서 처음 생성할 GameObject 개수입니다. 기본값은 5입니다.</param>
        public ObjectPool(GameObject prefab, int count = 5)
        {
            this.prefab = prefab;
            // 오브젝트 풀에 미리 오브젝트를 저장함
            for (int i = 0; i < count; i++)
            {
                GameObject obj = Object.Instantiate(this.prefab);
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
                obj = Object.Instantiate(prefab);
            }
            obj.SetActive(true);
            return obj;
        }

        public int GetCount() { return objects.Count; }
    }
}