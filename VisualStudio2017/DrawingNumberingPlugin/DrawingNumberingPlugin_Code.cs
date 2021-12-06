/* Copyright 2018 Dawid Dyrcz 
 * See License.txt file 
 */

using System;
using Tekla.Structures.Plugins;
using System.Collections.Generic;

namespace DrawingNumberingPlugin
{
    public class DrawingNumberingPlugin_StructuresData
    {
        [StructuresField("someAttribute")]
        public string someAttribute;
    }

    [Plugin("Drawing Numbering Tool")] 
    [PluginUserInterface("DrawingNumberingPlugin.StarterForm")]
    [InputObjectDependency(PluginBase.InputObjectDependency.NOT_DEPENDENT)]
   
    public class DrawingNumberingPlugin : PluginBase
    {
        private readonly DrawingNumberingPlugin_StructuresData _data;
        
        public DrawingNumberingPlugin(DrawingNumberingPlugin_StructuresData data)
        {
            _data = data;
        }

        public override List<InputDefinition> DefineInput()
        {
            throw new NotImplementedException();
        }

        public override bool Run(List<InputDefinition> Input)
        {
            throw new NotImplementedException();
        }

    }
}
