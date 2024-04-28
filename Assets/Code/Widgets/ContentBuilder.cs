using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ContentBuilder : MonoBehaviour
{
    public List<ContentItem> items = new List<ContentItem>();

    public ContentItem contentPrefab;

    public Transform scrollView;

    public virtual void RefreshContent()
    {

    }
}
