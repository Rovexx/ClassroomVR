﻿using UnityEngine;
using System.Collections.Generic;

namespace Com.Roel.ClassroomVR
{
    public class ExtraTags : MonoBehaviour 
    {
        [SerializeField]
        private List<string> tags = new List<string>();

        public bool HasTag(string tag)
        {
            return tags.Contains(tag);
        }
        
        public IEnumerable<string> GetTags()
        {
            return tags;
        }
        
        public void Rename(int index, string tagName)
        {
            tags[index] = tagName;
        }
        
        public string GetAtIndex(int index)
        {
            return tags[index];
        }
        
        public int Count
        {
            get { return tags.Count; }
        }
    }
}