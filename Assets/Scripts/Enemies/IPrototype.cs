using UnityEditor;
using UnityEngine;

namespace Enemies
{
    internal interface IPrototype
    {
        public GameObject Clone(Vector3 Position, Quaternion rot );
    }
}