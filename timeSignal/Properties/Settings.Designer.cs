﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.42000
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace timeSignal.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "15.7.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".\\Log")]
        public string ErrLogPath {
            get {
                return ((string)(this["ErrLogPath"]));
            }
            set {
                this["ErrLogPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ja-JP")]
        public string CultureInfoJp {
            get {
                return ((string)(this["CultureInfoJp"]));
            }
            set {
                this["CultureInfoJp"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("tt hh時mm分")]
        public string TimeFormatJp {
            get {
                return ((string)(this["TimeFormatJp"]));
            }
            set {
                this["TimeFormatJp"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("yyyy年MM月dd日(dddd)")]
        public string DateFormatJp {
            get {
                return ((string)(this["DateFormatJp"]));
            }
            set {
                this["DateFormatJp"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("en-US")]
        public string CultureInfoEn {
            get {
                return ((string)(this["CultureInfoEn"]));
            }
            set {
                this["CultureInfoEn"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("hh:mm tt")]
        public string TimeFormatEn {
            get {
                return ((string)(this["TimeFormatEn"]));
            }
            set {
                this["TimeFormatEn"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".\\img\\notify.ico")]
        public string NotifyIconPath {
            get {
                return ((string)(this["NotifyIconPath"]));
            }
            set {
                this["NotifyIconPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".\\img\\notify_stop.ico")]
        public string NotifyStopIconPath {
            get {
                return ((string)(this["NotifyStopIconPath"]));
            }
            set {
                this["NotifyStopIconPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Start/Pause")]
        public string RunToolStripMenuItem {
            get {
                return ((string)(this["RunToolStripMenuItem"]));
            }
            set {
                this["RunToolStripMenuItem"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("en-US/ja-JP")]
        public string LanguageToolStripMenuItem {
            get {
                return ((string)(this["LanguageToolStripMenuItem"]));
            }
            set {
                this["LanguageToolStripMenuItem"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("{D65231B0-B2F1-4857-A4CE-A8E7C6EA7D27}\\WindowsPowerShell\\v1.0\\powershell.exe")]
        public string NotificationAppID {
            get {
                return ((string)(this["NotificationAppID"]));
            }
            set {
                this["NotificationAppID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Error.log")]
        public string ErrLogFileName {
            get {
                return ((string)(this["ErrLogFileName"]));
            }
            set {
                this["ErrLogFileName"] = value;
            }
        }
    }
}
