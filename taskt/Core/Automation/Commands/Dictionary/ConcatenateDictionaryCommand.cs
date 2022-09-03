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
    [Attributes.ClassAttributes.SubGruop("Dictionary Action")]
    [Attributes.ClassAttributes.Description("This command allows you to concatenate two Dictionaries.")]
    [Attributes.ClassAttributes.UsesDescription("Use this command when you want to concatenate two Dictionaries.")]
    [Attributes.ClassAttributes.ImplementationDescription("")]
    [Attributes.ClassAttributes.EnableAutomateRender(true)]
    [Attributes.ClassAttributes.EnableAutomateDisplayText(true)]
    public class ConcatenateDictionaryCommand : ScriptCommand
    {
        [XmlAttribute]
        [PropertyDescription("Please input The Dictionary Variable 1")]
        [PropertyUIHelper(PropertyUIHelper.UIAdditionalHelperType.ShowVariableHelper)]
        [InputSpecification("Enter a string of comma seperated values.")]
        [SampleUsage("**myDictionary1** or **{{{vMyDic1}}}**")]
        [Remarks("")]
        [PropertyShowSampleUsageInDescription(true)]
        [PropertyRecommendedUIControl(PropertyRecommendedUIControl.RecommendeUIControlType.ComboBox)]
        [PropertyInstanceType(PropertyInstanceType.InstanceType.Dictionary)]
        [PropertyValidationRule("Dictionary1", PropertyValidationRule.ValidationRuleFlags.Empty)]
        [PropertyDisplayText(true, "Dictionary1")]
        public string v_InputDataA { get; set; }

        [XmlAttribute]
        [PropertyDescription("Please input The Dictionary Variable 2")]
        [PropertyUIHelper(PropertyUIHelper.UIAdditionalHelperType.ShowVariableHelper)]
        [InputSpecification("Enter a string of comma seperated values.")]
        [SampleUsage("**myDictionary2** or **{{{vMyDic2}}}**")]
        [Remarks("")]
        [PropertyShowSampleUsageInDescription(true)]
        [PropertyRecommendedUIControl(PropertyRecommendedUIControl.RecommendeUIControlType.ComboBox)]
        [PropertyInstanceType(PropertyInstanceType.InstanceType.Dictionary)]
        [PropertyValidationRule("Dictionary2", PropertyValidationRule.ValidationRuleFlags.Empty)]
        [PropertyDisplayText(true, "Dictionary2")]
        public string v_InputDataB { get; set; }

        [XmlAttribute]
        [PropertyDescription("If Key already exists")]
        [PropertyUIHelper(PropertyUIHelper.UIAdditionalHelperType.ShowVariableHelper)]
        [InputSpecification("")]
        [SampleUsage("**Ignore** or **Overwrite** or **Error**")]
        [Remarks("")]
        [PropertyIsOptional(true, "Ignore")]
        [PropertyRecommendedUIControl(PropertyRecommendedUIControl.RecommendeUIControlType.ComboBox)]
        [PropertyUISelectionOption("Ignore")]
        [PropertyUISelectionOption("Overwrite")]
        [PropertyUISelectionOption("Error")]
        public string v_KeyExists { get; set; }

        [XmlAttribute]
        [PropertyDescription("Please indicate the result Dictionary")]
        [PropertyUIHelper(PropertyUIHelper.UIAdditionalHelperType.ShowVariableHelper)]
        [InputSpecification("")]
        [SampleUsage("**newDic** or **{{{newDic}}}**")]
        [Remarks("")]
        [PropertyShowSampleUsageInDescription(true)]
        [PropertyRecommendedUIControl(PropertyRecommendedUIControl.RecommendeUIControlType.ComboBox)]
        [PropertyIsVariablesList(true)]
        [PropertyParameterDirection(PropertyParameterDirection.ParameterDirection.Output)]
        [PropertyInstanceType(PropertyInstanceType.InstanceType.Dictionary)]
        [PropertyValidationRule("New Dictionary", PropertyValidationRule.ValidationRuleFlags.Empty)]
        [PropertyDisplayText(true, "New Dictionary")]
        public string v_OutputName { get; set; }

        public ConcatenateDictionaryCommand()
        {
            this.CommandName = "ConcatenateDictionaryCommand";
            this.SelectionName = "Concatenate Dictionary";
            this.CommandEnabled = true;
            this.CustomRendering = true;
        }

        public override void RunCommand(object sender)
        {
            var engine = (Engine.AutomationEngineInstance)sender;

            Dictionary<string, string> dicA = v_InputDataA.GetDictionaryVariable(engine);

            Dictionary<string, string> dicB = v_InputDataB.GetDictionaryVariable(engine);

            Dictionary<string, string> myDic = new Dictionary<string, string>();
            foreach(var v in dicA)
            {
                myDic.Add(v.Key, v.Value);
            }

            string keyExists = v_KeyExists.GetUISelectionValue("v_KeyExists", this, engine);

            switch (keyExists)
            {
                case "ignore":
                    foreach(var v in dicB)
                    {
                        if (!myDic.ContainsKey(v.Key))
                        {
                            myDic.Add(v.Key, v.Value);
                        }
                    }
                    break;
                case "overwrite":
                    foreach(var v in dicB)
                    {
                        if (!myDic.ContainsKey(v.Key))
                        {
                            myDic.Add(v.Key, v.Value);
                        }
                        else
                        {
                            myDic[v.Key] = v.Value;
                        }
                    }
                    break;
                case "error":
                    foreach(var v in dicB)
                    {
                        if (!myDic.ContainsKey(v.Key))
                        {
                            myDic.Add(v.Key, v.Value);
                        }
                        else
                        {
                            throw new Exception("Key " + v.Key + " is already exists.");
                        }
                    }
                    break;
            }
            myDic.StoreInUserVariable(engine, v_OutputName);
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
        //    return base.GetDisplayValue() + " [Dictionary1: '" + v_InputDataA + "', Dictionary2: '" + v_InputDataB + "', Result: '" + v_OutputName + "']";
        //}

        //public override bool IsValidate(frmCommandEditor editor)
        //{
        //    base.IsValidate(editor);

        //    if (String.IsNullOrEmpty(v_InputDataA))
        //    {
        //        this.IsValid = false;
        //        this.validationResult += "Dictionary Variable Name 1 is empty.\n";
        //    }
        //    if (String.IsNullOrEmpty(v_InputDataB))
        //    {
        //        this.IsValid = false;
        //        this.validationResult += "Dictionary Variable Name 2 is empty.\n";
        //    }
        //    if (String.IsNullOrEmpty(v_OutputName))
        //    {
        //        this.IsValid = false;
        //        this.validationResult += "Result Dictionary is empty.\n";
        //    }

        //    return this.IsValid;
        //}
    }
}