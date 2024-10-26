using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MTL.Combat {

    [Serializable]
    public class Path : MonoBehaviour {
        List<Transform> nodeList = new List<Transform>();
        public int nodeTotalNum => nodeList.Count;

        private void Start() {
            foreach (Transform t in transform) {
                nodeList.Add(t);
            }
        }

        public Vector3 GetNodePos(int index) {
            return nodeList[index].position;
        }
    }
}