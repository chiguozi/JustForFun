using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WWWLoader
{
    static MonoBehaviour _behaviour;
    static MonoBehaviour behaveiour
    {
        get
        {
            if (null == _behaviour)
            {
                var go = GameObject.Find("Main");
                _behaviour = go.GetComponent<Launcher>();
            }
            return _behaviour;
        }
    }
    Dictionary<string, WWW> _loadingMap;
    public Action<WWW, string, string> onLoaded;
    public WWWLoader()
    {
        _loadingMap = new Dictionary<string, WWW>(5);
    }
    public int loadingCount { get { return _loadingMap.Count; } }
    public float GetProgress(string url)
    {
        WWW w3 = null;
        if (_loadingMap.TryGetValue(url, out w3))
            return w3.progress;
        return 0f;
    }
    public void Get(string url)
    {
        WWW w3 = new WWW(url);
        _loadingMap[url] = w3;
        behaveiour.StartCoroutine(DoLoad(this, w3));
    }
    void OnW3Done(WWW w3)
    {
        string url = w3.url;
        _loadingMap.Remove(url);
        if (null != onLoaded)
        {
            onLoaded(w3, url, w3.error);
        }
    }
    static IEnumerator DoLoad(WWWLoader loader,WWW w3)
    {
#if UNITY_EDITOR
        yield return null;
#endif
        yield return w3;
        loader.OnW3Done(w3);
    }
    
}
