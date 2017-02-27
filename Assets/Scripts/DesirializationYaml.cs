using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class DesirializationYaml
{
    private const string YamlDestinationPath = @"ProjectSettings\InputManager2.asset";
    private const string YamlHeader = "%YAML 1.1 \n%TAG !u! tag:unity3d.com,2011:\n--- !u!13 &1\n";

    public void Main()
    {
        Debug.Log("Output Yaml");

        var input = new StreamReader(@"ProjectSettings\InputManager.asset", Encoding.UTF8);
        var yaml = new YamlStream();
        yaml.Load(input);
        var mapping = (YamlMappingNode)yaml.Documents[0].RootNode;
        var inputManager = mapping.Children[new YamlScalarNode("InputManager")];
        var mapping2 = (YamlMappingNode)inputManager;

        var Axes = (YamlSequenceNode)mapping2.Children[new YamlScalarNode("m_Axes")];
        foreach (YamlMappingNode item in Axes)
        {
            foreach (var child in item)
            {
                Debug.Log(((YamlScalarNode)child.Key).Value);
                Debug.Log(((YamlScalarNode)child.Value).Value);
            }
        }

        string txt = input.ReadToEnd();
        //var input_d = new StringReader(input.ReadToEnd());
        Debug.Log(txt);
        input.Close();
        //var inputManager = (YamlSequenceNode)mapping.Children[new YamlSequenceNode("InputManager:")];
    }

    public void RecoveryDefaultInputManager()
    {
        //TODO: 一旦InputManagerStringをDeserializeしてからSerialize => 出力したほうが綺麗?
        //　　 ->検討したうえで修正

        //var serializer = new SerializerBuilder().Build();
        //var reSerializedYaml = serializer.Serialize(iM);

        if (System.IO.File.Exists(YamlDestinationPath))
        {
            System.IO.File.Delete(YamlDestinationPath);
        }

        string OutputString = YamlHeader + InputManagerString;
        StreamWriter writeYaml = new StreamWriter(YamlDestinationPath, false, Encoding.UTF8);
        writeYaml.Write(OutputString);
        writeYaml.Close();
    }

    public InputManagerFile DeserializeDefaultYaml()
    {
        var input_d = new StringReader(InputManagerString);

        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(new CamelCaseNamingConvention())
            .Build();
        var iM = deserializer.Deserialize<InputManagerFile>(input_d);

        input_d.Close();

        return iM;
    }

    public void SerializeYaml(InputManagerFile DeserializedYaml){
        var serializer = new SerializerBuilder().Build();
        var reSerializedYaml = serializer.Serialize(DeserializedYaml);

        if (System.IO.File.Exists(YamlDestinationPath))
        {
            System.IO.File.Delete(YamlDestinationPath);
        }

        string OutputString = YamlHeader + reSerializedYaml;
        StreamWriter writeYaml = new StreamWriter(YamlDestinationPath, false, Encoding.UTF8);
        writeYaml.Write(OutputString);
        writeYaml.Close();
    }

    public class InputManagerFile
    {
        [YamlMember(Alias = "InputManager", ApplyNamingConventions = false)]
        public InputManager inputManager { get; set; }
    }
    public class InputManager
    {
        [YamlMember(Alias = "m_ObjectHideFlags", ApplyNamingConventions = false)]
        public string M_ObjectHideFlags { get; set; }
        public string SerializedVersion { get; set; }

        [YamlMember(Alias = "m_Axes", ApplyNamingConventions = false)]
        public List<m_Axes> Axes { get; set; }

    }

    public class m_Axes
    {
        public int SerializedVersion { get; set; }
        [YamlMember(Alias = "m_Name", ApplyNamingConventions = false)]
        public string m_Name { get; set; }
        public string DescriptiveName { get; set; }
        [YamlMember(Alias = "descriptiveNegativeName", ApplyNamingConventions = false)]
        public string DescriptiveNegativeName { get; set; }
        public string NegativeButton { get; set; }
        public string PositiveButton { get; set; }
        public string AltNegativeButton { get; set; }
        public string AltPositiveButton { get; set; }
        public int Gravity { get; set; }
        public decimal Dead { get; set; }
        public int Sensitivity { get; set; }
        public int Snap { get; set; }
        public int Invert { get; set; }
        public int Type { get; set; }
        public int Axis { get; set; }
        public int joyNum { get; set; }
    }

    private const string InputManagerString = @"---
            InputManager:
                m_ObjectHideFlags: 0
                serializedVersion: 2
                m_Axes:
                  - serializedVersion: 3
                    m_Name: Horizontal2
                    descriptiveName:
                    descriptiveNegativeName:
                    negativeButton: left
                    positiveButton: right
                    altNegativeButton: 
                    altPositiveButton: 
                    gravity: 3
                    dead: 0.002
                    sensitivity: 3
                    snap: 0
                    invert: 0
                    type: 0
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: Horizontal
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: a
                    positiveButton: d
                    altNegativeButton: 
                    altPositiveButton: 
                    gravity: 3
                    dead: 0.001
                    sensitivity: 3
                    snap: 0
                    invert: 0
                    type: 0
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: Vertical2
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: down
                    positiveButton: up
                    altNegativeButton: 
                    altPositiveButton: 
                    gravity: 3
                    dead: 0.001
                    sensitivity: 3
                    snap: 0
                    invert: 0
                    type: 0
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: Vertical
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: s
                    positiveButton: w
                    altNegativeButton: 
                    altPositiveButton: 
                    gravity: 3
                    dead: 0.001
                    sensitivity: 3
                    snap: 0
                    invert: 0
                    type: 0
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: Fire1
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: left ctrl
                    altNegativeButton: 
                    altPositiveButton: mouse 0
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 0
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: Jump
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: left shift
                    altNegativeButton: 
                    altPositiveButton: 
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 0
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: Fire2
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: right ctrl
                    altNegativeButton: 
                    altPositiveButton: 
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 0
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: Jump2
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: right shift
                    altNegativeButton: 
                    altPositiveButton: 
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 0
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: Lock
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: joystick button 1
                    altNegativeButton: 
                    altPositiveButton: q
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 0
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: Lock2
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: 
                    altNegativeButton: 
                    altPositiveButton: p
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 0
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: CamReset
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: joystick button 9
                    altNegativeButton: 
                    altPositiveButton: e
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 0
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: Boost
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: 
                    altNegativeButton: 
                    altPositiveButton: z
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 0
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: Boost2
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: 
                    altNegativeButton: 
                    altPositiveButton: m
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 0
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: Submit
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: 
                    altNegativeButton: 
                    altPositiveButton: space
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 0
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: Cancel
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: 
                    altNegativeButton: 
                    altPositiveButton: space
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 0
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: Mouse ScrollWheel
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: 
                    altNegativeButton: 
                    altPositiveButton: joystick button 1
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 0
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: U
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: 
                    altNegativeButton: 
                    altPositiveButton: u
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 0
                    axis: 0
                    joyNum: 0
";

}
