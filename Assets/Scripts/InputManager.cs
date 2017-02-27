// This file is generated by InputManagerGenerator.
using UnityEngine;
using System.Collections;
 
public static class InputManager {
    public enum AxisType {
        KeyOrMouseButton = 0,
        MouseMovement = 1,
        JoystickAxis = 2
    };
    public class InputAxis {
        public string name;
        public string descriptiveName;
        public string descriptiveNegativeName;
        public string negativeButton;
        public string positiveButton;
        public string altNegativeButton;
        public string altPositiveButton;
        public float gravity;
        public float dead;
        public float sensitivity;
        public bool snap = false;
        public bool invert = false;
        public AxisType type;
        public int axis;
        public int joyNum;
    };
 
    public static InputAxis[] Config = new InputAxis[] {
        new InputAxis {
            name = "Horizontal2",
            descriptiveName = "",
            descriptiveNegativeName = "",
            negativeButton = "left",
            positiveButton = "right",
            altNegativeButton = "",
            altPositiveButton = "",
            gravity = 3f,
            dead = 0.002f,
            sensitivity = 3f,
            snap = false,
            invert = false,
            type = AxisType.KeyOrMouseButton,
            axis = 0,
            joyNum = 0,
        },
        new InputAxis {
            name = "Horizontal",
            descriptiveName = "",
            descriptiveNegativeName = "",
            negativeButton = "a",
            positiveButton = "d",
            altNegativeButton = "",
            altPositiveButton = "",
            gravity = 3f,
            dead = 0.001f,
            sensitivity = 3f,
            snap = false,
            invert = false,
            type = AxisType.KeyOrMouseButton,
            axis = 0,
            joyNum = 0,
        },
        new InputAxis {
            name = "Vertical2",
            descriptiveName = "",
            descriptiveNegativeName = "",
            negativeButton = "down",
            positiveButton = "up",
            altNegativeButton = "",
            altPositiveButton = "",
            gravity = 3f,
            dead = 0.001f,
            sensitivity = 3f,
            snap = false,
            invert = false,
            type = AxisType.KeyOrMouseButton,
            axis = 0,
            joyNum = 0,
        },
        new InputAxis {
            name = "Vertical",
            descriptiveName = "",
            descriptiveNegativeName = "",
            negativeButton = "s",
            positiveButton = "w",
            altNegativeButton = "",
            altPositiveButton = "",
            gravity = 3f,
            dead = 0.001f,
            sensitivity = 3f,
            snap = false,
            invert = false,
            type = AxisType.KeyOrMouseButton,
            axis = 0,
            joyNum = 0,
        },
        new InputAxis {
            name = "Fire1",
            descriptiveName = "",
            descriptiveNegativeName = "",
            negativeButton = "",
            positiveButton = "left ctrl",
            altNegativeButton = "",
            altPositiveButton = "mouse 0",
            gravity = 1000f,
            dead = 0.001f,
            sensitivity = 1000f,
            snap = false,
            invert = false,
            type = AxisType.KeyOrMouseButton,
            axis = 0,
            joyNum = 0,
        },
        new InputAxis {
            name = "Jump",
            descriptiveName = "",
            descriptiveNegativeName = "",
            negativeButton = "",
            positiveButton = "left shift",
            altNegativeButton = "",
            altPositiveButton = "",
            gravity = 1000f,
            dead = 0.001f,
            sensitivity = 1000f,
            snap = false,
            invert = false,
            type = AxisType.KeyOrMouseButton,
            axis = 0,
            joyNum = 0,
        },
        new InputAxis {
            name = "Fire2",
            descriptiveName = "",
            descriptiveNegativeName = "",
            negativeButton = "",
            positiveButton = "right ctrl",
            altNegativeButton = "",
            altPositiveButton = "",
            gravity = 1000f,
            dead = 0.001f,
            sensitivity = 1000f,
            snap = false,
            invert = false,
            type = AxisType.KeyOrMouseButton,
            axis = 0,
            joyNum = 0,
        },
        new InputAxis {
            name = "Jump2",
            descriptiveName = "",
            descriptiveNegativeName = "",
            negativeButton = "",
            positiveButton = "right shift",
            altNegativeButton = "",
            altPositiveButton = "",
            gravity = 1000f,
            dead = 0.001f,
            sensitivity = 1000f,
            snap = false,
            invert = false,
            type = AxisType.KeyOrMouseButton,
            axis = 0,
            joyNum = 0,
        },
        new InputAxis {
            name = "Lock",
            descriptiveName = "",
            descriptiveNegativeName = "",
            negativeButton = "",
            positiveButton = "joystick button 1",
            altNegativeButton = "",
            altPositiveButton = "q",
            gravity = 1000f,
            dead = 0.001f,
            sensitivity = 1000f,
            snap = false,
            invert = false,
            type = AxisType.KeyOrMouseButton,
            axis = 0,
            joyNum = 0,
        },
        new InputAxis {
            name = "Lock2",
            descriptiveName = "",
            descriptiveNegativeName = "",
            negativeButton = "",
            positiveButton = "",
            altNegativeButton = "",
            altPositiveButton = "p",
            gravity = 1000f,
            dead = 0.001f,
            sensitivity = 1000f,
            snap = false,
            invert = false,
            type = AxisType.KeyOrMouseButton,
            axis = 0,
            joyNum = 0,
        },
        new InputAxis {
            name = "CamReset",
            descriptiveName = "",
            descriptiveNegativeName = "",
            negativeButton = "",
            positiveButton = "joystick button 9",
            altNegativeButton = "",
            altPositiveButton = "e",
            gravity = 1000f,
            dead = 0.001f,
            sensitivity = 1000f,
            snap = false,
            invert = false,
            type = AxisType.KeyOrMouseButton,
            axis = 0,
            joyNum = 0,
        },
        new InputAxis {
            name = "Boost",
            descriptiveName = "",
            descriptiveNegativeName = "",
            negativeButton = "",
            positiveButton = "",
            altNegativeButton = "",
            altPositiveButton = "z",
            gravity = 1000f,
            dead = 0.001f,
            sensitivity = 1000f,
            snap = false,
            invert = false,
            type = AxisType.KeyOrMouseButton,
            axis = 0,
            joyNum = 0,
        },
        new InputAxis {
            name = "Boost2",
            descriptiveName = "",
            descriptiveNegativeName = "",
            negativeButton = "",
            positiveButton = "",
            altNegativeButton = "",
            altPositiveButton = "m",
            gravity = 1000f,
            dead = 0.001f,
            sensitivity = 1000f,
            snap = false,
            invert = false,
            type = AxisType.KeyOrMouseButton,
            axis = 0,
            joyNum = 0,
        },
        new InputAxis {
            name = "Submit",
            descriptiveName = "",
            descriptiveNegativeName = "",
            negativeButton = "",
            positiveButton = "",
            altNegativeButton = "",
            altPositiveButton = "space",
            gravity = 1000f,
            dead = 0.001f,
            sensitivity = 1000f,
            snap = false,
            invert = false,
            type = AxisType.KeyOrMouseButton,
            axis = 0,
            joyNum = 0,
        },
        new InputAxis {
            name = "Cancel",
            descriptiveName = "",
            descriptiveNegativeName = "",
            negativeButton = "",
            positiveButton = "",
            altNegativeButton = "",
            altPositiveButton = "space",
            gravity = 1000f,
            dead = 0.001f,
            sensitivity = 1000f,
            snap = false,
            invert = false,
            type = AxisType.KeyOrMouseButton,
            axis = 0,
            joyNum = 0,
        },
        new InputAxis {
            name = "Mouse ScrollWheel",
            descriptiveName = "",
            descriptiveNegativeName = "",
            negativeButton = "",
            positiveButton = "",
            altNegativeButton = "",
            altPositiveButton = "joystick button 1",
            gravity = 1000f,
            dead = 0.001f,
            sensitivity = 1000f,
            snap = false,
            invert = false,
            type = AxisType.KeyOrMouseButton,
            axis = 0,
            joyNum = 0,
        },
        new InputAxis {
            name = "U",
            descriptiveName = "",
            descriptiveNegativeName = "",
            negativeButton = "",
            positiveButton = "",
            altNegativeButton = "",
            altPositiveButton = "u",
            gravity = 1000f,
            dead = 0.001f,
            sensitivity = 1000f,
            snap = false,
            invert = false,
            type = AxisType.KeyOrMouseButton,
            axis = 0,
            joyNum = 0,
        },
    };
}
