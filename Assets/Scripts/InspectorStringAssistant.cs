using UnityEngine;
using System.Collections;

public class InspectorStringAssistant
{
    private static InspectorStringAssistant minstance;

    public static InspectorStringAssistant instance
    {
        get
        {
            if (minstance == null)
            {
                minstance = new InspectorStringAssistant();
            }
            return minstance;
        }
    }

    public string make(string str)
    {
        str = str.Replace("\\n", "\n");
        return str;
    }
}
