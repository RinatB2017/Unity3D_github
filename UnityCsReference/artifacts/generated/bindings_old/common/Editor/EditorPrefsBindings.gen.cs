// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using scm=System.ComponentModel;
using uei=UnityEngine.Internal;
using RequiredByNativeCodeAttribute=UnityEngine.Scripting.RequiredByNativeCodeAttribute;
using UsedByNativeCodeAttribute=UnityEngine.Scripting.UsedByNativeCodeAttribute;

using System;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEditor
{


public sealed partial class EditorPrefs
{
    internal delegate void ValueWasUpdated(string key);
    internal static event ValueWasUpdated onValueWasUpdated = null;
    
    
    [UnityEngine.Scripting.GeneratedByOldBindingsGeneratorAttribute] // Temporarily necessary for bindings migration
    [System.Runtime.CompilerServices.MethodImplAttribute((System.Runtime.CompilerServices.MethodImplOptions)0x1000)]
    extern private static  void SetIntInternal (string key, int value) ;

    public static void SetInt(string key, int value)
        {
            SetIntInternal(key, value);
            if (onValueWasUpdated != null)
                onValueWasUpdated(key);
        }
    
    
    [UnityEngine.Scripting.GeneratedByOldBindingsGeneratorAttribute] // Temporarily necessary for bindings migration
    [System.Runtime.CompilerServices.MethodImplAttribute((System.Runtime.CompilerServices.MethodImplOptions)0x1000)]
    extern public static  int GetInt (string key, [uei.DefaultValue("0")]  int defaultValue ) ;

    [uei.ExcludeFromDocs]
    public static int GetInt (string key) {
        int defaultValue = 0;
        return GetInt ( key, defaultValue );
    }

    [UnityEngine.Scripting.GeneratedByOldBindingsGeneratorAttribute] // Temporarily necessary for bindings migration
    [System.Runtime.CompilerServices.MethodImplAttribute((System.Runtime.CompilerServices.MethodImplOptions)0x1000)]
    extern private static  void SetFloatInternal (string key, float value) ;

    public static void SetFloat(string key, float value)
        {
            SetFloatInternal(key, value);
            if (onValueWasUpdated != null)
                onValueWasUpdated(key);
        }
    
    
    [UnityEngine.Scripting.GeneratedByOldBindingsGeneratorAttribute] // Temporarily necessary for bindings migration
    [System.Runtime.CompilerServices.MethodImplAttribute((System.Runtime.CompilerServices.MethodImplOptions)0x1000)]
    extern public static  float GetFloat (string key, [uei.DefaultValue("0.0F")]  float defaultValue ) ;

    [uei.ExcludeFromDocs]
    public static float GetFloat (string key) {
        float defaultValue = 0.0F;
        return GetFloat ( key, defaultValue );
    }

    [UnityEngine.Scripting.GeneratedByOldBindingsGeneratorAttribute] // Temporarily necessary for bindings migration
    [System.Runtime.CompilerServices.MethodImplAttribute((System.Runtime.CompilerServices.MethodImplOptions)0x1000)]
    extern private static  void SetStringInternal (string key, string value) ;

    public static void SetString(string key, string value)
        {
            SetStringInternal(key, value);
            if (onValueWasUpdated != null)
                onValueWasUpdated(key);
        }
    
    
    [UnityEngine.Scripting.GeneratedByOldBindingsGeneratorAttribute] // Temporarily necessary for bindings migration
    [System.Runtime.CompilerServices.MethodImplAttribute((System.Runtime.CompilerServices.MethodImplOptions)0x1000)]
    extern public static  string GetString (string key, [uei.DefaultValue("\"\"")]  string defaultValue ) ;

    [uei.ExcludeFromDocs]
    public static string GetString (string key) {
        string defaultValue = "";
        return GetString ( key, defaultValue );
    }

    [UnityEngine.Scripting.GeneratedByOldBindingsGeneratorAttribute] // Temporarily necessary for bindings migration
    [System.Runtime.CompilerServices.MethodImplAttribute((System.Runtime.CompilerServices.MethodImplOptions)0x1000)]
    extern private static  void SetBoolInternal (string key, bool value) ;

    public static void SetBool(string key, bool value)
        {
            SetBoolInternal(key, value);
            if (onValueWasUpdated != null)
                onValueWasUpdated(key);
        }
    
    
    [UnityEngine.Scripting.GeneratedByOldBindingsGeneratorAttribute] // Temporarily necessary for bindings migration
    [System.Runtime.CompilerServices.MethodImplAttribute((System.Runtime.CompilerServices.MethodImplOptions)0x1000)]
    extern public static  bool GetBool (string key, [uei.DefaultValue("false")]  bool defaultValue ) ;

    [uei.ExcludeFromDocs]
    public static bool GetBool (string key) {
        bool defaultValue = false;
        return GetBool ( key, defaultValue );
    }

    [UnityEngine.Scripting.GeneratedByOldBindingsGeneratorAttribute] // Temporarily necessary for bindings migration
    [System.Runtime.CompilerServices.MethodImplAttribute((System.Runtime.CompilerServices.MethodImplOptions)0x1000)]
    extern public static  bool HasKey (string key) ;

    [UnityEngine.Scripting.GeneratedByOldBindingsGeneratorAttribute] // Temporarily necessary for bindings migration
    [System.Runtime.CompilerServices.MethodImplAttribute((System.Runtime.CompilerServices.MethodImplOptions)0x1000)]
    extern public static  void DeleteKey (string key) ;

    [UnityEngine.Scripting.GeneratedByOldBindingsGeneratorAttribute] // Temporarily necessary for bindings migration
    [System.Runtime.CompilerServices.MethodImplAttribute((System.Runtime.CompilerServices.MethodImplOptions)0x1000)]
    extern public static  void DeleteAll () ;

}


}
