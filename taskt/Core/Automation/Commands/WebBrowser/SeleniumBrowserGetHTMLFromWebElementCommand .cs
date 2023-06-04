﻿using System;
using System.Xml.Serialization;
using taskt.Core.Automation.Attributes.PropertyAttributes;

namespace taskt.Core.Automation.Commands
{
    [Serializable]
    [Attributes.ClassAttributes.Group("Web Browser Commands")]
    [Attributes.ClassAttributes.SubGruop("WebElement Action")]
    [Attributes.ClassAttributes.CommandSettings("Get HTML From WebElement")]
    [Attributes.ClassAttributes.Description("This command allows you to Get HTML from WebElement.")]
    [Attributes.ClassAttributes.UsesDescription("Use this command when you want to Get HTML from WebElement.")]
    [Attributes.ClassAttributes.ImplementationDescription("")]
    [Attributes.ClassAttributes.EnableAutomateRender(true)]
    [Attributes.ClassAttributes.EnableAutomateDisplayText(true)]
    public class SeleniumBrowserGetHTMLFromWebElementCommand : ScriptCommand
    {
        [XmlAttribute]
        [PropertyVirtualProperty(nameof(SeleniumBrowserControls), nameof(SeleniumBrowserControls.v_InputWebElementName))]
        public string v_WebElement { get; set; }

        [XmlAttribute]
        [PropertyVirtualProperty(nameof(GeneralPropertyControls), nameof(GeneralPropertyControls.v_Result))]
        public string v_Result { get; set; }

        public SeleniumBrowserGetHTMLFromWebElementCommand()
        {
        }

        public override void RunCommand(object sender)
        {
            var engine = (Engine.AutomationEngineInstance)sender;

            var getAttribute = new SeleniumBrowserGetAttributeFromWebElementCommand()
            {
                v_WebElement = this.v_WebElement,
                v_AttributeName = "outerHTML",
                v_Result = this.v_Result
            };
            getAttribute.RunCommand(engine);
        }
    }
}