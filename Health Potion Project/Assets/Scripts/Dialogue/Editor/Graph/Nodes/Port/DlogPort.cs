using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dlog {
    public class DlogPort : Port {
        private PortType type;
        public PortType Type => type;
        private DlogPort(Orientation portOrientation, Direction portDirection, Capacity portCapacity) : base(portOrientation, portDirection, portCapacity, typeof(object)) { }

        public static DlogPort Create(string name, Orientation portOrientation, Direction portDirection, Capacity portCapacity, PortType type, bool required, EdgeConnectorListener edgeConnectorListener, bool hideLabel = false) {
            var port = new DlogPort(portOrientation, portDirection, portCapacity);
            if (edgeConnectorListener != null) {
                port.m_EdgeConnector = new EdgeConnector<Edge>(edgeConnectorListener);
                port.AddManipulator(port.m_EdgeConnector);
            }
            
            port.AddStyleSheet("Styles/Node/Port");
            if (!required) {
                port.AddToClassList("optional");
            }
            
            port.type = type;
            port.portColor = PortHelper.PortColor(port);
            port.viewDataKey = Guid.NewGuid().ToString();
            port.portName = name;

            if (hideLabel) {
                var label = port.Q<Label>();
                var fs = label.style.fontSize;
                fs.value = -1;
                label.style.fontSize = fs;
                var color = label.style.color;
                color.value = Color.clear;
                label.style.color = color;
            }
            
            
            port.InjectCustomStyle();
            
            return port;
        }
    }
}
