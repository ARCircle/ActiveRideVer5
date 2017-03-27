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
    //Destenation InputManager Path
    private const string YamlDestinationPath = @"ProjectSettings\InputManager2.asset";
    private const string YamlHeader = "%YAML 1.1 \n%TAG !u! tag:unity3d.com,2011:\n--- !u!13 &1\n";

    public void Main()
    {
        using (var input = new StreamReader(@"ProjectSettings\InputManager.asset", Encoding.UTF8))
        {
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

        }
        //var inputManager = (YamlSequenceNode)mapping.Children[new YamlSequenceNode("InputManager:")];
    }

    public void RecoveryDefaultInputManager()
    {

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

	//デフォルトのYamlファイル InputManager.assets
    private const string InputManagerString = @"---
            InputManager:
                m_ObjectHideFlags: 0
                serializedVersion: 2
                m_Axes:
                  - serializedVersion: 3
                    m_Name: Horizontal2
                    descriptiveName:
                    descriptiveNegativeName:
                    negativeButton:
                    positiveButton:
                    altNegativeButton: left
                    altPositiveButton: right
                    gravity: 3
                    dead: 0.1
                    sensitivity: 3
                    snap: 0
                    invert: 0
                    type: 2
                    axis: 0
                    joyNum: 2
                  - serializedVersion: 3
                    m_Name: Horizontal
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: 
                    altNegativeButton: a
                    altPositiveButton: d
                    gravity: 3
                    dead: 0.1
                    sensitivity: 3
                    snap: 0
                    invert: 0
                    type: 2
                    axis: 0
                    joyNum: 1
                  - serializedVersion: 3
                    m_Name: Vertical2
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: down
                    positiveButton: up
                    altNegativeButton: 
                    altPositiveButton: 
                    gravity: 1
                    dead: 0.1
                    sensitivity: 3
                    snap: 0
                    invert: 1
                    type: 2
                    axis: 1
                    joyNum: 2
                  - serializedVersion: 3
                    m_Name: Vertical
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: 
                    altNegativeButton: s
                    altPositiveButton: w
                    gravity: 1
                    dead: 0.1
                    sensitivity: 3
                    snap: 0
                    invert: 1
                    type: 2
                    axis: 1
                    joyNum: 1
                  - serializedVersion: 3
                    m_Name: Fire1
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: joystick 1 button 0
                    altNegativeButton: 
                    altPositiveButton: left ctrl
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 2
                    axis: 0
                    joyNum: 1
                  - serializedVersion: 3
                    m_Name: Jump
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: joystick 1 button 2
                    altNegativeButton: 
                    altPositiveButton: left alt
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 2
                    axis: 0
                    joyNum: 1
                  - serializedVersion: 3
                    m_Name: Fire2
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: joystick 2 button 0
                    altNegativeButton: 
                    altPositiveButton: right ctrl
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 2
                    axis: 0
                    joyNum: 2
                  - serializedVersion: 3
                    m_Name: Jump2
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: joystick 2 button 2
                    altNegativeButton: 
                    altPositiveButton: right alt
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
                    positiveButton: joystick 1 button 3
                    altNegativeButton: 
                    altPositiveButton: q
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 2
                    axis: 0
                    joyNum: 1
                  - serializedVersion: 3
                    m_Name: Lock2
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: joystick 2 button 3
                    altNegativeButton: 
                    altPositiveButton: p
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 2
                    axis: 0
                    joyNum: 2
                  - serializedVersion: 3
                    m_Name: CamReset
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: joystick button 9
                    altNegativeButton: 
                    altPositiveButton: 
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 2
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: Boost
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: joystick 1 button 6
                    altNegativeButton: 
                    altPositiveButton: z
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 2
                    axis: 0
                    joyNum: 1
                  - serializedVersion: 3
                    m_Name: Boost2
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: joystick 2 button 6
                    altNegativeButton: 
                    altPositiveButton: m
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 2
                    axis: 0
                    joyNum: 2
                  - serializedVersion: 3
                    m_Name: Submit
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: joystick button 3
                    altNegativeButton: 
                    altPositiveButton: enter
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 2
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: Cancel
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: joystick 1 button 2
                    altNegativeButton: 
                    altPositiveButton: c
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 2
                    axis: 0
                    joyNum: 0
                  - serializedVersion: 3
                    m_Name: Horizontal3
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: 
                    altNegativeButton: j
                    altPositiveButton: l
                    gravity: 3
                    dead: 0.1
                    sensitivity: 3
                    snap: 0
                    invert: 0
                    type: 2
                    axis: 2
                    joyNum: 1
                  - serializedVersion: 3
                    m_Name: Vertical3
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: 
                    altNegativeButton: k
                    altPositiveButton: i
                    gravity: 3
                    dead: 0.1
                    sensitivity: 3
                    snap: 0
                    invert: 1
                    type: 2
                    axis: 4
                    joyNum: 1
                  - serializedVersion: 3
                    m_Name: Horizontal4
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: 
                    altNegativeButton: 
                    altPositiveButton: 
                    gravity: 3
                    dead: 0.1
                    sensitivity: 3
                    snap: 0
                    invert: 0
                    type: 2
                    axis: 2
                    joyNum: 2
                  - serializedVersion: 3
                    m_Name: Vertical4
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: 
                    altNegativeButton: 
                    altPositiveButton: 
                    gravity: 3
                    dead: 0.1
                    sensitivity: 3
                    snap: 0
                    invert: 1
                    type: 2
                    axis: 4
                    joyNum: 2
                  - serializedVersion: 3
                    m_Name: ShootMode1
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: joystick 1 button 7
                    altNegativeButton: 
                    altPositiveButton: space
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 2
                    axis: 0
                    joyNum: 1
                  - serializedVersion: 3
                    m_Name: ShootMode2
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: joystick 2 button 7
                    altNegativeButton: 
                    altPositiveButton: n
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 2
                    axis: 0
                    joyNum: 2
                  - serializedVersion: 3
                    m_Name: Option
                    descriptiveName: 
                    descriptiveNegativeName: 
                    negativeButton: 
                    positiveButton: joystick button 11
                    altNegativeButton: 
                    altPositiveButton: o
                    gravity: 1000
                    dead: 0.001
                    sensitivity: 1000
                    snap: 0
                    invert: 0
                    type: 2
                    axis: 0
                    joyNum: 0
";

}
