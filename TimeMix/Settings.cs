using System;
using System.ComponentModel;
using System.Configuration;
using System.Windows;
namespace TimeMix.Properties
{


    // 通过此类可以处理设置类的特定事件: 
    //  在更改某个设置的值之前将引发 SettingChanging 事件。
    //  在更改某个设置的值之后将引发 PropertyChanged 事件。
    //  在加载设置值之后将引发 SettingsLoaded 事件。
    //  在保存设置值之前将引发 SettingsSaving 事件。
    internal sealed partial class Settings
    {

        public Settings()
        {
            PropertyChanged += PropertyChangedEventHandler;
        }
        private void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e)
        {
            Default.Save();
        }
    }
}
