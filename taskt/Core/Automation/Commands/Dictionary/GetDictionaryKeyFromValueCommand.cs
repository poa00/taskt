﻿using System;
using System.Linq;
using System.Xml.Serialization;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using taskt.UI.Forms;
using taskt.UI.CustomControls;
using taskt.Core.Automation.Attributes.PropertyAttributes;

namespace taskt.Core.Automation.Commands
{
    [Serializable]
    [Attributes.ClassAttributes.Group("Dictionary Commands")]
    [Attributes.ClassAttributes.SubGruop("Dictionary Key")]
    [Attributes.ClassAttributes.Description("This command allows you to get Dictionary key Name from Value")]
    [Attributes.ClassAttributes.UsesDescription("Use this command when you want to get Dictionary key Name from Value.")]
    [Attributes.ClassAttributes.ImplementationDescription("")]
    [Attributes.ClassAttributes.EnableAutomateRender(true)]
    [Attributes.ClassAttributes.EnableAutomateDisplayText(true)]
    public class GetDictionaryKeyFromValueCommand : ScriptCommand
    {
        [XmlAttribute]
        [PropertyDescription("Please input The Dictionary Variable")]
        [PropertyUIHelper(PropertyUIHelper.UIAdditionalHelperType.ShowVariableHelper)]
        [InputSpecification("Enter a string of comma seperated values.")]
        [SampleUsage("**myDictionary** or **{{{vMyDic}}}**")]
        [Remarks("")]
        [PropertyShowSampleUsageInDescription(true)]
        [PropertyRecommendedUIControl(PropertyRecommendedUIControl.RecommendeUIControlType.ComboBox)]
        [PropertyInstanceType(PropertyInstanceType.InstanceType.Dictionary)]
        [PropertyValidationRule("Dictionary", PropertyValidationRule.ValidationRuleFlags.Empty)]
        [PropertyDisplayText(true, "Dictionary")]
        public string v_InputData { get; set; }

        [XmlAttribute]
        [PropertyDescription("Please indicate the value for the Dictionary")]
        [PropertyUIHelper(PropertyUIHelper.UIAdditionalHelperType.ShowVariableHelper)]
        [InputSpecification("")]
        [SampleUsage("**value1** or **{{{vValue}}}**")]
        [Remarks("If Key not found, result is empty.")]
        [PropertyShowSampleUsageInDescription(true)]
        [PropertyRecommendedUIControl(PropertyRecommendedUIControl.RecommendeUIControlType.TextBox)]
        [PropertyValidationRule("Value", PropertyValidationRule.ValidationRuleFlags.Empty)]
        [PropertyDisplayText(true, "Value")]
        public string v_Value { get; set; }

        [XmlAttribute]
        [PropertyDescription("Please indicate the variable to apply Key name")]
        [InputSpecification("Enter a unique dataset name that will be used later to traverse over the data")]
        [SampleUsage("**vKey** or **{{{vKey}}}**")]
        [Remarks("")]
        [PropertyShowSampleUsageInDescription(true)]
        [PropertyUIHelper(PropertyUIHelper.UIAdditionalHelperType.ShowVariableHelper)]
        [PropertyRecommendedUIControl(PropertyRecommendedUIControl.RecommendeUIControlType.ComboBox)]
        [PropertyIsVariablesList(true)]
        [PropertyValidationRule("Key", PropertyValidationRule.ValidationRuleFlags.Empty)]
        [PropertyDisplayText(true, "Result")]
        public string v_OutputVariable { get; set; }

        public GetDictionaryKeyFromValueCommand()
        {
            this.CommandName = "GetDictionaryKeyFromValueCommand";
            this.SelectionName = "Get Dictionary Key From Value";
            this.CommandEnabled = true;
            this.CustomRendering = true;
        }

        public override void RunCommand(object sender)
        {
            var engine = (Engine.AutomationEngineInstance)sender;
            var vValue = v_Value.ConvertToUserVariable(sender);

            Dictionary<string, string> dic = v_InputData.GetDictionaryVariable(engine);
            
            foreach(KeyValuePair<string, string> item in dic)
            {
                if (item.Value == vValue)
                {
                    item.Key.StoreInUserVariable(engine, v_OutputVariable);
                    return;
                }
            }
            "".StoreInUserVariable(engine, v_OutputVariable);
        }
        
        //public override List<Control> Render(frmCommandEditor editor)
        //{
        //    base.Render(editor);

        //    var ctrls = CommandControls.MultiCreateInferenceDefaultControlGroupFor(this, editor);
        //    RenderedControls.AddRange(ctrls);

        //    return RenderedControls;
        //}

        //public override string GetDisplayValue()
        //{
        //    return base.GetDisplayValue() + $" [From: {v_InputData}, Value: {v_Value}, Result: {v_OutputVariable}]";
        //}

        //public override bool IsValidate(frmCommandEditor editor)
        //{
        //    base.IsValidate(editor);

        //    if (String.IsNullOrEmpty(v_InputData))
        //    {
        //        this.IsValid = false;
        //        this.validationResult += "Dictionary Variable Name is empty.\n";
        //    }
        //    if (String.IsNullOrEmpty(v_Value))
        //    {
        //        this.IsValid = false;
        //        this.validationResult += "Key is empty.\n";
        //    }
        //    if (String.IsNullOrEmpty(v_OutputVariable))
        //    {
        //        this.IsValid = false;
        //        this.validationResult += "Variable is empty.\n";
        //    }

        //    return this.IsValid;
        //}
    }
}