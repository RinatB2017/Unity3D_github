// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEditor.Experimental.Build.AssetBundle
{
    [Serializable]
    [UsedByNativeCode]
    [StructLayout(LayoutKind.Sequential)]
    public struct SerializedLocation
    {
        [NativeName("fileName")]
        internal string m_FileName;
        public string fileName { get { return m_FileName; } }

        [NativeName("offset")]
        internal ulong m_Offset;
        public ulong offset { get { return m_Offset; } }

        [NativeName("size")]
        internal ulong m_Size;
        public ulong size { get { return m_Size; } }
    }


    [Serializable]
    [UsedByNativeCode]
    [StructLayout(LayoutKind.Sequential)]
    public struct ObjectSerializedInfo
    {
        [NativeName("serializedObject")]
        internal ObjectIdentifier m_SerializedObject;
        public ObjectIdentifier serializedObject { get { return m_SerializedObject; } }

        [NativeName("header")]
        internal SerializedLocation m_Header;
        public SerializedLocation header { get { return m_Header; } }

        [NativeName("rawData")]
        internal SerializedLocation m_RawData;
        public SerializedLocation rawData { get { return m_RawData; } }
    }


    [Serializable]
    [UsedByNativeCode]
    [StructLayout(LayoutKind.Sequential)]
    public struct WriteResult
    {
        [NativeName("assetBundleName")]
        internal string m_AssetBundleName;
        public string assetBundleName { get { return m_AssetBundleName; } }

        [NativeName("assetBundleAssets")]
        internal GUID[] m_AssetBundleAssets;
        public ReadOnlyCollection<GUID> assetBundleAssets { get { return Array.AsReadOnly(m_AssetBundleAssets); } }

        [NativeName("assetBundleObjects")]
        internal ObjectSerializedInfo[] m_AssetBundleObjects;
        public ReadOnlyCollection<ObjectSerializedInfo> assetBundleObjects { get { return Array.AsReadOnly(m_AssetBundleObjects); } }

        [NativeName("resourceFiles")]
        internal ResourceFile[] m_ResourceFiles;
        public ReadOnlyCollection<ResourceFile> resourceFiles { get { return Array.AsReadOnly(m_ResourceFiles); } }

        [NativeName("includedTypes")]
        internal Type[] m_IncludedTypes;
        public ReadOnlyCollection<Type> includedTypes { get { return Array.AsReadOnly(m_IncludedTypes); } }
    }


    [Serializable]
    [UsedByNativeCode]
    [StructLayout(LayoutKind.Sequential)]
    public struct BuildOutput
    {
        [NativeName("results")]
        internal WriteResult[] m_Results;
        public ReadOnlyCollection<WriteResult> results { get { return Array.AsReadOnly(m_Results); } }
    }
}
