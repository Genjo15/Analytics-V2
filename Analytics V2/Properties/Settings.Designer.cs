﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18444
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Analytics_V2.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\\\MIMAS\\BUREAUTIQUE\\GROUPES\\Direction de l\'International\\POLE INFORMATIQUE\\Develo" +
            "ppement Internes\\References Applications\\Analytics\\HC\\hc.cfg")]
        public string hc_config {
            get {
                return ((string)(this["hc_config"]));
            }
            set {
                this["hc_config"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("D:\\\\")]
        public string local_path {
            get {
                return ((string)(this["local_path"]));
            }
            set {
                this["local_path"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\\\MIMAS\\BUREAUTIQUE\\GROUPES\\Direction de l\'International\\POLE INFORMATIQUE\\Develo" +
            "ppement Internes\\References Applications\\Analytics\\Process Templates")]
        public string interpretation_template {
            get {
                return ((string)(this["interpretation_template"]));
            }
            set {
                this["interpretation_template"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\\\MIMAS\\BUREAUTIQUE\\GROUPES\\Direction de l\'International\\POLE INFORMATIQUE\\Develo" +
            "ppement Internes\\References Applications\\Analytics\\Recettes\\Template_XML.xml")]
        public string xml_template {
            get {
                return ((string)(this["xml_template"]));
            }
            set {
                this["xml_template"] = value;
            }
        }
    }
}
