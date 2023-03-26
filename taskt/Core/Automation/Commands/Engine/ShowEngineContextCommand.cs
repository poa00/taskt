﻿using System;
using System.Xml.Serialization;
using taskt.Core.Automation.Attributes.PropertyAttributes;

namespace taskt.Core.Automation.Commands
{
    [Serializable]
    [Attributes.ClassAttributes.Group("Engine Commands")]
    [Attributes.ClassAttributes.CommandSettings("Show Engine Context")]
    [Attributes.ClassAttributes.Description("This command allows you to show a message to the user.")]
    [Attributes.ClassAttributes.UsesDescription("Use this command when you want to present or display a value on screen to the user.")]
    [Attributes.ClassAttributes.ImplementationDescription("This command implements 'MessageBox' and invokes VariableCommand to find variable data.")]
    [Attributes.ClassAttributes.EnableAutomateRender(true)]
    [Attributes.ClassAttributes.EnableAutomateDisplayText(true)]
    public class ShowEngineContextCommand : ScriptCommand
    {
        [XmlAttribute]
        [PropertyVirtualProperty(nameof(GeneralPropertyControls), nameof(GeneralPropertyControls.v_DisallowNewLine_OneLineTextBox))]
        [PropertyDescription("Close After X Seconds - 0 to bypass")]
        [InputSpecification("Close Time", true)]
        [PropertyDetailSampleUsage("**0**", "Show Indefinitely")]
        [PropertyDetailSampleUsage("**5**", PropertyDetailSampleUsage.ValueType.Value, "Close Time")]
        [PropertyDetailSampleUsage("**{{{vTime}}}**", PropertyDetailSampleUsage.ValueType.VariableValue, "Close Time")]
        [PropertyValidationRule("Close Time", PropertyValidationRule.ValidationRuleFlags.LessThanZero)]
        [PropertyFirstValue("0")]
        [PropertyIsOptional(true, "0")]
        [PropertyDisplayText(true, "Close After", "Seconds")]
        public string v_AutoCloseAfter { get; set; }

        public ShowEngineContextCommand()
        {
            //this.CommandName = "ShowEngineContextCommand";
            //this.SelectionName = "Show Engine Context";
            //this.CommandEnabled = true;
            //this.v_AutoCloseAfter = "0";
            //this.CustomRendering = true;
        }

        public override void RunCommand(object sender)
        {
            var engine = (Engine.AutomationEngineInstance)sender;

            //if (engine.tasktEngineUI == null)
            //{           
            //    return;
            //}

            //v_AutoCloseAfter = v_AutoCloseAfter.ConvertToUserVariable(sender);
            //int closeValue;
            //if (!int.TryParse(v_AutoCloseAfter, out closeValue))
            //{
            //    closeValue = 10;
            //}

            var closeValue = this.ConvertToUserVariableAsInteger(nameof(v_AutoCloseAfter), engine);

            //automatically close messageboxes for server requests
            if (engine.serverExecution && closeValue <= 0)
            {
                closeValue = 10;
            }

            var result = engine.tasktEngineUI.Invoke(new Action(() =>
                {
                    engine.tasktEngineUI.ShowEngineContext(engine.GetEngineContext(), closeValue);
                }
            ));
        }

        //public override List<Control> Render(frmCommandEditor editor)
        //{
        //    base.Render(editor);


        //    //create auto close control set
        //    var autocloseControlSet = CommandControls.CreateDefaultInputGroupFor("v_AutoCloseAfter", this, editor);
        //    RenderedControls.AddRange(autocloseControlSet);


        //    return RenderedControls;
        //}

        //public override string GetDisplayValue()
        //{
        //    return base.GetDisplayValue();
        //}

        //public override bool IsValidate(frmCommandEditor editor)
        //{
        //    this.IsValid = true;
        //    this.validationResult = "";

        //    if (String.IsNullOrEmpty(v_AutoCloseAfter))
        //    {
        //        this.validationResult += "Close time is empty.\n";
        //        this.IsValid = true;
        //    }
        //    else
        //    {
        //        int v;
        //        if (!int.TryParse(v_AutoCloseAfter, out v))
        //        {
        //            if (v < 0)
        //            {
        //                this.validationResult += "Close time less than zero.\n";
        //                this.IsValid = true;
        //            }
        //        }
        //    }

        //    return this.IsValid;
        //}
    }
}
